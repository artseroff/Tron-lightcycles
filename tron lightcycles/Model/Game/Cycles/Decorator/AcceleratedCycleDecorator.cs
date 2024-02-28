using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Game.Cycles.Decorator
{
  /// <summary>
  /// Декоратор ускоренного мотоцикла 
  /// </summary>
  public class AcceleratedCycleDecorator : BaseCycleDecorator
  {
    /// <summary>
    /// Множитель ускорения
    /// </summary>
    private const float SPEED_UP = 2f;

    /// <summary>
    /// Длительность ускорения (в сек)
    /// </summary>
    private const int SPEED_UP_DURATION = 5;

    /// <summary>
    /// Осталось времени в сек до конца ускорения
    /// </summary>
    private float _speedUpTimeLeft = 0;

    /// <summary>
    /// Нужно ускориться
    /// </summary>
    private volatile bool _needSpeedUp;

    /// <summary>
    /// Нужно ускориться (Свойство)
    /// </summary>
    public bool NeedSpeedUp
    {
      get => _needSpeedUp;
      set
      {
        _speedUpTimeLeft = SPEED_UP_DURATION;
        _needSpeedUp = value;
      }
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parCycle">Обертываемый мотоцикл</param>
    public AcceleratedCycleDecorator(ICycle parCycle) : base(parCycle)
    {
      _needSpeedUp = false;
    }

    /// <summary>
    /// Двигать обертываемый мотоцикл в соответствии с направлением движения 
    /// (основной декорируемый метод)
    /// </summary>
    /// <param name="parTime">Время в сек для расчета перемещения</param>
    public override void Move(float parTime)
    {

      if (_needSpeedUp)
      {
        _speedUpTimeLeft -= parTime;
        if (_speedUpTimeLeft < 0)
        {
          _needSpeedUp = false;
        }
        Cycle.Move(parTime * SPEED_UP);
      }
      else
      {
        Cycle.Move(parTime);
      }
    }
  }
}
