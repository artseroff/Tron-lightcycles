using ViewConsole;
using Controller;
using ControllerConsole.Menu;
using ControllerConsole.Records;
using Model.Game;
using Model.Game.Direction;
using Model.Records;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ViewConsole.Game;
using static Controller.IGameController;

namespace ControllerConsole.Game
{
  /// <summary>
  /// Консольный контроллер игры
  /// </summary>
  public class GameControllerConsole : ConsoleControllerBase, IGameController
  {    
    /// <summary>
    /// Консольное представление игры
    /// </summary>
    private ViewGameConsole _viewGameConsole;

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
    public GameControllerConsole()
    {
      GameModel = new GameModel();
      ((IGameController)this).SubcribeOnGameModelEvents();      
    }

    /// <summary>
    /// Начать работу
    /// </summary>
    public override void Start()
    {
      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        // Если пришло событие от игры, то
        // ожидание нажатия любой кнопки
        if (GameEvent != null)
        {
          HandleGameEvent();
          GameEvent = null;
          continue;
        }

        switch (keyInfo.Key)
        {
          case ConsoleKey.UpArrow:
            GameModel.ChangeUserDirection(DirectionEnum.Up);
            break;
          case ConsoleKey.RightArrow:
            GameModel.ChangeUserDirection(DirectionEnum.Right);
            break;
          case ConsoleKey.DownArrow:
            GameModel.ChangeUserDirection(DirectionEnum.Down);
            break;
          case ConsoleKey.LeftArrow:
            GameModel.ChangeUserDirection(DirectionEnum.Left);
            break;
          case ConsoleKey.Spacebar:
            GameModel.SpeedUpUser();
            break;
          case ConsoleKey.Escape:
            ((IGameController)this).StartPauseController();
            break;
        }
      } while (!NeedExit);
    }

    /// <summary>
    /// Обработка события, пришедшего от модели
    /// </summary>
    public void HandleGameEvent()
    {      
      if (GameEvent is GameEvents.LevelStarted)
      {        
        _viewGameConsole = new ViewGameConsole(GameModel);
        GameModel.Resume();
      }
      else if (GameEvent is GameEvents.LevelEnded)
      {        
        _viewGameConsole?.Clear();        
        GameModel.Resume();
      }
      else
      {
        Stop();
        _viewGameConsole?.Clear();
        if (GameEvent is GameEvents.Win)
        {
          NewRecordInputModel newRecordInputModel = new NewRecordInputModel(GameModel.StopwatchValue);
          if (newRecordInputModel.IsTimeLessThanWorst())
          {
            new NewRecordControllerConsole(newRecordInputModel).Start();
          }
        }
      }
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
      _viewGameConsole?.Draw();
      GameModel.Resume();
    }

    /// <summary>
    /// Прервать игру
    /// </summary>
    public void InterruptGame()
    {
      GameModel.StopInterruptible();
      Stop();
    }

    /// <summary>
    /// Уровень начался
    /// </summary>
    void IGameController.LevelStarted()
    {
      GameModel.Pause();
      _viewGameConsole?.Clear();      
      new ViewNextLevelInfoConsole(GameModel.LevelCounter);      
      GameEvent = GameEvents.LevelStarted;
    }

    void IGameController.LevelEnded()
    {
      GameModel.Pause();
      _viewGameConsole.DrawPressAnyKeyMessage();
      _viewGameConsole?.UnsubscribeOnHeadsUpdate();
      _viewGameConsole?.UnsubscribeOnSpeedUpUpdate();
      GameEvent = GameEvents.LevelEnded;
    }

    /// <summary>
    /// Проигрыш
    /// </summary>
    void IGameController.Loss()
    {
      _viewGameConsole.DrawLossMessage();
      GameEvent = GameEvents.Loss;
    }

    /// <summary>
    /// Победа
    /// </summary>
    void IGameController.Victory()
    {
      _viewGameConsole.DrawWinMessage();
      GameEvent = GameEvents.Win;      
    }

    /// <summary>
    /// Передать управление контроллеру паузы
    /// </summary>
    void IGameController.StartPauseController()
    {
      GameModel.Pause();
      _viewGameConsole?.Clear();      
      new ControllerPauseConsole(this).Start();
    }
  }
}
