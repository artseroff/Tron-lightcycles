using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Menu
{
  /// <summary>
  /// Главное меню (Синглтон)
  /// </summary>
  public class MenuMain : CyclicMenu
  {
    /// <summary>
    /// Поле экземпляр главного меню
    /// </summary>
    private static MenuMain _instance = null;

    /// <summary>
    /// Свойство экземпляр главного меню
    /// </summary>
    public static MenuMain Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new MenuMain();
        }
        return _instance;
      }
    }    

    /// <summary>
    /// Закрытый конструктор
    /// </summary>
    private MenuMain()
    {
      AddMenuItem(new MenuItem((int)MenuMainIds.New, "Новая игра"));
      AddMenuItem(new MenuItem((int)MenuMainIds.Info, "Инструкция"));
      AddMenuItem(new MenuItem((int)MenuMainIds.Records, "Рекорды"));
      AddMenuItem(new MenuItem((int)MenuMainIds.Exit, "Выход"));
      FocusNextItem();
    }
  }
}
