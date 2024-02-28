using ViewConsole;
using Model.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewConsole.Records
{
  /// <summary>
  /// Консольное представление окна ввода нового рекорда
  /// </summary>
  public class ViewNewRecordInputConsole : View.Records.ViewNewRecordInputBase
  {
    /// <summary>
    /// Высота меню
    /// </summary>
    private const int HEIGHT_MENU = 11;

    /// <summary>
    /// Ширина меню
    /// </summary>
    private const int WIDTH_MENU = 40;

    /// <summary>
    /// Отступ вниз
    /// </summary>
    private const int MARGIN_DOWN = 1;

    /// <summary>
    /// Ширина рамки ввода имени
    /// </summary>
    private const int WIDTH_BORDER = WIDTH_MENU / 2 + 2;

    /// <summary>
    /// Х координата рамки ввода имени
    /// </summary>
    private readonly int _borderX = Console.WindowWidth / 2 - (int)Math.Ceiling(WIDTH_BORDER / 2.0);


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parInputModel">Модель ввода нового рекорда</param>
    public ViewNewRecordInputConsole(NewRecordInputModel parInputModel) : base(parInputModel)
    {
      Init();
      Draw();
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Init()
    {
      Console.Clear();
      X = Console.WindowWidth / 2 - WIDTH_MENU / 2;
      Y = Console.WindowHeight / 2 - HEIGHT_MENU / 2;
      
    }

    /// <summary>
    /// Отобразить введенное имя
    /// </summary>
    public override void Draw()
    {
      ClearErrors();
      string timeInfo = $"{GAME_TIME_TEXT} {string.Format("{0:f2}", InputModel.Time)}";
      FastWriter.WriteToBuffer(timeInfo, Console.WindowWidth / 2 - timeInfo.Length / 2, Y);
      FastWriter.WriteToBuffer(INPUT_NAME_TEXT, Console.WindowWidth / 2 - INPUT_NAME_TEXT.Length / 2, Y + 2 * MARGIN_DOWN);

      //Рамка      
      string textBorder = "┌" + "".PadRight(WIDTH_BORDER-2, '─') + "┐";
      FastWriter.WriteToBuffer(textBorder, _borderX, Y + 4 * MARGIN_DOWN);
      FastWriter.WriteToBuffer("│", _borderX, Y + 5 * MARGIN_DOWN);
      FastWriter.WriteToBuffer("│", _borderX + WIDTH_BORDER - 1, Y + 5 * MARGIN_DOWN);
      textBorder = "└" + "".PadRight(WIDTH_BORDER - 2, '─') + "┘";
      FastWriter.WriteToBuffer(textBorder, _borderX, Y + 6 * MARGIN_DOWN);

      int inputNameX = Console.WindowWidth / 2 - (int)Math.Ceiling(InputModel.InputedName.Length/2.0);
      FastWriter.WriteToBuffer(InputModel.InputedName, inputNameX, Y + 5 * MARGIN_DOWN);            

      FastWriter.PrintBuffer();

    }

    /// <summary>
    /// Отобразить ошибки
    /// </summary>
    /// <param name="parErrors">Ошибки</param>
    public override void DrawErrors(string parErrors)
    {      
      List<string> textLines = TextWrapper.GetLinesWithWidthWrappedBySpace(parErrors.Replace("\r", "").Replace("\n", " "), WIDTH_MENU);
      for (int i = 0; i < textLines.Count; i++)
      {
        FastWriter.WriteToBuffer(textLines[i], X, Y + 8 * MARGIN_DOWN + i);
      }
      FastWriter.PrintBuffer();
    }


    /// <summary>
    /// Очистить ошибки
    /// </summary>
    protected override void ClearErrors()
    {
      FastWriter.Clear();      
    }
  }
}
