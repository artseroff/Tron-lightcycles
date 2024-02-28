using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewConsole;
using Model.Menu;
using View.Menu;

namespace ViewConsole.Menu
{
  /// <summary>
  /// Консольное представление главного меню
  /// </summary>
  public class ViewMenuMainConsole : ViewMenuBase
  {
    /// <summary>
    /// Отступ вниз от названия игры
    /// </summary>
    private const int MARGIN_DOWN_GAME_NAME = 4;

    /// <summary>
    /// Отступ вниз от пункта меню
    /// </summary>
    private const int MARGIN_DOWN_MENU_ITEM = 1;

    /// <summary>
    /// Массив строк из названия игры
    /// </summary>
    private readonly string[] _arrayGameNameRows;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenu">Циклическое меню</param>
    public ViewMenuMainConsole(CyclicMenu parMenu) : base(parMenu)
    {      
      _arrayGameNameRows = Properties.Resources.GameName.Split("\n");
      Init();
    }

    /// <summary>
    /// Создать представление пункта меню
    /// </summary>
    /// <param name="parItem">Модель пункта меню</param>
    /// <returns>Представление пункта меню</returns>
    protected override ViewMenuItemBase CreateMenuItem(MenuItem parItem)
    {
        return new ViewMenuItemConsole(parItem);
    }

    /// <summary>
    /// Нарисовать представление класса
    /// </summary>
    public override void Draw()
    {
     
      Console.Clear();

      // Отрисовка названия игры
      Console.ForegroundColor = ConsoleColor.Cyan;
      for (int i = 0; i < _arrayGameNameRows.Length; i++)
      {
        Console.SetCursorPosition(X, Y + i);
        Console.WriteLine(_arrayGameNameRows[i]);
      }
      Console.ForegroundColor = ConsoleColor.White;

      base.Draw();
    }


    /// <summary>
    /// Инициализация
    /// </summary>
    private void Init()
    {
      
      int height = _arrayGameNameRows.Length;
      ViewMenuItemBase[] items = Items;
      foreach (ViewMenuItemBase elViewMenuItemBase in items)
      {
        height += elViewMenuItemBase.Height + MARGIN_DOWN_MENU_ITEM;        
      }
      Height = height + MARGIN_DOWN_GAME_NAME;
      Width = _arrayGameNameRows[0].Length;

      X = Console.WindowWidth / 2 - Width / 2;
      Y = Console.WindowHeight / 2 - Height / 2;

      int y = Y + _arrayGameNameRows.Length + MARGIN_DOWN_GAME_NAME;

      foreach (ViewMenuItemBase elViewMenuItemBase in items)
      {
        // Центровка относительно самого длинного пункта меню
        elViewMenuItemBase.X = Console.WindowWidth / 2 - (int)Math.Ceiling(elViewMenuItemBase.Width / 2.0);
        elViewMenuItemBase.Y = y;
        y+= elViewMenuItemBase.Height + MARGIN_DOWN_MENU_ITEM;
      }
    }
    /// <summary>
    /// Очистить представление
    /// </summary>
    public void Clear()
    {
      Console.Clear();
    }

  }
}
