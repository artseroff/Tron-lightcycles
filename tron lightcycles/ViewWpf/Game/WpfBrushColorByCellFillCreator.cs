using Model.Game;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ViewWpf.Game
{
  /// <summary>
  /// Класс - утилита, хранящий соответствие 
  /// между заполнителем ячейки и ее цветом
  /// для кисти в wpf
  /// </summary>
  public static class WpfBrushColorByCellFillCreator
  {
    /// <summary>
    /// Белая кисть
    /// </summary>
    private static readonly Brush whiteBrush = new SolidColorBrush(Colors.White);

    /// <summary>
    /// Черная кисть
    /// </summary>
    private static readonly Brush blackBrush = new SolidColorBrush(Colors.Black);

    /// <summary>
    /// Серая кисть
    /// </summary>
    private static readonly Brush greyBrush = new SolidColorBrush(Colors.LightGray);

    /// <summary>
    /// Кисть мотоцикла пользователя
    /// </summary>
    private static readonly Brush userCycleBrush = new SolidColorBrush(Colors.Cyan);

    /// <summary>
    /// Кисть мотоцикла бота 1
    /// </summary>
    private static readonly Brush bot1Brush = new SolidColorBrush(Colors.Yellow);

    /// <summary>
    /// Кисть мотоцикла бота 2
    /// </summary>
    private static readonly Brush bot2Brush = new SolidColorBrush(Colors.Red);

    /// <summary>
    /// Кисть мотоцикла бота 3
    /// </summary>
    private static readonly Brush bot3Brush = new SolidColorBrush(Colors.Green);

    /// <summary>
    /// Получить кисть по заполнителю ячейки
    /// </summary>
    /// <param name="parCellFillEnum">Заполнитель ячейки</param>
    /// <returns>Кисть</returns>
    public static Brush GetBrushByCellFill(CellFillEnum parCellFillEnum)
    {
      return parCellFillEnum switch
      {
        CellFillEnum.Empty => blackBrush,
        CellFillEnum.Wall => greyBrush,
        CellFillEnum.UserTrail => userCycleBrush,
        CellFillEnum.UserHead => whiteBrush,
        CellFillEnum.Bot1Trail => bot1Brush,
        CellFillEnum.Bot1Head => whiteBrush,
        CellFillEnum.Bot2Trail => bot2Brush,
        CellFillEnum.Bot2Head => whiteBrush,
        CellFillEnum.Bot3Trail => bot3Brush,
        CellFillEnum.Bot3Head => whiteBrush,
        _ => throw new ArgumentException("Не верный заполнитель"),
      };
    }
  }
}
