using Model.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewConsole.Game
{
  /// <summary>
  /// Класс - утилита, хранящий соответствие 
  /// между заполнителем ячейки и ее цветом в консоли
  /// </summary>
  public static class ConsoleColorByCellFillCreator
  {
    /// <summary>
    /// Получить цвет по заполнителю ячейки
    /// </summary>
    /// <param name="parCellFillEnum">Заполнитель ячейки</param>
    /// <returns>Цвет</returns>
    public static ConsoleColor GetColorByCellFill(CellFillEnum parCellFillEnum)
    {
      return parCellFillEnum switch
      {
        CellFillEnum.Empty => ConsoleColor.Black,
        CellFillEnum.Wall => ConsoleColor.Gray,
        CellFillEnum.UserTrail => ConsoleColor.Cyan,
        CellFillEnum.UserHead => ConsoleColor.White,
        CellFillEnum.Bot1Trail => ConsoleColor.DarkYellow,
        CellFillEnum.Bot1Head => ConsoleColor.White,
        CellFillEnum.Bot2Trail => ConsoleColor.DarkRed,
        CellFillEnum.Bot2Head => ConsoleColor.White,
        CellFillEnum.Bot3Trail => ConsoleColor.Green,
        CellFillEnum.Bot3Head => ConsoleColor.White,
        _ => throw new ArgumentException("Не верный заполнитель"),
      };
    }

  }
}
