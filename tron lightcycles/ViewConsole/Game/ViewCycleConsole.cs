using ViewConsole;
using Model.Game;
using Model.Game.Cycles;
using Model.Game.Direction;
using Model.Game.Level;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using View.Game;

namespace ViewConsole.Game
{
  /// <summary>
  /// Консольное представление мотоцикла
  /// </summary>
  public class ViewCycleConsole : ViewCycleBase
  {
    /// <summary>
    /// Половина ячейки арены
    /// </summary>
    private const float HALF_CELL = 0.5f;

    /// <summary>
    /// Координата верхнего левого угла арены
    /// </summary>
    private readonly Model.Game.Point _arenaPoint;

    /// <summary>
    /// Цвет головы
    /// </summary>
    private ConsoleColor _headColor;

    /// <summary>
    /// Цвет следа
    /// </summary>
    private ConsoleColor _trailColor;

    /// <summary>
    /// Последняя напечатанная координата
    /// </summary>
    private Model.Game.Point _lastPrintedPoint;
 
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parCycle">Модель мотоцикла</param>
    /// <param name="parPoint">Координата верхнего левого угла арены</param>
    public ViewCycleConsole(ICycle parCycle, Model.Game.Point parPoint) : base(parCycle)
    {
      _arenaPoint = parPoint;      

      CellFillEnum headfillerValue = LevelUtils.GetCycleCellValueByCycleId(parCycle.Id, true);
      _headColor = ConsoleColorByCellFillCreator.GetColorByCellFill(headfillerValue);

      CellFillEnum trailfillerValue = LevelUtils.GetCycleCellValueByCycleId(parCycle.Id, false);
      _trailColor = ConsoleColorByCellFillCreator.GetColorByCellFill(trailfillerValue);
    }    

    /// <summary>
    /// Инициализировать мотоцикл на арене
    /// </summary>
    protected override void InitCycleOnArena()
    {
      if (Cycle.CurrentDirection is DirectionEnum.Up || Cycle.CurrentDirection is DirectionEnum.Down)
      {
        Y = (Cycle.CurrentDirection is DirectionEnum.Up) ? Cycle.Head.Y + 1 : Cycle.Head.Y - 1;
        X = Cycle.Head.X;
      }
      else
      {
        X = (Cycle.CurrentDirection is DirectionEnum.Left) ? Cycle.Head.X + 1 : Cycle.Head.X - 1;
        Y = Cycle.Head.Y;
      }
      X = X * ViewGameConsole.SCALE_X + _arenaPoint.X;
      Y += _arenaPoint.Y;
      _lastPrintedPoint = new Model.Game.Point(X, Y);

      FastWriter.WriteToBuffer(GameSymbols.ScaledFullBlock, X, Y, _headColor);
    }

    /// <summary>
    /// Отобразить частичное перемещение 
    /// внутри клетки арены
    /// </summary>
    protected override void DrawPartialMovement()
    {
      float newPointFloat;
      int newPointInt;
      if (Cycle.CurrentDirection is DirectionEnum.Up || Cycle.CurrentDirection is DirectionEnum.Down)
      {
        newPointFloat = (Cycle.CurrentDirection is DirectionEnum.Up) ? Y - Cycle.PartCellY : Y + Cycle.PartCellY;
        newPointInt = (int)Math.Round(newPointFloat);

        if (!_lastPrintedPoint.Equals(Cycle.Head) && Cycle.PartCellY > HALF_CELL)
        {

          if (Cycle.CurrentDirection is DirectionEnum.Up)
          {
            FastWriter.WriteToBuffer(GameSymbols.ScaledLowerHalfBlock, X, newPointInt, _headColor);
            FastWriter.WriteToBuffer(GameSymbols.ScaledUpperHalfBlock, X, Y, _headColor, _trailColor);
          }
          else
          {
            FastWriter.WriteToBuffer(GameSymbols.ScaledLowerHalfBlock, X, Y, _headColor, _trailColor);
            FastWriter.WriteToBuffer(GameSymbols.ScaledUpperHalfBlock, X, newPointInt, _headColor);
          }

          _lastPrintedPoint.Y = Y;
          _lastPrintedPoint.X = X;
        }
      }
      else
      {
        newPointFloat = (Cycle.CurrentDirection is DirectionEnum.Left) ? X - Cycle.PartCellX : X + Cycle.PartCellX;
        newPointInt = (int)Math.Round(newPointFloat);

        if (!_lastPrintedPoint.Equals(Cycle.Head) && Cycle.PartCellX > HALF_CELL)
        {

          FastWriter.WriteToBuffer(GameSymbols.ScaledFullBlock, newPointInt, Y, _headColor);
          int tempX = (Cycle.CurrentDirection is DirectionEnum.Left) ? X + 1 : X;
          FastWriter.WriteToBuffer(GameSymbols.FULL_BLOCK_SYMBOL.ToString(), tempX, Y, _trailColor);

          _lastPrintedPoint.X = X;
          _lastPrintedPoint.Y = Y;
        }
      }
    }

    /// <summary>
    /// Нарисовать мотоцикл в момент
    /// перехода в новую клетку
    /// </summary>
    protected override void DrawCycleInNewCell()
    {
      FastWriter.WriteToBuffer(GameSymbols.ScaledFullBlock, X, Y, _trailColor);
      X = Cycle.Head.X;
      Y = Cycle.Head.Y;
      X = X * ViewGameConsole.SCALE_X + _arenaPoint.X;
      Y += _arenaPoint.Y;
      FastWriter.WriteToBuffer(GameSymbols.ScaledFullBlock, X, Y, _headColor);
    }    

    /// <summary>
    /// Убрать след мотоцикла с поля
    /// </summary>
    protected override void ClearCycleTrailOnField()
    {
      FastWriter.WriteToBuffer(GameSymbols.ScaledFullBlock, Cycle.Head.X * ViewGameConsole.SCALE_X + _arenaPoint.X, Cycle.Head.Y + _arenaPoint.Y, ConsoleColor.Black);
      foreach (Model.Game.Point elPoint in Cycle.Trail)
      {
        FastWriter.WriteToBuffer(GameSymbols.ScaledFullBlock, elPoint.X * ViewGameConsole.SCALE_X + _arenaPoint.X, elPoint.Y + _arenaPoint.Y, ConsoleColor.Black);
      }
    }
  }
}
