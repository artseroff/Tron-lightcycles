using Controller;
using Model.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerConsole.Menu
{
  /// <summary>
  /// Консольный контроллер паузы
  /// </summary>
  public class ControllerPauseConsole : ConsoleControllerBase, IControllerPause
  {
    /// <summary>
    /// Родительский контроллер
    /// </summary>
    public ConsoleControllerBase ParentController { get; protected set; }

    /// <summary>
    /// Модель главного меню
    /// </summary>
    public PauseMenu PauseMenu { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parParentController">Родительский контроллер</param>
    public ControllerPauseConsole(ConsoleControllerBase parParentController)
    {
      ParentController = parParentController;
      PauseMenu = new PauseMenu();
      ((IControllerPause)this).SubcribeOnPauseMenuItemsEvents();
    }


    /// <summary>
    /// Начать работу
    /// </summary>
    public override void Start()
    {
      new ViewConsole.Menu.ViewPauseMenuConsole(PauseMenu); 
      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        switch (keyInfo.Key)
        {
          case ConsoleKey.LeftArrow:
            PauseMenu.FocusPrevItem();
            break;
          case ConsoleKey.RightArrow:
            PauseMenu.FocusNextItem();
            break;
          case ConsoleKey.Enter:
            PauseMenu.EnterSelectedItem();
            break;
        }
      } while (!NeedExit);
    }

    /// <summary>
    /// Выйти в главное меню
    /// </summary>
    void IControllerPause.Exit()
    {
      ((IGameController)ParentController).InterruptGame();
      Stop();
      ParentController.Stop();
    }


    /// <summary>
    /// Продолжить игру
    /// </summary>
    void IControllerPause.Resume()
    {
      Stop();
      ((IGameController)ParentController).ResumeGame();
    }
  }
}
