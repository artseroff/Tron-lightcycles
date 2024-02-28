using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace ViewConsole
{
  /// <summary>
  /// Класс, позволяющий быстро выводить данные на консоль
  /// </summary>
  public class FastWriter
  {
    /// <summary>
    /// Дескриптор устройства стандартного вывода
    /// </summary>
    private const uint STD_OUTPUT_HANDLE = 0xFFFFFFF5;

    /// <summary>
    /// Буффер
    /// </summary>
    private static CharInfo[] _buf;

    /// <summary>
    /// Прямоугольный блок в буффере
    /// </summary>
    private static SmallRect _rect;

    /// <summary>
    /// Ширина буфера
    /// </summary>
    private static short _width;

    /// <summary>
    /// Высота буфера
    /// </summary>
    private static short _height;

    /// <summary>
    /// Дескриптор окна консоли
    /// </summary>
    private static SafeFileHandle _h;

    /*[DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern SafeFileHandle CreateFile(
       string fileName,
       [MarshalAs(UnmanagedType.U4)] uint fileAccess,
       [MarshalAs(UnmanagedType.U4)] uint fileShare,
       IntPtr securityAttributes,
       [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
       [MarshalAs(UnmanagedType.U4)] int flags,
       IntPtr template);*/    

    /// <summary>
    /// Извлекает дескриптор для стандартного ввода данных,
    /// стандартного вывода или стандартной ошибки устройства.
    /// </summary>
    /// <param name="parStdHandle">Устройство стандартного ввода, вывода или ошибки устройства</param>
    /// <returns>Если функция завершается успешно,
    /// возвращаемое значение - дескриптор
    /// определяемого устройства. Иначе возвращается
    /// значение INVALID_HANDLE_VALUE (-1). </returns>
    [DllImport("kernel32.dll")]
    private static extern SafeFileHandle GetStdHandle(uint parStdHandle);

    /// <summary>
    /// Записывает символ и данные атрибута цвета в заданном
    /// прямоугольном блоке символьных знакомест в экранном буфере консоли. 
    /// Данные, которые будут записаны, берутся из прямоугольного блока
    /// соответствующего размера в заданном месте в исходном буфере.
    /// </summary>
    /// <param name="parHConsoleOutput">Дескриптор экранного буфера</param>
    /// <param name="parLpBuffer">Буфер данных</param>
    /// <param name="parDwBufferSize">Размер буфера данных</param>
    /// <param name="parDwBufferCoord">Координаты ячейки</param>
    /// <param name="refLpWriteRegion">Прямоугольник для чтения</param>
    /// <returns>Если функция завершается успешно, 
    /// величина возвращаемого значения - не ноль. Иначе - 0</returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteConsoleOutput(
      SafeFileHandle parHConsoleOutput,
      CharInfo[] parLpBuffer,
      Coord parDwBufferSize,
      Coord parDwBufferCoord,
      ref SmallRect refLpWriteRegion);

    /// <summary>
    /// Структура координат
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
      /// <summary>
      /// Абсцисса
      /// </summary>
      public short x;

      /// <summary>
      /// Ордината
      /// </summary>
      public short y;

      /// <summary>
      /// Конструктор
      /// </summary>
      /// <param name="parX">Абсцисса</param>
      /// <param name="parY">Ордината</param>
      public Coord(short parX, short parY)
      {
        this.x = parX;
        this.y = parY;
      }
    };

    /// <summary>
    /// Объединение с информацией об
    /// Unicode и Ascii кодах символа
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct CharUnion
    {
      /// <summary>
      /// Unicode код
      /// </summary>
      [FieldOffset(0)] public char UnicodeChar;

      /// <summary>
      /// Ascii код
      /// </summary>
      [FieldOffset(0)] public byte AsciiChar;
    }

    /// <summary>
    /// Информация о кодах символа и
    /// цвете текста и фона
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
      /// <summary>
      /// Объединение с информацией об
      /// Unicode и Ascii кодах символа
      /// </summary>
      [FieldOffset(0)] public CharUnion Char;

      /// <summary>
      /// Атрибут цвет текста и фона
      /// (в старшем байте - цвет фона,
      /// в младшем - цвет текста)
      /// </summary>
      [FieldOffset(2)] public short Attributes;
    }

    /// <summary>
    /// Структура - прямоугольник
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
      /// <summary>
      /// Точка левой стороны
      /// </summary>
      public short Left;

      /// <summary>
      /// Точка верхнего стороны
      /// </summary>
      public short Top;

      /// <summary>
      /// Точка правой стороны
      /// </summary>
      public short Right;

      /// <summary>
      /// Точка нижней стороны
      /// </summary>
      public short Bottom;
    }

    /// <summary>
    /// Статический конструктор класса
    /// </summary>
    static FastWriter()
    {
      //_h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
      _h = GetStdHandle(STD_OUTPUT_HANDLE);
      _width = (short)Console.WindowWidth;
      _height = (short)Console.WindowHeight;

      if (!_h.IsInvalid)
      {
        _buf = new CharInfo[_height * _width];
        _rect = new SmallRect() { Left = 0, Top = 0, Right = _width, Bottom = _height };
      }
    }
   

    /// <summary>
    /// Записать строку в буфер на позиции с заданным цветом
    /// </summary>
    /// <param name="parS">Строка</param>
    /// <param name="parX">Позиция X</param>
    /// <param name="parY">Позиция Y</param>
    /// <param name="parForeground">Цвет текста</param>
    /// <param name="parBackground">Цвет заднего фона</param>
    public static void WriteToBuffer(string parS, int parX, int parY, ConsoleColor parForeground = ConsoleColor.White, ConsoleColor? parBackground = null)
    {

      byte[] bytes = Console.OutputEncoding.GetBytes(parS);
      byte currentByte;
      int currentIndex = parY * _width + parX;
      for (int i = 0; i < bytes.Length; i++)
      {
        currentByte = bytes[i];
        if (currentByte == 10 && bytes[i - 1] == 13)// \n и \r
        {
          parY++;
          currentIndex = parY * _width + parX;
        }
        else
        {
          if (currentByte != 13)
          {
            if (parBackground == null)
            {
              _buf[currentIndex].Attributes = (byte)parForeground;
            }
            else
            {
              _buf[currentIndex].Attributes = (short)((int)parForeground | (((short)parBackground) << 4));
            }
            _buf[currentIndex++].Char.AsciiChar = currentByte;
          }

        }
      }
    }

    /// <summary>
    /// Очистить буфер
    /// </summary>
    public static void Clear()
    {      
      _buf = new CharInfo[_height * _width];
      _rect = new SmallRect() { Left = 0, Top = 0, Right = _width, Bottom = _height };
    }

    /// <summary>
    /// Вывести буфер на консоль
    /// </summary>
    public static void PrintBuffer()
    {
      WriteConsoleOutput(_h, _buf,
            new Coord() { x = _width, y = _height },
            new Coord() { x = 0, y = 0 },
            ref _rect);
    }
  }
}
