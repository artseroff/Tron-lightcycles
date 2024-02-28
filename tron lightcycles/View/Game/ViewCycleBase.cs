using Model.Game.Cycles;
using Model.Game.Cycles.Decorator;
using System;
using System.Collections.Generic;
using System.Text;

namespace View.Game
{
  /// <summary>
  /// Базовое представление мотоцикла
  /// </summary>
  public abstract class ViewCycleBase : ViewBase
  {
    /// <summary>
    /// Значение по умолчанию для Х и У
    /// </summary>
    protected const int DEFAULT_X_AND_Y_VALUE = -1;

    /// <summary>
    /// Модель мотоцикла
    /// </summary>
    protected ICycle Cycle { get; private set; }

    /// <summary>
    /// Разбился ли мотоцикл
    /// </summary>
    public bool IsCrashed { get => Cycle.IsCrashed; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parCycle">Модель мотоцикла</param>
    public ViewCycleBase(ICycle parCycle)
    {
      X = DEFAULT_X_AND_Y_VALUE;
      Y = DEFAULT_X_AND_Y_VALUE;
      Cycle = parCycle;
    }

    /// <summary>
    /// Нарисовать голову мотоцикла на новых координатах,
    /// заменив старые координаты на след соответствующего мотоцикла
    /// </summary>
    public override void Draw()
    {
      if (IsCrashed)
      {
        ClearCycleTrailOnField();
        return;
      }
      if (X == DEFAULT_X_AND_Y_VALUE && Y == DEFAULT_X_AND_Y_VALUE)
      {
        InitCycleOnArena();
      }
      else
      {
        DrawPartialMovement();
        if (Cycle.IsCycleMovedInNewCell)
        {
          DrawCycleInNewCell();
        }
      }
    }

    /// <summary>
    /// Инициализировать мотоцикл на арене
    /// </summary>
    protected abstract void InitCycleOnArena();

    /// <summary>
    /// Отобразить частичное перемещение 
    /// внутри клетки арены
    /// </summary>
    protected abstract void DrawPartialMovement();

    /// <summary>
    /// Нарисовать мотоцикл в момент
    /// перехода в новую клетку
    /// </summary>
    protected abstract void DrawCycleInNewCell();

    /// <summary>
    /// Убрать след мотоцикла с поля
    /// </summary>
    protected abstract void ClearCycleTrailOnField();
    

  }
}
