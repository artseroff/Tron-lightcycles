using Model.Game.Level;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ViewConsole.Game;

namespace ViewConsole
{
  /// <summary>
  /// Класс настроек консольного окна
  /// </summary>
  public class ConsoleConfiguration
  {    

    /// <summary>
    /// Указывает, что parNPosition (в методе DeleteMenu)
    /// предоставляет идентификатор элемента меню
    /// </summary>
    private const int MF_BYCOMMAND = 0x00000000;

    /// <summary>
    /// Значение идентификатора меню свертывания окна
    /// </summary>
    private const int SC_MINIMIZE = 0xF020;

    /// <summary>
    /// Значение идентификатора меню развертывания окна
    /// </summary>
    private const int SC_MAXIMIZE = 0xF030;

    /// <summary>
    /// Значение идентификатора меню изменения размеров окна
    /// </summary>
    private const int SC_SIZE = 0xF000;

    /// <summary>
    /// Сохраняет текущий размер(игнорирует parCx и parCy параметры метода SetWindowPos)
    /// </summary>
    private const uint SWP_NOSIZE = 0x0001;

    /// <summary>
    /// Сохраняет текущую Z-последовательность(игнорирует параметр parHWndInsertAfter метода SetWindowPos)
    /// </summary>
    private const uint SWP_NOZORDER = 0x0004;

    /// <summary>
    /// Удаляет элемент из указанного меню
    /// </summary>
    /// <param name="parHMenu">Дескриптор меню, который необходимо изменить</param>
    /// <param name="parNPosition">Удаляемый элемент меню, определяемый параметром parWFlags</param>
    /// <param name="parWFlags">Указывает, как интерпретируется параметр parNPosition.
    /// MF_BYCOMMAND - Указывает, что parNPosition предоставляет идентификатор элемента меню.
    /// MF_BYPOSITION - Указывает, что parNPosition дает относительное положение элемента меню
    /// отсчитываемое от нуля.</param>
    /// <returns>Если функция выполняется успешно, возвращается ненулевое значение.
    /// Если функция выполняется неудачно, возвращается нулевое значение</returns>
    [DllImport("user32.dll")]
    public static extern int DeleteMenu(IntPtr parHMenu, int parNPosition, int parWFlags);
   

    /// <summary>
    /// Получить доступ к меню окна для копирования и изменения
    /// </summary>
    /// <param name="parHWnd">Дескриптор окна, который будет принадлежать копии меню окна</param>
    /// <param name="parBRevert">Если этот параметр имеет значение FALSE, GetSystemMenu
    /// возвращает дескриптор копии используемого меню окна. Если этот параметр имеет значение TRUE,
    /// GetSystemMenu сбрасывает меню окна обратно в состояние по умолчанию. Предыдущее меню окна,
    /// если таковое имеется, уничтожается.</param>
    /// <returns>Если параметр parBRevert имеет значение FALSE, возвращаемое значение является дескриптором копии меню окна.
    /// Если параметр parBRevert имеет значение TRUE, возвращаемое значение равно NULL</returns>
    [DllImport("user32.dll")]
    private static extern IntPtr GetSystemMenu(IntPtr parHWnd, bool parBRevert);

    /// <summary>
    /// Извлекает дескриптор окна, используемый консолью, связанной с вызывающим процессом
    /// </summary>
    /// <returns>Дескриптор окна, используемого консолью, связанной с вызывающим процессом
    /// или NULL, если нет какой-либо связанной консоли.</returns>
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GetConsoleWindow();


    /// <summary>
    /// Иизменяет размер, позицию и Z-индекс окна
    /// </summary>
    /// <param name="parHWnd">Дескриптор окна</param>
    /// <param name="parHWndInsertAfter">Дескриптор порядка размещения</param>
    /// <param name="parX">Позиция по горизонтали</param>
    /// <param name="parY">Позиция по горизонтали</param>
    /// <param name="parCx">Новая ширина</param>
    /// <param name="parCy">Новая высота</param>
    /// <param name="parUFlags">Флаги позиционирования окна</param>
    /// <returns>Если функция завершилась успешно, возвращается значение - не нуль.
    /// Если функция потерпела неудачу, возвращаемое значение - ноль. </returns>
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr parHWnd, IntPtr parHWndInsertAfter, int parX, int parY, int parCx, int parCy, uint parUFlags);


    /// <summary>
    /// Конструктор
    /// </summary>
    public ConsoleConfiguration()
    {      

      int height = LevelUtils.MATRIX_SIZE + 2 * ViewGameConsole.MARGIN_ARENA;
      int width = (height+ ViewGameConsole.X_SIDE_ARENA_EMPTY_CELL) *2;

      Console.SetWindowSize(width, height);
      Console.SetBufferSize(width, height);
      Console.CursorVisible = false;
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
      Console.OutputEncoding = Encoding.GetEncoding(866);
      //Console.OutputEncoding = Encoding.UTF8;
      Console.ForegroundColor = ConsoleColor.White;

      IntPtr handle = GetConsoleWindow();
      IntPtr sysMenu = GetSystemMenu(handle, false);

      if (handle != IntPtr.Zero)
      {

        DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
        DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
        DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);

        SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
      }
    }
  }

}
