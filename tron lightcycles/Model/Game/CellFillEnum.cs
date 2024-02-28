using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Game
{
  /// <summary>
  /// Перечисление типов заполнителей ячейки арены
  /// </summary>
  public enum CellFillEnum : int
  {
    /// <summary>
    /// Обозначение пустой клетки
    /// </summary>
    Empty = 0,

    /// <summary>
    /// Обозначение стены
    /// </summary>
    Wall = 1,

    /// <summary>
    /// Обозначение того, что в ячейке след мотоцикла пользователя
    /// </summary>
    UserTrail = 2,

    /// <summary>
    /// Обозначение того, что в ячейке голова мотоцикла пользователя
    /// </summary>
    UserHead = 3,

    /// <summary>
    /// Обозначение того, что в ячейке след первого вражеского мотоцикла
    /// </summary>
    Bot1Trail = 4,

    /// <summary>
    /// Обозначение того, что в ячейке голова первого вражеского мотоцикла
    /// </summary>
    Bot1Head = 5,

    /// <summary>
    /// Обозначение того, что в ячейке след второго вражеского мотоцикла
    /// </summary>
    Bot2Trail = 6,

    /// <summary>
    /// Обозначение того, что в ячейке голова второго вражеского мотоцикла
    /// </summary>
    Bot2Head = 7,

    /// <summary>
    /// Обозначение того, что в ячейке след третьего вражеского мотоцикла
    /// </summary>
    Bot3Trail = 8,

    /// <summary>
    /// Обозначение того, что в ячейке голова третьего вражеского мотоцикла
    /// </summary>
    Bot3Head = 9,
  }
}
