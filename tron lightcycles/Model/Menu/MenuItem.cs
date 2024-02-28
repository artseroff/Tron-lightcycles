using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Menu
{
  /// <summary>
  /// Пункт меню
  /// </summary>
  public class MenuItem
  {    
  
    /// <summary>
    /// Делегат, описывающий тип события перерисовки элемента меню
    /// </summary>
    public delegate void dNeedRedraw();

    /// <summary>
    /// Делегат, описывающий тип события нажатия на пункт меню
    /// </summary>
    public delegate void dEnter();

    /// <summary>
    /// Событие перерисовки элемента меню
    /// </summary>
    public event dNeedRedraw NeedRedraw = null;

    /// <summary>
    /// Событие нажатия на пункт меню
    /// </summary>
    public event dEnter Enter = null;
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Поле статус пункта
    /// </summary>
    private MenuItemStatuses _status = MenuItemStatuses.None;

    /// <summary>
    /// Свойство статус пункта
    /// </summary>
    public MenuItemStatuses Status
    {
      get
      {
        return _status;
      }
      set
      {
        _status = value;
        NeedRedraw?.Invoke();
        if (_status == MenuItemStatuses.Selected)
          Enter?.Invoke();
      }
    }

    /// <summary>
    /// Номер пункта меню
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parId">Номер пункта меню</param>
    /// <param name="parName">Название пункта меню</param>
    public MenuItem(int parId, string parName)
    {
      Id = parId;
      Name = parName;
      Status = MenuItemStatuses.None;
    }
  }
}
