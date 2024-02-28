using Model.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Menu
{
  /// <summary>
  /// Базовое представление пункта меню
  /// </summary>
  public abstract class ViewMenuItemBase : ViewBase
  {

    /// <summary>
    /// Модель пункта меню
    /// </summary>
    protected MenuItem MenuItem { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenuItemBase">Модель пункта меню</param>
    public ViewMenuItemBase(MenuItem parMenuItemBase)
    {
      MenuItem = parMenuItemBase;
    }
  }
}
