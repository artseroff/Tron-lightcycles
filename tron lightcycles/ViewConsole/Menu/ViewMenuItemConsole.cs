using Model.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Menu;

namespace ViewConsole.Menu
{
  /// <summary>
  /// Представление пункта меню
  /// </summary>
  public class ViewMenuItemConsole : ViewMenuItemBase
  {
    /// <summary>
    /// Высота
    /// </summary>
    private const int HEIGHT = 1;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenuItemBase">Модель пункта меню</param>
    public ViewMenuItemConsole(MenuItem parMenuItemBase) : base(parMenuItemBase)
    {
      if (parMenuItemBase != null)
      {
        Height = HEIGHT;
        Width = MenuItem.Name.Length;
      }
      parMenuItemBase.NeedRedraw += Draw;
    }

    /// <summary>
    /// Нарисовать представление класса
    /// </summary>
    public override void Draw()
    {
      
      Console.CursorLeft = X;
      Console.CursorTop = Y;

      ConsoleColor savedColor = Console.ForegroundColor;

      switch(this.MenuItem.Status)
      {
        case MenuItemStatuses.Focused:
          Console.ForegroundColor = ConsoleColor.White;
          break;
        case MenuItemStatuses.Selected:
          Console.ForegroundColor = ConsoleColor.White;
          break;
        case MenuItemStatuses.None:
          Console.ForegroundColor = ConsoleColor.Cyan;
          break;
      }

      Console.Write(MenuItem.Name);

      Console.ForegroundColor = savedColor;
    }
  }
}
