using Model.Game.Cycles.BestPath;
using Model.Game.Direction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Game.Cycles.Decorator
{
  /// <summary>
  /// Декоратор мотоцикла с ИИ
  /// </summary>
  public class CycleWithAIDecorator : BaseCycleDecorator
  {
    /// <summary>
    /// Генератор псевдослучайных чисел
    /// </summary>
    private static readonly Random _random = new Random();

    /// <summary>
    /// Количество оставшихся клеток до смены направления
    /// (до запуска алгоритма поиска наилучшего направления)
    /// </summary>
    private int _counterRemainedCellsInCurrentDirection;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parCycle">Обертываемый мотоцикл</param>
    public CycleWithAIDecorator(ConcreteCycle parCycle) : base(parCycle)
    {
    }

    /// <summary>
    /// Двигать обертываемый мотоцикл в соответствии с направлением движения 
    /// (основной декорируемый метод)
    /// </summary>
    /// <param name="parTime">Время в сек для расчета перемещения</param>
    public override void Move(float parTime)
    {
      if (_counterRemainedCellsInCurrentDirection == 0)
      {
        PossiblePath possiblePath = BestPathFinder.FindBestPath(((ConcreteCycle)Cycle).Arena, Cycle.CurrentDirection, Cycle.Head);
        _counterRemainedCellsInCurrentDirection = _random.Next(1, (int)Math.Ceiling(possiblePath.Length / 2f) + 1);
        CheckAndSetNewDirection(possiblePath.Direction);
      }
      base.Move(parTime);
      DecrementCounterRemainedCellsIfCycleMovedInNewCell();

    }

    /// <summary>
    /// Если мотоцикл переехал в новую клетку,
    /// уменьшить счетчик клеток до смены направления
    /// </summary>
    private void DecrementCounterRemainedCellsIfCycleMovedInNewCell()
    {

      // Если мотоцикл переехал в новую ячейку арены
      if (IsCycleMovedInNewCell)
      {
        _counterRemainedCellsInCurrentDirection--;
      }
    }
  }

}
