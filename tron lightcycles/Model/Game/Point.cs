using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Game
{
  /// <summary>
  /// Класс точка
  /// </summary>
  public class Point
  {
    /// <summary>
    /// Координата по х
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Координата по y
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parX">Координата по х</param>
    /// <param name="parY">Координата по y</param>
    public Point(int parX, int parY)
    {
      X = parX;
      Y = parY;
    }

    /// <summary>
    /// Конструктор копирования
    /// </summary>
    /// <param name="parPoint">Координаты точки для копирования</param>
    public Point(Point parPoint)
    {
      X = parPoint.X;
      Y = parPoint.Y;
    }

    /// <summary>
    /// Конструктор без параметров
    /// </summary>
    public Point()
    {
    }

    /// <summary>
    /// Равны ли объекты
    /// </summary>
    /// <param name="parObj">Сравниваемый объект</param>
    /// <returns>Равны ли объекты</returns>
    public override bool Equals(object parObj)
    {
      return parObj is Point point &&
             X == point.X &&
             Y == point.Y;
    }

    /// <summary>
    /// Хеш-код
    /// </summary>
    /// <returns>Хеш-код</returns>
    public override int GetHashCode()
    {
      return HashCode.Combine(X, Y);
    }
  }
}
