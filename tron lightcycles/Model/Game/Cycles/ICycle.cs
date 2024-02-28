using Model.Game.Direction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Game.Cycles
{
  /// <summary>
  /// Интерфейс мотоцикла
  /// </summary>
  public interface ICycle
  {
    /// <summary>
    /// Текущее направление мотоцикла
    /// </summary>
    public DirectionEnum? CurrentDirection { get; }

    /// <summary>
    /// Координаты головы мотоцикла
    /// </summary>
    public Point Head { get; }

    /// <summary>
    /// Пройденная часть ячейки по Х мотоцикла
    /// </summary>
    public float PartCellX { get; }

    /// <summary>
    /// Пройденная часть ячейки по Y мотоцикла
    /// </summary>
    public float PartCellY { get; }

    /// <summary>
    /// Разбился ли мотоцикл
    /// </summary>
    public bool IsCrashed { get; set; }

    /// <summary>
    /// Id мотоцикла
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// След - стек прошлых координат мотоцикла
    /// </summary>
    public Stack<Point> Trail { get; }

    /// <summary>
    /// Мотоцикл переместился в новую клетку
    /// </summary>
    public bool IsCycleMovedInNewCell { get; }

    /// <summary>
    /// Двигаться в соответствии с направлением движения 
    /// </summary>
    /// <param name="parTime">Время в сек для расчета перемещения</param>
    public void Move(float parTime);

    /// <summary>
    /// Проверить направление (на то, чтобы не являлось противоположным и текущим)
    /// и установить его как новое текущее направление
    /// </summary>
    /// <param name="parNewDirection">Новое направление</param>
    public void CheckAndSetNewDirection(DirectionEnum parNewDirection);
  }
}
