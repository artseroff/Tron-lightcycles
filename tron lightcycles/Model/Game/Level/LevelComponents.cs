using Model.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Game.Level
{
  /// <summary>
  /// Класс, описывающий компоненты уровня
  /// </summary>
  public class LevelComponents
  {
    /// <summary>
    /// Список координат голов мотоциклов
    /// </summary>
    public List<Point> CycleHeads { get; set; } = new List<Point>();

    /// <summary>
    /// Список прямоугольных стен
    /// </summary>
    public List<RectangleWall> RectangleWalls { get; set; } = new List<RectangleWall>();

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parCycleHeads">Список координат голов мотоциклов</param>
    /// <param name="parRectangleWalls">Список прямоугольных стен</param>
    public LevelComponents(List<Point> parCycleHeads, List<RectangleWall> parRectangleWalls)
    {
      CycleHeads = parCycleHeads;
      RectangleWalls = parRectangleWalls;
    }

    /// <summary>
    /// Конструктор без параметров
    /// </summary>
    public LevelComponents()
    {
    }
  }
}
