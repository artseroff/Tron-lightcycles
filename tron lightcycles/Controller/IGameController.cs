using Model.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
  /// <summary>
  /// Интерфейс игрового контроллера
  /// </summary>
  public interface IGameController
  {
    /// <summary>
    /// Задержка в мс для перехода на уровни 
    /// и после конца игры
    /// </summary>
    public const int SLEEP_TIMEOUT = 2000;

    /// <summary>
    /// Модель главного меню
    /// </summary>
    GameModel GameModel { get; }

    /// <summary>
    /// Событие от модели игры
    /// </summary>
    GameEvents? GameEvent { get; }    

    /// <summary>
    /// Подписаться на события игры
    /// </summary>
    public void SubcribeOnGameModelEvents()
    {
      GameModel.OnLevelStarted += LevelStarted;
      GameModel.OnLevelEnded += LevelEnded;
      GameModel.OnLoss += Loss;
      GameModel.OnVictory += Victory;
    }
    

    /// <summary>
    /// Начать игру
    /// </summary>
    public void StartGame();

    /// <summary>
    /// Продолжить игру
    /// </summary>
    void ResumeGame();

    /// <summary>
    /// Прервать игру
    /// </summary>
    void InterruptGame();

    /// <summary>
    /// Передать управление контроллеру паузы
    /// </summary>
    void StartPauseController();

    /// <summary>
    /// Обработка события, пришедшего от модели
    /// </summary>
    void HandleGameEvent();

    /// <summary>
    /// Уровень начался
    /// </summary>
    protected void LevelStarted();

    /// <summary>
    /// Уровень закончился
    /// </summary>
    protected void LevelEnded();

    /// <summary>
    /// Проигрыш
    /// </summary>
    protected void Loss();

    /// <summary>
    /// Победа
    /// </summary>
    protected void Victory();

  }
}
