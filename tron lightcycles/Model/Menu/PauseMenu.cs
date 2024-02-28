using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Menu
{
  /// <summary>
  /// Меню паузы
  /// </summary>
  public class PauseMenu : CyclicMenu
  {    

    /// <summary>
    /// Конструктор
    /// </summary>
    public PauseMenu()
    {
      AddMenuItem(new MenuItem((int)PauseMenuIds.Yes, "Да"));
      AddMenuItem(new MenuItem((int)PauseMenuIds.No, "Нет"));
      FocusNextItem();
      FocusNextItem();
    }
  }
}
