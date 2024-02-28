using Controller;
using ControllerWpf.Menu;
using ControllerWpf.Records;
using Model.Game;
using Model.Game.Direction;
using Model.Records;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using ViewWpf;
using ViewWpf.Game;
using static Controller.IGameController;

namespace ControllerWpf.Game
{
  /// <summary>
  /// Wpf контроллер игры
  /// </summary>
  public class GameControllerWpf : WpfControllerBase, IGameController
  {

    /// <summary>
    /// Модель главного меню
    /// </summary>
    public GameModel GameModel { get; private set; }

    /// <summary>
    /// Событие от модели игры
    /// </summary>
    public GameEvents? GameEvent { get; private set; }


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parParentController">Родительский контроллер</param>
    public GameControllerWpf(WpfControllerBase parParentController)
    {
      ParentController = parParentController;
      GameModel = new GameModel();
      ((IGameController)this).SubcribeOnGameModelEvents();
    }

    /// <summary>
    /// Начать игру
    /// </summary>
    public void StartGame()
    {
      GameModel.StartGame();
      Start();
    }


    /// <summary>
    /// Продолжить игру
    /// </summary>
    public void ResumeGame()
    {
      ((ViewGameWpf)KeyPressableView).Draw();
      Start();
      GameModel.Resume();
    }

    /// <summary>
    /// Прервать игру
    /// </summary>
    public void InterruptGame()
    {
      Stop();
      GameModel.StopInterruptible();
    }

    /// <summary>
    /// Обработчик события нажатия на клавиатуру
    /// </summary>
    /// <param name="parSender">Источник события</param>
    /// <param name="parArgs">Объект KeyEventArgs, содержащий данные события</param>
    protected override void KeyEventHandler(object parSender, KeyEventArgs parArgs)
    {
      // Если пришло событие от игры, то
      // ожидание нажатия любой кнопки
      if (GameEvent != null)
      {
        HandleGameEvent();
        GameEvent = null;
        return;
      }

      switch (parArgs.Key)
      {
        case Key.Up:
          GameModel.ChangeUserDirection(DirectionEnum.Up);
          break;
        case Key.Right:
          GameModel.ChangeUserDirection(DirectionEnum.Right);
          break;
        case Key.Down:
          GameModel.ChangeUserDirection(DirectionEnum.Down);
          break;
        case Key.Left:
          GameModel.ChangeUserDirection(DirectionEnum.Left);
          break;
        case Key.Space:
          GameModel.SpeedUpUser();
          break;
        case Key.Escape:
          ((IGameController)this).StartPauseController();
          break;
      }
    }

    /// <summary>
    /// Обработка события, пришедшего от модели
    /// </summary>
    public void HandleGameEvent()
    {
      Stop();
      if (GameEvent is GameEvents.LevelStarted)
      {      
        KeyPressableView = new ViewGameWpf(GameModel);
        Start();
        GameModel.Resume();
      }
      else if (GameEvent is GameEvents.LevelEnded)
      {       
        GameModel.Resume();
      }
      else
      {        
        if (GameEvent is GameEvents.Loss)
        {
          ParentController.Start();
        }
        else
        if (GameEvent is GameEvents.Win)
        {
          NewRecordInputModel newRecordInputModel = new NewRecordInputModel(GameModel.StopwatchValue);
          if (!newRecordInputModel.IsTimeLessThanWorst())
          {
            ParentController.Start();
          }
          else
          {
            new NewRecordControllerWpf(this, newRecordInputModel).Start();
          }
        }
      }
    }

    /// <summary>
    /// Уровень начался
    /// </summary>
    void IGameController.LevelStarted()
    {
      GameModel.Pause();
      KeyPressableView = new ViewNextLevelInfoWpf(GameModel.LevelCounter);
      Start();
      GameEvent = GameEvents.LevelStarted;
    }


    /// <summary>
    /// Обработка события, пришедшего от модели
    /// </summary>
    void IGameController.LevelEnded()
    {
      GameModel.Pause();
      ((ViewGameWpf)KeyPressableView).DrawPressAnyKeyMessage();
      GameEvent = GameEvents.LevelEnded;
    }

    /// <summary>
    /// Проигрыш
    /// </summary>
    void IGameController.Loss()
    {      
      ((ViewGameWpf)KeyPressableView).DrawLossMessage();
      GameEvent = GameEvents.Loss;      
    }

    /// <summary>
    /// Победа
    /// </summary>
    void IGameController.Victory()
    {
      //Stop();
      ((ViewGameWpf)KeyPressableView).DrawWinMessage();
      GameEvent = GameEvents.Win;      
    }

    /// <summary>
    /// Передать управление контроллеру паузы
    /// </summary>
    void IGameController.StartPauseController()
    {
      GameModel.Pause();
      Stop();
      new ControllerPauseMenuWpf(this).Start();
    }

  }
}
