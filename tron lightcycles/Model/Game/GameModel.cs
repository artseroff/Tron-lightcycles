using Model.Game.Cycles;
using Model.Game.Cycles.BestPath;
using Model.Game.Cycles.Decorator;
using Model.Game.Direction;
using Model.Game.Level;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Model.Game
{
  /// <summary>
  /// Модель игры
  /// </summary>
  public class GameModel
  {
    /// <summary>
    /// Последний уровень
    /// </summary>
    private const int LAST_LEVEL = 5;

    /// <summary>
    /// Погрешность сравнения вещественных чисел
    /// </summary>
    public const float EPS = 0.0001f;

    /// <summary>
    /// Начальное количество ускорений
    /// </summary>
    public const int INIT_SPEED_UPS_COUNT = 3;

    /// <summary>
    /// Игровой поток
    /// </summary>
    private Thread _gameThread;

    /// <summary>
    /// Защелка паузы игры
    /// </summary>
    private readonly ManualResetEvent _pauseResetEvent = new ManualResetEvent(true);

    /// <summary>
    /// Секундоменр
    /// </summary>
    private Stopwatch _stopwatch;

    /// <summary>
    /// Предыдущее значение секундомера
    /// </summary>
    private float _previousStopwatchValue;

    /// <summary>
    /// Событие изменения модели
    /// </summary>
    public Action OnModelChanged;

    /// <summary>
    /// Событие пользователь ускорился
    /// </summary>
    public Action OnSpeedUp;

    /// <summary>
    /// Событие проигрыша
    /// </summary>
    public Action OnLoss;

    /// <summary>
    /// Событие победы
    /// </summary>
    public Action OnVictory;

    /// <summary>
    /// Событие наступления следующего уровня
    /// </summary>
    public Action OnLevelStarted;

    /// <summary>
    /// Событие окончания уровня
    /// (кроме последнего)
    /// </summary>
    public Action OnLevelEnded;

    /// <summary>
    /// Пользовательский мотоцикл
    /// </summary>
    public AcceleratedCycleDecorator Player { get; private set; }

    /// <summary>
    /// Счетчик секундомера
    /// </summary>
    public float StopwatchValue { get; private set; }

    /// <summary>
    /// Счетчик уровней
    /// </summary>
    public int LevelCounter { get; private set; }

    /// <summary>
    /// Арена
    /// </summary>
    public Arena Arena { get; private set; }

    /// <summary>
    /// Список мотоциклов
    /// </summary>
    public List<ICycle> Cycles { get; private set; }

    /// <summary>
    /// Количество оставшихся ускорений пользователя 
    /// </summary>
    public int LeftUserSpeedUps { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public GameModel()
    {
    }

    /// <summary>
    /// Начать игру
    /// </summary>
    public void StartGame()
    {
      _gameThread = new Thread(StartGameCycle);
      _gameThread.IsBackground = true;
      _gameThread.Start();
    }
    

    /// <summary>
    /// Запустить игровой цикл
    /// </summary>
    private void StartGameCycle()
    {
      try
      {
        _stopwatch = new Stopwatch();
        LevelCounter = 1;
        StopwatchValue = 0;
        float floatInterval = 0;
        while (LevelCounter <= LAST_LEVEL)
        {
          LevelUtils.FormArenaAndCyclesFromFile(LevelCounter, out Arena localArena, out List<ICycle> localCycles);
          Arena = localArena;
          Cycles = localCycles;
          Arena.OnCycleHeadsCrashed += CycleHeadsCrashed;
          Player = (AcceleratedCycleDecorator)localCycles[0];


          _stopwatch.Start();
          OnLevelStarted?.Invoke();
          // Раскоментировать для тестирования без уровней
          //_pauseResetEvent.WaitOne();
          bool levelEnded = false;
          LeftUserSpeedUps = INIT_SPEED_UPS_COUNT;
          while (!levelEnded)
          {

            _pauseResetEvent.WaitOne();

            MoveCycles(floatInterval);
            _previousStopwatchValue = StopwatchValue;
            StopwatchValue = _stopwatch.ElapsedMilliseconds / 1000f;
            floatInterval = StopwatchValue - _previousStopwatchValue;

            OnModelChanged?.Invoke();

            // уровень закончился или проигрыш
            levelEnded = (!Player.IsCrashed && Cycles.Count == 1) || Player.IsCrashed;
          }

          _stopwatch.Stop();          
          OnModelChanged?.Invoke();
          // проигрыш
          if (Player.IsCrashed)
          {
            OnLoss?.Invoke();
            return;
          }
          // победа
          if (LevelCounter == LAST_LEVEL)
          {
            OnVictory?.Invoke();
            return;
          }
          // следующий уровень
          OnLevelEnded?.Invoke();
          _pauseResetEvent.WaitOne();
          LevelCounter++;
        }
      }
      catch (ThreadInterruptedException)
      {
        return;
      }
    }

    /// <summary>
    /// Событие аварии голову в голову двух мотоциклов,
    /// помечает того, кто уже приехал в клетку (первый мотоцикл), как разбитый
    /// </summary>
    /// <param name="parCycleId">Id первого приехавшего в клетку мотоцикла</param>
    private void CycleHeadsCrashed(int parCycleId)
    {
      ICycle cycle = Cycles.Find((parSearchCycle) => parSearchCycle.Id == parCycleId);
      if (cycle != null)
      {
        cycle.IsCrashed = true;

        Arena.ClearCycleTrailOnField(cycle.Head, cycle.Trail);
      }
    }

    /// <summary>
    /// Двигать мотоциклы по направлению их движения и
    /// убрать с арены разбитые мотоциклы
    /// </summary>
    /// <param name="parTime">Время в сек для расчета перемещения</param>
    private void MoveCycles(float parTime)
    {
      for (int i = 0; i < Cycles.Count; i++)
      {
        // Если разбился пользователь или только пользователь выжил
        // прекратить перемещение
        /*if ((!Player.IsCrashed && Cycles.Count == 1) || Player.IsCrashed)
        {
          break;
        }*/
        if (i == 0)
        {
          lock (this)
          {
            Player.Move(parTime);            
          }
        }
        else
        {
          
          Cycles[i].Move(parTime);          
          if (Cycles[i].IsCrashed)
          {
            Cycles.RemoveAt(i);
            i--;
          }
        }
      }
    }

    /// <summary>
    /// Поставить игру на паузу
    /// </summary>
    public void Pause()
    {
      _stopwatch.Stop();
      _pauseResetEvent.Reset();
    }

    /// <summary>
    /// Продолжить игру
    /// </summary>
    public void Resume()
    {
      _stopwatch.Start();
      _pauseResetEvent.Set();
    }

    /// <summary>
    /// Прервать игру
    /// </summary>
    public void StopInterruptible()
    {
      _gameThread.Interrupt();
    }

    /// <summary>
    /// Поменять направление пользовательского мотоцикла
    /// </summary>
    /// <param name="parNewDirection">Новое направление</param>
    public void ChangeUserDirection(DirectionEnum parNewDirection)
    {
      lock (this)
      {
        Player.CheckAndSetNewDirection(parNewDirection);
      }
    }

    /// <summary>
    /// Ускорить пользовательский мотоцикл,
    /// если ускорения доступны
    /// </summary>
    public void SpeedUpUser()
    {
      lock (this)
      {
        if (LeftUserSpeedUps > 0 && !Player.NeedSpeedUp)
        {
          LeftUserSpeedUps--;
          Player.NeedSpeedUp = true;
          OnSpeedUp?.Invoke();
        }
      }
    }
  }
}
