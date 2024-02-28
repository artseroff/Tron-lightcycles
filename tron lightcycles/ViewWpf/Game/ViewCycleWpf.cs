using Model.Game;
using Model.Game.Cycles;
using Model.Game.Cycles.Decorator;
using Model.Game.Direction;
using Model.Game.Level;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using View.Game;

namespace ViewWpf.Game
{
  /// <summary>
  /// Представление мотоцикла Wpf
  /// </summary>
  public class ViewCycleWpf : ViewCycleBase
  {
    /// <summary>
    /// Z - index головы
    /// </summary>
    private const int HEAD_Z_INDEX = 999;

    /// <summary>
    /// Холст арены
    /// </summary>
    private readonly Canvas _canvas;

    /// <summary>
    /// Прямоугольник головы мотоцикла
    /// </summary>
    private Rectangle _headRect;

    /// <summary>
    /// Кисть головы мотоцикла
    /// </summary>
    private readonly Brush _headBrush;

    /// <summary>
    /// Кисть пути мотоцикла 
    /// </summary>
    private readonly Brush _trailBrush;

    /// <summary>
    /// Список прямоугольников следа
    /// </summary>
    private List<Rectangle> _trailRectangles;

    /// <summary>
    /// Конуструктор
    /// </summary>
    /// <param name="parCycle">Модель мотоцикла</param>
    /// <param name="parCanvas">Холст арены</param>
    public ViewCycleWpf(ICycle parCycle, Canvas parCanvas) : base(parCycle)
    {

      _trailRectangles = new List<Rectangle>();
      _canvas = parCanvas;

      CellFillEnum headfillerValue = LevelUtils.GetCycleCellValueByCycleId(parCycle.Id, true);
      _headBrush = WpfBrushColorByCellFillCreator.GetBrushByCellFill(headfillerValue);

      CellFillEnum trailfillerValue = LevelUtils.GetCycleCellValueByCycleId(parCycle.Id, false);
      _trailBrush = WpfBrushColorByCellFillCreator.GetBrushByCellFill(trailfillerValue);


    }

    /// <summary>
    /// Убрать след мотоцикла с поля
    /// </summary>
    protected override void ClearCycleTrailOnField()
    {
      _canvas.Children.Remove(_headRect);
      foreach (Rectangle elRectangle in _trailRectangles)
      {
        _canvas.Children.Remove(elRectangle);
      }
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

      _headRect = new Rectangle();
      Panel.SetZIndex(_headRect, HEAD_Z_INDEX);
      _headRect.Fill = _headBrush;
      _headRect.Width = ViewGameWpf.ARENA_CELL_SIZE;
      _headRect.Height = ViewGameWpf.ARENA_CELL_SIZE;
      _canvas.Children.Add(_headRect);
      Canvas.SetLeft(_headRect, X * ViewGameWpf.ARENA_CELL_SIZE);
      Canvas.SetTop(_headRect, Y * ViewGameWpf.ARENA_CELL_SIZE);

      Rectangle trailRectangle = new Rectangle();
      trailRectangle.Fill = _trailBrush;
      trailRectangle.Width = ViewGameWpf.ARENA_CELL_SIZE;
      trailRectangle.Height = ViewGameWpf.ARENA_CELL_SIZE;
      _canvas.Children.Add(trailRectangle);
      Canvas.SetLeft(trailRectangle, X * ViewGameWpf.ARENA_CELL_SIZE);
      Canvas.SetTop(trailRectangle, Y * ViewGameWpf.ARENA_CELL_SIZE);
      _trailRectangles.Add(trailRectangle);
    }

    /// <summary>
    /// Отобразить частичное перемещение 
    /// внутри клетки арены
    /// </summary>
    protected override void DrawPartialMovement()
    {
      if (!_canvas.Children.Contains(_headRect))
      {
        _canvas.Children.Add(_headRect);
      }

      float newCell;
      if (Cycle.CurrentDirection is DirectionEnum.Up || Cycle.CurrentDirection is DirectionEnum.Down)
      {
        newCell = (Cycle.CurrentDirection is DirectionEnum.Up) ? Y - Cycle.PartCellY : Y + Cycle.PartCellY;
        Canvas.SetLeft(_headRect, X * ViewGameWpf.ARENA_CELL_SIZE);
        Canvas.SetTop(_headRect, newCell * ViewGameWpf.ARENA_CELL_SIZE);
      }
      else
      {
        newCell = (Cycle.CurrentDirection is DirectionEnum.Left) ? X - Cycle.PartCellX : X + Cycle.PartCellX;
        Canvas.SetLeft(_headRect, newCell * ViewGameWpf.ARENA_CELL_SIZE);
        Canvas.SetTop(_headRect, Y * ViewGameWpf.ARENA_CELL_SIZE);
      }
    }

    /// <summary>
    /// Нарисовать мотоцикл в момент
    /// перехода в новую клетку
    /// </summary>
    protected override void DrawCycleInNewCell()
    {
      X = Cycle.Head.X;
      Y = Cycle.Head.Y;
      Rectangle trailRectangle = new Rectangle();
      trailRectangle.Fill = _trailBrush;
      trailRectangle.Width = ViewGameWpf.ARENA_CELL_SIZE;
      trailRectangle.Height = ViewGameWpf.ARENA_CELL_SIZE;
      _canvas.Children.Add(trailRectangle);
      Canvas.SetLeft(trailRectangle, X * ViewGameWpf.ARENA_CELL_SIZE);
      Canvas.SetTop(trailRectangle, Y * ViewGameWpf.ARENA_CELL_SIZE);
      _trailRectangles.Add(trailRectangle);
    }
  }
}
