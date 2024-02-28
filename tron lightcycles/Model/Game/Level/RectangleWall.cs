using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Game.Level
{
  /// <summary>
  /// Прямоугольная стена
  /// </summary>
  public class RectangleWall
  {
    /// <summary>
    /// Координата верхней левой точки
    /// </summary>
    public Point LeftTopPoint { get; set; }

    /// <summary>
    /// Ширина стены в клетках арены
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Высота стены в клетках арены
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parLeftTopPoint">Координата верхней левой точки</param>
    /// <param name="parWidth">Ширина стены </param>
    /// <param name="parHeight">Высота стены</param>
    public RectangleWall(Point parLeftTopPoint, int parWidth, int parHeight)
    {
      LeftTopPoint = parLeftTopPoint;
      Width = parWidth;
      Height = parHeight;
    }

    /// <summary>
    /// Конструктор без параметров
    /// </summary>
    public RectangleWall()
    {

    }
  }
}
