using Model.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Menu
{
  /// <summary>
  /// Абстрактное представление меню
  /// </summary>
  public abstract class ViewMenuBase : ViewBase
  {
    /// <summary>
    /// Список пунктов меню
    /// </summary>
    private List<ViewMenuItemBase> _items = new List<ViewMenuItemBase>();

    /// <summary>
    /// Массив пунктов меню
    /// </summary>
    protected ViewMenuItemBase[] Items
    {
      get
      {
        return _items.ToArray();
      }
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenu">Циклическое меню</param>
    public ViewMenuBase(CyclicMenu parMenu)
    {
      foreach (MenuItem elMenuItem in parMenu.Items)
        _items.Add(CreateMenuItem(elMenuItem));
    }

    /// <summary>
    /// Создать представление пункта меню
    /// </summary>
    /// <param name="parItem">Модель пункта меню</param>
    /// <returns>Представление пункта меню</returns>
    protected abstract ViewMenuItemBase CreateMenuItem(MenuItem parItem);

    /// <summary>
    /// Нарисовать представление класса
    /// </summary>
    public override void Draw()
    {
      ViewMenuItemBase[] items = Items;
      foreach (ViewMenuItemBase elViewMenuItemBase in items)
      {
        elViewMenuItemBase.Draw();
      }
    }
  }
}
