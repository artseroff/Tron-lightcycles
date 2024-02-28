using Model.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using View.Menu;

namespace ViewConsole.Menu
{
  /// <summary>
  /// Консольное представление паузы
  /// </summary>
  public class ViewPauseMenuConsole : ViewPauseMenuBase
  {
    /// <summary>
    /// Ширина меню паузы
    /// </summary>
    private const int MENU_WIDTH = 33;

    /// <summary>
    /// Отступ вниз от текста меню паузы
    /// </summary>
    private const int MARGIN_DOWN_TEXT_MENU = 1;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenu">Меню паузы</param>
    public ViewPauseMenuConsole(PauseMenu parMenu) : base(parMenu)
    {      
      Init();
      Draw();
    }


    /// <summary>
    /// Инициализация
    /// </summary>
    private void Init()
    {
      
      Console.Clear();      
      List<string> textLines = TextWrapper.GetLinesWithWidthWrappedBySpace(PAUSE_TEXT, MENU_WIDTH);
      Width = MENU_WIDTH;
      Y = Console.WindowHeight / 2 - textLines.Count/ 2;
      X = Console.WindowWidth / 2 - MENU_WIDTH / 2;
      for (int i = 0; i < textLines.Count; i++)
      {
        Console.SetCursorPosition(X, Y + i);
        Console.WriteLine(textLines[i]);
      }

      Items[0].X = X + MENU_WIDTH / 4 - Items[1].Width / 2;
      Items[0].Y = Y + textLines.Count + MARGIN_DOWN_TEXT_MENU;

      Items[1].X = X + MENU_WIDTH - MENU_WIDTH/4 - Items[1].Width/2;
      Items[1].Y = Items[0].Y;
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
  }
}
