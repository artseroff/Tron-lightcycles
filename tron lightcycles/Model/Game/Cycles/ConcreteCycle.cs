using System;
using System.Collections.Generic;
using System.Text;
using Model.Game.Direction;
using Model.Game.Level;

namespace Model.Game.Cycles
{
  /// <summary>
  /// Мотоцикл
  /// </summary>
  public class ConcreteCycle : ICycle
  {

    /// <summary>
    /// Скорость перемещения (клеток арены в секунду)
    /// </summary>
    private const float SPEED = 10f;

    /// <summary>
    /// Была ли проверка аварии в текущей ячейке головы
    /// </summary>
    private bool _wasCheckedCrashInHeadCurrentPoint = false;

    /// <summary>
    /// Матрица арены
    /// </summary>
    public Arena Arena { get; private set; }

    /// <summary>
    /// Разбился ли мотоцикл
    /// </summary>
    public bool IsCrashed { get; set; }

    /// <summary>
    /// Номер мотоцикла
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Координаты головы
    /// </summary>
    public Point Head { get; private set; }

    /// <summary>
    /// След - стек прошлых координат
    /// </summary>
    public Stack<Point> Trail { get; } = new Stack<Point>();

    /// <summary>
    /// Пройденная часть ячейки по Х
    /// </summary>
    public float PartCellX { get; private set; } = 0;

    /// <summary>
    /// Пройденная часть ячейки по Y
    /// </summary>
    public float PartCellY { get; private set; } = 0;

    /// <summary>
    /// Текущее направление
    /// </summary>
    public DirectionEnum? CurrentDirection { get; set; }

    /// <summary>
    /// Мотоцикл переместился в новую клетку
    /// </summary>
    public bool IsCycleMovedInNewCell { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parId">Номер</param>
    /// <param name="parPoint">Начальные координаты</param>
    /// <param name="parArena">Арена</param>
    /// <param name="parDirectionEnum">Направление</param>
    public ConcreteCycle(int parId, Point parPoint, Arena parArena, DirectionEnum? parDirectionEnum = null)
    {
      if (parId < 1)
      {
        throw new ArgumentException("Id должно быть больше 0");
      }
      if (parArena == null)
      {
        throw new ArgumentNullException("Арена не должна быть null");
      }
      if (parPoint == null)
      {
        throw new ArgumentNullException("Координата головы не должна быть null");
      }
      if (parArena.IsOutOfBounds(parPoint.X, parPoint.Y))
      {
        throw new ArgumentException("Координаты вне матрицы арены");
      }
     
      Id = parId;
      Arena = parArena;            
      Head = parPoint;
      CurrentDirection = parDirectionEnum;
    }

    /// <summary>
    /// Проверить направление (на то, чтобы не являлось противоположным и текущим)
    /// и установить его как новое текущее направление
    /// </summary>
    /// <param name="parNewDirection">Новое направление</param>
    public void CheckAndSetNewDirection(DirectionEnum parNewDirection)
    {
      DirectionEnum? oppositeDirection = DirectionUtils.OppositeDirection(CurrentDirection);
      if (parNewDirection != oppositeDirection && CurrentDirection != parNewDirection)
      {
        CurrentDirection = parNewDirection;
      }
    }

    /// <summary>
    /// Двигаться в соответствии с направлением движения 
    /// </summary>
    /// <param name="parTime">Время в сек для расчета перемещения</param>
    public void Move(float parTime)
    {
      if (IsCrashed)
      {
        return;
      }
      if (CurrentDirection == null)
      {
        throw new InvalidOperationException("CurrentDirection должно быть присвоено" +
          " значение, перед вызовом Move()");
      }
      MoveWithDeltaAndCheckCanDirectionChange(parTime * SPEED);
    }

    /// <summary>
    /// Проверяет не разобъется ли мотоцикл в следующей клетке,
    /// двигаясь в текущем направлении
    /// </summary>
    /// <returns>True, если разобъется, иначе false</returns>
    private bool CheckIsCrashed()
    {
      int newCell;
      // Если тут клетка занята, то мотоцикл разбился
      if (CurrentDirection is DirectionEnum.Up || CurrentDirection is DirectionEnum.Down)
      {
        newCell = (CurrentDirection is DirectionEnum.Up) ? Head.Y - 1 : Head.Y + 1;
        IsCrashed = Arena.IsNotEmptyOrOutOfBoundsCell(Head.X, newCell);
      }
      else
      {
        newCell = (CurrentDirection is DirectionEnum.Left) ? Head.X - 1 : Head.X + 1;
        IsCrashed = Arena.IsNotEmptyOrOutOfBoundsCell(newCell, Head.Y);
      }

      if (IsCrashed)
      {
        Arena.ClearCycleTrailOnField(Head, Trail);
      }
      return IsCrashed;
    }

    /// <summary>
    /// Передвинуть мотоцикл в соответствии с направлением движения на перемещение
    /// и проверить можно ли сменить направление
    /// </summary>
    /// <param name="parDelta">Перемещение</param>
    private void MoveWithDeltaAndCheckCanDirectionChange(float parDelta)
    {
      // Тут координата головы - координата текущей частично заполненной ячейки
      IsCycleMovedInNewCell = false;
      if (CurrentDirection is DirectionEnum.Up || CurrentDirection is DirectionEnum.Down)
      {
        PartCellY += parDelta;
        // Проверка аварии только при первом перемещении в новой клетке
        if (!_wasCheckedCrashInHeadCurrentPoint)
        {
          _wasCheckedCrashInHeadCurrentPoint = true;
          if (CheckIsCrashed())
          {
            return;
          }
          CycleHeadMovedInNewCell();
        }

        // Если перемещение в клетке составило больше величины клетки 
        if (PartCellY + GameModel.EPS >= 1)
        {
          PartCellY -= 1;

          //CycleHeadMovedInNewCell();
          IsCycleMovedInNewCell = true;
          _wasCheckedCrashInHeadCurrentPoint = false;
        }

      }
      else
      {
        PartCellX += parDelta;
        // Проверка аварии только при первом перемещении в новой клетке
        if (!_wasCheckedCrashInHeadCurrentPoint)
        {
          _wasCheckedCrashInHeadCurrentPoint = true;
          if (CheckIsCrashed())
          {
            return;
          }
          CycleHeadMovedInNewCell();
        }
        // Если перемещение в клетке составило больше величины клетки 
        if (PartCellX + GameModel.EPS >= 1)
        {
          PartCellX -= 1;
          IsCycleMovedInNewCell = true;
          _wasCheckedCrashInHeadCurrentPoint = false;
        }
      }
    }

    /// <summary>
    /// Голова мотоцикла переместилась в новую клетку
    /// </summary>
    private void CycleHeadMovedInNewCell()
    {

      // Ячейку на прошлых координатах головы в матрице надо пометить как след мотоцикла с id     
      Arena.Matrix[Head.Y][Head.X] = (int)LevelUtils.GetCycleCellValueByCycleId(Id);
      // Добавить старую голову в след
      AddHeadToTrail();

      // Переместить голову на новые координаты
      if (CurrentDirection is DirectionEnum.Up || CurrentDirection is DirectionEnum.Down)
      {
        Head.Y = (CurrentDirection is DirectionEnum.Up) ? Head.Y - 1 : Head.Y + 1;
        // в новой клетке перемещенение 0
        PartCellX = 0;
      }
      else
      {
        Head.X = (CurrentDirection is DirectionEnum.Left) ? Head.X - 1 : Head.X + 1;
        PartCellY = 0;
      }
      // Ячейку на новых координатах головы в матрице пометить как голова  мотоцикла с id
      Arena.Matrix[Head.Y][Head.X] = (int)LevelUtils.GetCycleCellValueByCycleId(Id, true);
    }

    /// <summary>
    /// Добавить голову в след
    /// </summary>
    private void AddHeadToTrail()
    {
      Trail.Push(new Point(Head));
    }
  }
}
