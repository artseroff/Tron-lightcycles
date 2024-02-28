using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Menu
{
  /// <summary>
  /// Статус пункта меню
  /// </summary>
  public enum MenuItemStatuses : int
  {
    /// <summary>
    /// Нажат
    /// </summary>
    Selected,

    /// <summary>
    /// В фокусе
    /// </summary>
    Focused,

    /// <summary>
    /// Никакой
    /// </summary>
    None
  }
}
