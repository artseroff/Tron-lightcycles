using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
  /// <summary>
  /// События от модели игры
  /// </summary>
  public enum GameEvents
  {
    /// <summary>
    /// Уровень начался
    /// </summary>
    LevelStarted,

    /// <summary>
    /// Уровень закончился (кроме последнего)
    /// </summary>
    LevelEnded,

    /// <summary>
    /// Победа
    /// </summary>
    Win,

    /// <summary>
    /// Проигрыш
    /// </summary>
    Loss
  }
}
