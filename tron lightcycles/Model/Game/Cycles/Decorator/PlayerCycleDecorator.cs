using Model.Game.Direction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Game.Cycles.Decorator
{
  /// <summary>
  /// Декоратор пользовательского мотоцикла
  /// </summary>
  public class PlayerCycleDecorator : BaseCycleDecorator
  {
    /// <summary>
    /// Было ли уже изменение направления в текущей ячейке головы.
    /// Если координата головы не изменилась, а в ней было изменение направления - 
    /// поворот запрещен
    /// </summary>
    private bool _wasChangedDirectionInHeadCurrentPoint;

    /// <summary>
    /// Сохраненное новое направление
    /// </summary>
    public DirectionEnum? SavedNewDirection { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parCycle">Обертываемый мотоцикл</param>
    public PlayerCycleDecorator(ConcreteCycle parCycle) : base(parCycle)
    {      
    }

    /// <summary>
    /// Не изменяет текущее направление обернутого мотоцикла, 
    /// Проверяет, не является ли parNewDirection противоположным текущему направлением.
    /// Если нет, то сохраняет parNewDirection во временной переменной.
    /// Когда обернутый мотоцикл переместится в другую клетку, (в методе Move()) 
    /// текущее направление заменится на направление из этой временной переменной
    /// </summary>
    /// <param name="parNewDirection">Новое направление</param>
    public override void CheckAndSetNewDirection(DirectionEnum parNewDirection)
    {
      DirectionEnum? oppositeDirection = DirectionUtils.OppositeDirection(CurrentDirection);
      // Если не было поворотов в текущей ячейке
      if (parNewDirection != oppositeDirection && CurrentDirection != parNewDirection && !_wasChangedDirectionInHeadCurrentPoint)
      {
        _wasChangedDirectionInHeadCurrentPoint = true;
        SavedNewDirection = parNewDirection;
      }
    }

    /// <summary>
    /// Двигать обертываемый мотоцикл в соответствии с направлением движения 
    /// (основной декорируемый метод)
    /// </summary>
    /// <param name="parTime">Время в сек для расчета перемещения</param>
    public override void Move(float parTime)
    {      
      base.Move(parTime);
      // Если мотоцикл доехал до конца клетки
      if (IsCycleMovedInNewCell && _wasChangedDirectionInHeadCurrentPoint)
      {
        // То нужно сменить направление
        _wasChangedDirectionInHeadCurrentPoint = false;        
        ((ConcreteCycle)Cycle).CurrentDirection = SavedNewDirection;
      }
    }

  }
}
