using Model.Game.Direction;
using Model.Game.Level;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Game.Cycles.BestPath
{
  /// <summary>
  /// Искатель наилучшего пути
  /// </summary>
  public static class BestPathFinder
  {
    /// <summary>
    /// Наилуший путь в тупике со всех сторон
    /// </summary>
    private static readonly PossiblePath _deadEndPath = new PossiblePath(1, DirectionEnum.Up);

    /// <summary>
    /// Свойство наилуший путь в тупике со всех сторон
    /// </summary>
    public static PossiblePath DeadEndPath { get => _deadEndPath; }

    /// <summary>
    /// Ищет самый длинный и наиболее свободный путь
    /// </summary>
    /// <param name="parArena">Арена</param>
    /// <param name="parDirection">Текущее направление мотоцикла</param>
    /// <param name="parPoint">Точка от которой ищется путь</param>
    /// <returns>Объект возможный путь,
    /// содержащий величину самого длинного пути без препятствий 
    /// и направление этого пути</returns>
    public static PossiblePath FindBestPath(Arena parArena, DirectionEnum? parDirection, Point parPoint)
    {
      List<PossiblePath> possiblePaths = new List<PossiblePath>();
      DirectionEnum? oppositeDirection = DirectionUtils.OppositeDirection(parDirection);

      if (DirectionEnum.Up != oppositeDirection)
      {
        int i = parPoint.Y - 1;
        while (i >= 0)
        {
          if (parArena.Matrix[i][parPoint.X] != (int)CellFillEnum.Empty || i == 0)
          {
            int length = parPoint.Y - (i + 1);
            if (length > 0)
            {
              if (i == 0 && parArena.Matrix[i][parPoint.X] == (int)CellFillEnum.Empty)
              {
                length++;
              }
              possiblePaths.Add(new PossiblePath(length, DirectionEnum.Up));
            }
            break;
          }
          i--;
        }
      }
      if (DirectionEnum.Right != oppositeDirection)
      {
        int i = parPoint.X + 1;
        while (i < LevelUtils.MATRIX_SIZE)
        {
          if (parArena.Matrix[parPoint.Y][i] != (int)CellFillEnum.Empty || i == LevelUtils.MATRIX_SIZE - 1)
          {
            int length = i - 1 - parPoint.X;

            if (length > 0)
            {
              if (i == LevelUtils.MATRIX_SIZE - 1 && (parArena.Matrix[parPoint.Y][i] == (int)CellFillEnum.Empty))
              {
                length++;
              }
              possiblePaths.Add(new PossiblePath(length, DirectionEnum.Right));
            }
            break;
          }
          i++;
        }
      }
      if (DirectionEnum.Down != oppositeDirection)
      {
        int i = parPoint.Y + 1;
        while (i < LevelUtils.MATRIX_SIZE)
        {
          if (parArena.Matrix[i][parPoint.X] != (int)CellFillEnum.Empty || i == LevelUtils.MATRIX_SIZE - 1)
          {
            int length = i - 1 - parPoint.Y;

            if (length > 0)
            {
              if (i == LevelUtils.MATRIX_SIZE - 1 && parArena.Matrix[i][parPoint.X] == (int)CellFillEnum.Empty)
              {
                length++;
              }
              possiblePaths.Add(new PossiblePath(length, DirectionEnum.Down));
            }
            break;
          }
          i++;
        }
      }
      if (DirectionEnum.Left != oppositeDirection)
      {
        int i = parPoint.X - 1;
        while (i >= 0)
        {
          if (parArena.Matrix[parPoint.Y][i] != (int)CellFillEnum.Empty || i == 0)
          {
            int length = parPoint.X - (i + 1);

            if (length > 0)
            {
              if (i == 0 && parArena.Matrix[parPoint.Y][i] == (int)CellFillEnum.Empty)
              {
                length++;
              }
              possiblePaths.Add(new PossiblePath(length, DirectionEnum.Left));
            }
            break;
          }
          i--;
        }
      }

      // Если в тупике и со всех сторон
      // максимально свободный путь составляет 0
      if (possiblePaths.Count == 0)
      {
        return _deadEndPath;
      }
      possiblePaths.Sort((parPath1, parPath2) => -parPath1.Length.CompareTo(parPath2.Length));

      return possiblePaths[0];
    }
  }
}

