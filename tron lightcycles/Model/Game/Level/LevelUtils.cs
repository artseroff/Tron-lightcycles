using Model.Game.Cycles;
using Model.Game.Cycles.Decorator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Model.Game.Level
{
  /// <summary>
  /// Считыватель уровня из файла в Properties
  /// </summary>
  public static class LevelUtils
  {
    /// <summary>
    /// Размерность матрицы
    /// </summary>
    public const int MATRIX_SIZE = 39;

    /// <summary>
    /// Получить значение заполнителя для ячейки с мотоциклом по его id 
    /// </summary>
    /// <param name="parId">Id мотоцикла (отсчет с 1)</param>
    /// <param name="parIsHead">Значение для головы?</param>
    /// <returns>Значение заполнителя</returns>
    public static CellFillEnum GetCycleCellValueByCycleId(int parId, bool parIsHead = false)
    {
      //return parId * 2 + (parIsHead ? 1 : 0);
      if (parIsHead)
      {
        return parId switch
        {
          1 => CellFillEnum.UserHead,
          2 => CellFillEnum.Bot1Head,
          3 => CellFillEnum.Bot2Head,
          4 => CellFillEnum.Bot3Head,
          _ => throw new ArgumentException("Неверный Id мотоцикла"),
        };
      }
      else
      {
        return parId switch
        {
          1 => CellFillEnum.UserTrail,
          2 => CellFillEnum.Bot1Trail,
          3 => CellFillEnum.Bot2Trail,
          4 => CellFillEnum.Bot3Trail,
          _ => throw new ArgumentException("Неверный Id мотоцикла"),
        };
      }
    }

    /// <summary>
    /// Получить Id мотоцикла по заполнителю ячейки
    /// </summary>
    /// <param name="parCellFillEnum">Заполнитель ячейки</param>
    /// <returns>Id мотоцикла</returns>
    public static int GetCycleIdByCellFiller(CellFillEnum parCellFillEnum)
    {
      return parCellFillEnum switch
      {
        CellFillEnum.Empty => 0,
        CellFillEnum.Wall => 0,
        CellFillEnum.UserTrail => 1,
        CellFillEnum.UserHead => 1,
        CellFillEnum.Bot1Trail => 2,
        CellFillEnum.Bot1Head => 2,
        CellFillEnum.Bot2Trail => 3,
        CellFillEnum.Bot2Head => 3,
        CellFillEnum.Bot3Trail => 4,
        CellFillEnum.Bot3Head => 4,
        _ => throw new ArgumentException("Неверный заполнитель ячейки"),
      };
    }

    /// <summary>
    /// Проверяет, есть ли в этой клетке чей-то путь
    /// </summary>
    /// <param name="parCellFillEnum">Заполнитель ячейки</param>
    /// <returns>True, если в клетке путь, иначе false</returns>
    public static bool IsTrailCell(CellFillEnum parCellFillEnum)
    {
      return parCellFillEnum is CellFillEnum.UserTrail
        || parCellFillEnum is CellFillEnum.Bot1Trail
        || parCellFillEnum is CellFillEnum.Bot2Trail
        || parCellFillEnum is CellFillEnum.Bot3Trail;
    }

    /// <summary>
    /// Проверяет, занята ли эта клетка головой какого-то мотоцикла
    /// </summary>
    /// <param name="parCellFillEnum">Заполнитель ячейки</param>
    /// <returns>True, если в клетке голова мотоцикла, иначе false</returns>
    public static bool IsHeadCell(CellFillEnum parCellFillEnum)
    {
      return parCellFillEnum is CellFillEnum.UserHead
        || parCellFillEnum is CellFillEnum.Bot1Head
        || parCellFillEnum is CellFillEnum.Bot2Head
        || parCellFillEnum is CellFillEnum.Bot3Head;
    }

    /// <summary>
    /// Формирует арену и список мотоциклов для текущего уровня
    /// </summary>
    /// <param name="parLevel">Уровень</param>
    /// <param name="outArena">Арена</param>
    /// <param name="outCycles">Список мотоциклов</param>
    public static void FormArenaAndCyclesFromFile(int parLevel, out Arena outArena, out List<ICycle> outCycles)
    {
      int[][] matrix = InitMatrix();
      outCycles = new List<ICycle>();
      outArena = new Arena(matrix);

      LevelComponents levelComponents = JsonSerializer.Deserialize<LevelComponents>(Properties.Resources.ResourceManager.GetString($"Level{parLevel}"));           

      int idCycles = 1;
      if (parLevel == 2)
      {
        // Для 3 уровня задается неожиданное начальное направление пользователя
        outCycles.Add(new AcceleratedCycleDecorator(new PlayerCycleDecorator(new ConcreteCycle(idCycles++, levelComponents.CycleHeads[idCycles - 2], outArena, Direction.DirectionEnum.Right))));
       }
      else
      {
        outCycles.Add(new AcceleratedCycleDecorator(new PlayerCycleDecorator(new ConcreteCycle(idCycles++, levelComponents.CycleHeads[idCycles - 2], outArena, Direction.DirectionEnum.Up))));        
      }
      // idCycles - 2, так как -1 из-за того что айди уже увеличен через ++ и еще один -1 из-за того, что индексация с 0      
      outCycles.Add(new CycleWithAIDecorator(new ConcreteCycle(idCycles++, levelComponents.CycleHeads[idCycles - 2], outArena)));
      outCycles.Add(new CycleWithAIDecorator(new ConcreteCycle(idCycles++, levelComponents.CycleHeads[idCycles - 2], outArena)));
      outCycles.Add(new CycleWithAIDecorator(new ConcreteCycle(idCycles++, levelComponents.CycleHeads[idCycles - 2], outArena)));


      foreach (RectangleWall elRectangleWall in levelComponents.RectangleWalls)
      {
        for (int i = elRectangleWall.LeftTopPoint.Y; i < elRectangleWall.LeftTopPoint.Y + elRectangleWall.Height; i++)
        {
          for (int j = elRectangleWall.LeftTopPoint.X; j < elRectangleWall.LeftTopPoint.X + elRectangleWall.Width; j++)
          {
            matrix[i][j] = (int)CellFillEnum.Wall;
          }
        }
      }

    }

    /// <summary>
    /// Инициализировать целочисленную матрицу размером MATRIX_SIZE*MATRIX_SIZE
    /// </summary>
    /// <returns>Целочисленная матрица размером MATRIX_SIZE*MATRIX_SIZE</returns>
    private static int[][] InitMatrix()
    {
      int[][] matrix = new int[MATRIX_SIZE][];
      for (int i = 0; i < MATRIX_SIZE; i++)
      {
        matrix[i] = new int[MATRIX_SIZE];
      }
      return matrix;
    }
  }
}
