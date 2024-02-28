using Model.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Records;

namespace ViewConsole.Records
{

  /// <summary>
  /// Представление таблицы рекордов
  /// </summary>
  public class ViewTableRecordsConsole : ViewTableRecordsBase
  {

    /// <summary>
    /// Ширина ячейки
    /// </summary>
    private const int WIDTH_CELL = 25;

    /// <summary>
    /// Текущее количество напечатанных строк
    /// </summary>
    private int _rowCounter = 0;

    /// <summary>
    /// Конструктор
    /// </summary>
    public ViewTableRecordsConsole()
    {
      if (Records.Count == 0)
      {
        DrawTitleNoRecords();
      }
      else
      {
        Init();
        Draw();
      }

    }

    /// <summary>
    /// Нарисовать заголовок об отсутствии рекордов
    /// </summary>
    protected override void DrawTitleNoRecords()
    {      
      Width = NO_RECORDS.Length;    
      X = Console.BufferWidth / 2 - Width / 2;
      Y = Console.BufferHeight / 2;

      Console.Clear();
      Console.SetCursorPosition(X, Y);
      Console.Write(NO_RECORDS);
    }

    /// <summary>
    /// Инциализация
    /// </summary>
    public void Init()
    {

      // 2 вертикальных | по бокам, сивмол между ячейками и 2 ячейки
      Width = 2 + 1 + 2 * WIDTH_CELL;
      // строка шапки и подвала, строка с наименованиями столцбов, для каждого рекорда 2 строки
      // минус 1, так как у последнего рекорда нижняя полоска не печатается
      Height = 2 + 1 + 2 * Records.Count - 1;
      X = Console.BufferWidth / 2 - Width / 2;
      Y = Console.BufferHeight / 2 - Height / 2;
    }

    /// <summary>
    /// Нарисовать представление класса
    /// </summary>
    public override void Draw()
    {
      Console.Clear();
      Console.SetCursorPosition(X, Y);
      DrawRecordsTable();
    }

    /// <summary>
    /// Нарисовать таблицу рекордов
    /// </summary>
    private void DrawRecordsTable()
    {
      DrawTableHeader();

      DrawTableRow(LEFT_COL_NAME, RIGHT_COL_NAME);
      foreach (Record elRecord in Records)
      {
        DrawTableRow(elRecord.Name, string.Format("{0:f2}",elRecord.Time));
      }
      DrawTableFooter();

    }

    /// <summary>
    /// Нарисовать заголовок таблицы рекордов
    /// </summary>
    private void DrawTableHeader()
    {
      Console.SetCursorPosition(X, Y + _rowCounter++);
      string text = "┌" + "".PadRight(WIDTH_CELL, '─') + '┬' + "".PadRight(WIDTH_CELL, '─') + "┐";
      Console.Write(text);
    }

    /// <summary>
    /// Нарисовать строку таблицы
    /// </summary>
    /// <param name="parName">Имя игрока</param>
    /// <param name="parTime">Время игры</param>
    private void DrawTableRow(string parName, string parTime)
    {
      Console.SetCursorPosition(X, Y + _rowCounter++);
      string text = "│" + parName.PadLeft(parName.Length + (WIDTH_CELL - parName.Length) / 2, ' ').PadRight(WIDTH_CELL, ' ') + "│";
      text += parTime.PadLeft(parTime.Length + (WIDTH_CELL - parTime.Length) / 2, ' ').PadRight(WIDTH_CELL, ' ') + "│";
      Console.Write(text);

      if (_rowCounter != Height)
      {
        Console.SetCursorPosition(X, Y + _rowCounter++);
        text = "├" + "".PadRight(WIDTH_CELL, '─') + '┼' + "".PadRight(WIDTH_CELL, '─') + "┤";
        Console.Write(text);
      }

    }

    /// <summary>
    /// Нарисовать подвал таблицы рекордов
    /// </summary>
    private void DrawTableFooter()
    {
      Console.SetCursorPosition(X, Y + _rowCounter++);
      string text = "└" + "".PadRight(WIDTH_CELL, '─') + '┴' + "".PadRight(WIDTH_CELL, '─') + "┘";
      Console.Write(text);
    }

  }
}
