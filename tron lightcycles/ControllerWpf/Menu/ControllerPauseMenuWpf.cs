using Controller;
using ControllerWpf.Game;
using Model.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewWpf.Menu;

namespace ControllerWpf.Menu
{
  /// <summary>
  /// Контроллер меню паузы
  /// </summary>
  public class ControllerPauseMenuWpf : WpfControllerBase, IControllerPause
  {

    /// <summary>
    /// Модель главного меню
    /// </summary>
    public PauseMenu PauseMenu { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parParentController">Родительский контроллер</param>
    public ControllerPauseMenuWpf(WpfControllerBase parParentController)
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
      KeyPressableView = new ViewPauseWpf(PauseMenu);
      base.Start();
    }

    /// <summary>
    /// Обработчик события нажатия на клавиатуру
    /// </summary>
    /// <param name="parSender">Источник события</param>
    /// <param name="parArgs">Объект KeyEventArgs, содержащий данные события</param>
    protected override void KeyEventHandler(object parSender, KeyEventArgs parArgs)
    {
      switch (parArgs.Key)
      {
        case Key.Left:
          PauseMenu.FocusPrevItem();
          break;
        case Key.Right:
          PauseMenu.FocusNextItem();
          break;
        case Key.Enter:
          PauseMenu.EnterSelectedItem();
          break;
      }
    }

    /// <summary>
    /// Выйти в главное меню
    /// </summary>
    void IControllerPause.Exit()
    {
      Stop();
      ((GameControllerWpf)ParentController).InterruptGame();
      ParentController.ParentController.Start();
    }

    /// <summary>
    /// Продолжить игру
    /// </summary>
    void IControllerPause.Resume()
    {
      Stop();
      ((GameControllerWpf)ParentController).ResumeGame();
    }
  }
}
