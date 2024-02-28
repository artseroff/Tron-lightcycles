using Model.Game.Cycles;
using Model.Game.Cycles.Decorator;
using Model.Game.Level;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Game
{

  /// <summary>
  /// Игровая арена
  /// </summary>
  public class Arena
  {

    /// <summary>
    /// Делегат события аварии голову в голову двух мотоциклов.
    /// Помечает того, кто уже приехал в клетку как разбитый.
    /// Мотоцикл, Id которого выше, то есть того, который приехал
    /// приехал позже (не по времени, а по порядку в списке) 
    /// помечается как разбитый после вызова IsEmptyCell() для него в Move()
    /// </summary>
    /// <param name="parCycleId">Id мотоцикла, который
    /// первым приехал в клетку</param>
    public delegate void dOnCycleHeadsCrashed(int parCycleId);

    /// <summary>
    /// Событие аварии голову в голову двух мотоциклов,
    /// помечает того, кто уже приехал в клетку как разбитый
    /// </summary>
    public event dOnCycleHeadsCrashed OnCycleHeadsCrashed;

    /// <summary>
    /// Матрица арены
    /// </summary>
    public int[][] Matrix { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMatrix">Матрица арены</param>
    public Arena(int[][] parMatrix)
    {
      Matrix = parMatrix;
    }


    /// <summary>
    /// Занятая ли ячейка и не выехал ли мотоцикл за арену
    /// </summary>
    /// <param name="parX">Координаты ячейки по Х</param>
    /// <param name="parY">Координаты ячейки по Y</param>
    /// <returns>true - если занята ли, иначе false</returns>
    public bool IsNotEmptyOrOutOfBoundsCell(int parX, int parY)
    {
      if (IsOutOfBounds(parX, parY))
      {
        return true;
      }
      int cellValue = Matrix[parY][parX];
      if (LevelUtils.IsHeadCell((CellFillEnum)cellValue))
      {
        int cycleId = LevelUtils.GetCycleIdByCellFiller((CellFillEnum)cellValue);
        OnCycleHeadsCrashed?.Invoke(cycleId);
      }
      return cellValue != (int)CellFillEnum.Empty;
    }

    /// <summary>
    /// Проверяет переданные координаты находятся
    /// ли за границей матрицы арены
    /// </summary>
    /// <param name="parX">Координаты ячейки по Х</param>
    /// <param name="parY">Координаты ячейки по Y</param>
    /// <returns>true - если координаты находятся
    /// ли за границей матрицы арены, иначе false</returns>
    public bool IsOutOfBounds(int parX, int parY)
    {
      return parY < 0 || parY >= LevelUtils.MATRIX_SIZE || parX < 0 || parX >= LevelUtils.MATRIX_SIZE;
    }

    /// <summary>
    /// Убрать координаты следа мотоцикла с поля
    /// </summary>
    /// <param name="parHead">Координаты головы</param>
    /// <param name="parTrail">След</param>
    public void ClearCycleTrailOnField(Point parHead, Stack<Point> parTrail)
    {
      Matrix[parHead.Y][parHead.X] = (int)CellFillEnum.Empty;
      foreach (Point point in parTrail)
      {
        Matrix[point.Y][point.X] = (int)CellFillEnum.Empty;
      }
    }

  }
}
