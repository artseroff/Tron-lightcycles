using Model.Game.Direction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Game.Cycles.Decorator
{
  /// <summary>
  /// Декоратор мотоцикла
  /// </summary>
  public abstract class BaseCycleDecorator : ICycle
  {

    /// <summary>
    /// Обертываемый мотоцикл
    /// </summary>
    protected ICycle Cycle { get; private set; }

    /// <summary>
    /// Текущее направление обернутого мотоцикла
    /// </summary>
    public DirectionEnum? CurrentDirection { get => Cycle.CurrentDirection; }

    /// <summary>
    /// Координаты головы обернутого мотоцикла
    /// </summary>
    public Point Head { get => Cycle.Head; }

    /// <summary>
    /// Пройденная часть ячейки по Х обернутого мотоцикла
    /// </summary>
    public float PartCellX { get => Cycle.PartCellX; }

    /// <summary>
    /// Пройденная часть ячейки по Y обернутого мотоцикла
    /// </summary>
    public float PartCellY { get => Cycle.PartCellY; }

    /// <summary>
    /// Разбился ли обернутый мотоцикл
    /// </summary>
    public bool IsCrashed { get => Cycle.IsCrashed; set => Cycle.IsCrashed = value; }

    /// <summary>
    /// Id обернутого мотоцикла
    /// </summary>
    public int Id { get => Cycle.Id; }

    /// <summary>
    /// След - стек прошлых координат обернутого мотоцикла
    /// </summary>
    public Stack<Point> Trail
    {
      get => Cycle.Trail;
    }

    /// <summary>
    /// Мотоцикл переместился в новую клетку
    /// </summary>
    public bool IsCycleMovedInNewCell { get => Cycle.IsCycleMovedInNewCell; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parCycle">Обертываемый мотоцикл</param>
    public BaseCycleDecorator(ICycle parCycle)
    {
      Cycle = parCycle;
    }

    /// <summary>
    /// Двигать обертываемый мотоцикл в соответствии с направлением движения 
    /// (основной декорируемый метод)
    /// </summary>
    /// <param name="parTime">Время в сек для расчета перемещения</param>
    public virtual void Move(float parTime)
    {
      Cycle.Move(parTime);
    }

    /// <summary>
    /// Проверить направление (на то, чтобы не являлось противоположным и текущим)
    /// и установить его как новое текущее направление
    /// </summary>
    /// <param name="parNewDirection">Новое направление</param>
    public virtual void CheckAndSetNewDirection(DirectionEnum parNewDirection)
    {
      Cycle.CheckAndSetNewDirection(parNewDirection);
    }
  }
}
