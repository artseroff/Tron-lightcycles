using Model.Game.Direction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Game.Cycles.BestPath
{
  /// <summary>
  /// Класс возможный путь
  /// </summary>
  public class PossiblePath
  {
    /// <summary>
    /// Величина пути
    /// </summary>
    public int Length { get; private set; }

    /// <summary>
    /// Направление
    /// </summary>
    public DirectionEnum Direction { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parLength">Величина пути</param>
    /// <param name="parDirection">Направление</param>
    public PossiblePath(int parLength, DirectionEnum parDirection)
    {
      Length = parLength;
      Direction = parDirection;
    }
  }
}
