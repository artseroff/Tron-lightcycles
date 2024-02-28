using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Menu
{
  /// <summary>
  /// Циклическое меню
  /// </summary>
  public class CyclicMenu
  {
    /// <summary>
    /// Пункты меню
    /// </summary>
    private Dictionary<int, MenuItem> _items = new Dictionary<int, MenuItem>();

    /// <summary>
    /// Упорядоченные пункты меню (фактический номер в меню)
    /// </summary>
    private Dictionary<int, MenuItem> _orderedItems = new Dictionary<int, MenuItem>();

    /// <summary>
    /// Последний выбранный пункт меню
    /// </summary>
    private MenuItem _lastSelectedItem = null;

    /// <summary>
    /// Индекс выбранного элемента
    /// </summary>
    private int _selectedItemIndex = -1;

    /// <summary>
    /// Массив пунктов меню
    /// </summary>
    public MenuItem[] Items
    {
      get
      {
        return _items.Values.ToArray();
      }
    }

    /// <summary>
    /// Получить пункт меню по Id в перечислении
    /// </summary>
    /// <param name="parId">Id в перечислении</param>
    /// <returns>Пункт меню</returns>
    public MenuItem this[int parId]
    {
      get
      {
        return _items[parId];
      }
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public CyclicMenu()
    {
    }

    /// <summary>
    /// Добавить пункт меню
    /// </summary>
    /// <param name="parMenuItem">Модель пункта меню</param>
    public void AddMenuItem(MenuItem parMenuItem)
    {
      _items.Add(parMenuItem.Id, parMenuItem);
      _orderedItems.Add(_orderedItems.Count, parMenuItem);
    }

    /// <summary>
    /// Установить статус пункта меню
    /// </summary>
    /// <param name="parId">Номер пункта меню</param>
    /// <param name="parStatus">Статус пункта меню</param>
    public void SetMenuItemStatus(int parId, MenuItemStatuses parStatus)
    {
      MenuItem menuItem = _items[parId];
      if (parStatus == MenuItemStatuses.Focused || parStatus == MenuItemStatuses.Selected)
      {
        if (_lastSelectedItem != null)
          _lastSelectedItem.Status = MenuItemStatuses.None;
        _lastSelectedItem = menuItem;
      }
      menuItem.Status = parStatus;
    }

    /// <summary>
    /// Перейти по выбранному пункту меню
    /// </summary>
    public void EnterSelectedItem()
    {
      if (_selectedItemIndex == -1)
        return;
      SetMenuItemStatus(_orderedItems[_selectedItemIndex].Id, MenuItemStatuses.Selected);
    }

    /// <summary>
    /// Сфокусироваться на следующем пункте
    /// </summary>
    public void FocusNextItem()
    {
      if (_selectedItemIndex == -1 || _selectedItemIndex == _orderedItems.Count - 1)
        _selectedItemIndex = 0;
      else
        _selectedItemIndex++;

      SetMenuItemStatus(_orderedItems[_selectedItemIndex].Id, MenuItemStatuses.Focused);
    }

    /// <summary>
    /// Сфокусироваться на предыдущем пункте
    /// </summary>
    public void FocusPrevItem()
    {
      if (_selectedItemIndex == -1 || _selectedItemIndex == 0)
        _selectedItemIndex = _orderedItems.Count - 1;
      else
        _selectedItemIndex--;

      SetMenuItemStatus(_orderedItems[_selectedItemIndex].Id, MenuItemStatuses.Focused);
    }
  }
}
