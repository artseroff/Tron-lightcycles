using Controller;
using ControllerWpf.instructions;
using ControllerWpf.Records;
using Model.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using ViewWpf;
using ViewWpf.Menu;

namespace ControllerWpf.Menu
{
  /// <summary>
  /// Контроллер главного меню wpf
  /// </summary>
  public class ControllerMenuMainWpf : WpfControllerBase, IControllerMainMenu
  {
    /// <summary>
    /// Модель главного меню
    /// </summary>
    public MenuMain MenuMain { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public ControllerMenuMainWpf()
    {
      MenuMain = MenuMain.Instance;
      ((IControllerMainMenu)this).SubcribeOnMainMenuItemsEvents();
    }

    /// <summary>
    /// Начать работу
    /// </summary>
    public override void Start()
    {
      KeyPressableView = new ViewMenuMainWpf(MenuMain);
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
        case Key.Up:
          MenuMain.FocusPrevItem();
          break;
        case Key.Down:
          MenuMain.FocusNextItem();
          break;
        case Key.Enter:
          MenuMain.EnterSelectedItem();
          break;
      }
    }


    /// <summary>
    /// Начать игру 
    /// </summary>
    void IControllerMainMenu.StartGame()
    {
      Stop();
      new ControllerWpf.Game.GameControllerWpf(this).StartGame();
    }

    /// <summary>
    /// Вызвать инструкции
    /// </summary>
    void IControllerMainMenu.CallInstruction()
    {
      Stop();
      new ControllerInstructionsWpf(this).Start();
    }

    /// <summary>
    /// Вызвать таблицу рекордов
    /// </summary>
    void IControllerMainMenu.CallRecords()
    {
      Stop();
      new ControllerTableRecordsWpf(this).Start();
    }

    /// <summary>
    /// Выйти из главного меню
    /// </summary>
    void IControllerMainMenu.Exit()
    {
      ((ViewMenuMainWpf)KeyPressableView).Close();
    }


  }
}
