using Controller;
using ControllerConsole.Instructions;
using ControllerConsole.Records;
using Model.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewConsole;
using ViewConsole.Menu;

namespace ControllerConsole.Menu
{
  /// <summary>
  /// Консольный контроллер главного меню
  /// </summary>
  public class ControllerMenuMainConsole : ConsoleControllerBase, IControllerMainMenu
  {
    /// <summary>
    /// Моедль главного меню
    /// </summary>
    public MenuMain MenuMain { get; set; }

    /// <summary>
    /// Представление главного меню
    /// </summary>
    private ViewMenuMainConsole _menuMainView = null;

    /// <summary>
    /// Конструктор
    /// </summary>
    public ControllerMenuMainConsole() : base()
    {
      new ConsoleConfiguration();
      MenuMain = MenuMain.Instance;
      ((IControllerMainMenu)this).SubcribeOnMainMenuItemsEvents();
      _menuMainView = new ViewMenuMainConsole(MenuMain);
    }


    /// <summary>
    /// Начать работу
    /// </summary>
    public override void Start()
    {
      
      _menuMainView.Draw();
      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        switch (keyInfo.Key)
        {
          case ConsoleKey.UpArrow:
            MenuMain.FocusPrevItem();
            break;
          case ConsoleKey.DownArrow:
            MenuMain.FocusNextItem();
            break;
          case ConsoleKey.Enter:
            MenuMain.EnterSelectedItem();
            break;
        }
      } while (!NeedExit);
    }

    /// <summary>
    /// Вызвать инструкции
    /// </summary>
    void IControllerMainMenu.CallInstruction()
    {
      ControllerInstructionConsole controller = new ControllerInstructionConsole();
      controller.Start();
      _menuMainView.Draw();
    }

    /// <summary>
    /// Вызвать таблицу рекордов
    /// </summary>
    void IControllerMainMenu.CallRecords()
    {
      ControllerTableRecordsConsole controller = new ControllerTableRecordsConsole();
      controller.Start();
      _menuMainView.Draw();
    }

    /// <summary>
    /// Начать игру 
    /// </summary>
    void IControllerMainMenu.StartGame()
    {
      //Stop();
      new Game.GameControllerConsole().StartGame();
      _menuMainView.Draw();
    }

    /// <summary>
    /// Выйти из главного меню
    /// </summary>
    void IControllerMainMenu.Exit()
    {
      _menuMainView.Clear();
      Stop();
    }
  }
}
