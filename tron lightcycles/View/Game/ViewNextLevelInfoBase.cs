using System;
using System.Collections.Generic;
using System.Text;

namespace View.Game
{
  /// <summary>
  /// Представление перехода на следующий уровень
  /// </summary>
  public abstract class ViewNextLevelInfoBase : ViewBase
  {
    /// <summary>
    /// Текст "уровень"
    /// </summary>
    protected const string LEVEL_TEXT = "Уровень";

    /// <summary>
    /// Номер уровня
    /// </summary>
    protected int Level { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parLevel">Номер уровня</param>
    public ViewNextLevelInfoBase(int parLevel)
    {
      Level = parLevel;
    }
  }
}
