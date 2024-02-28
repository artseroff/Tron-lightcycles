using Model.Game;
using Model.Game.Cycles.Decorator;
using System;
using System.Collections.Generic;
using System.Text;

namespace View.Game
{
  /// <summary>
  /// Базовое представление игры
  /// </summary>
  public abstract class ViewGameBase : ViewBase
  {
    /// <summary>
    /// Текст ускорения
    /// </summary>
    protected const string SPEED_UP_TEXT = "Ускорение";

    /// <summary>
    /// Текст при проигрыше
    /// </summary>
    protected const string GAME_LOSS_TEXT = "Вы проиграли";

    /// <summary>
    /// Текст при победе
    /// </summary>
    protected const string GAME_WIN_TEXT = "Победа";

    /// <summary>
    /// Текст для продолжения нажмите любую клавишу
    /// </summary>
    public const string PRESS_ANY_KEY_TEXT = "Для продолжения нажмите любую клавишу...";


    /// <summary>
    /// Игровая модель
    /// </summary>
    protected GameModel GameModel { get; private set; }

    /// <summary>
    /// Представления мотоциклов
    /// </summary>
    protected List<ViewCycleBase> CycleViews { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parGameModel">Модель игры</param>
    public ViewGameBase(GameModel parGameModel)
    {
      GameModel = parGameModel;
      GameModel.OnModelChanged += DrawMovedObjects;
      GameModel.OnSpeedUp += UpdateSpeedUpBlock;
      CycleViews = new List<ViewCycleBase>();

    }

    /// <summary>
    /// Инициализация представлений мотоциклов
    /// </summary>
    protected abstract void InitCycleViews();

    /// <summary>
    /// Полная перерисовка окна игры
    /// </summary>
    public override void Draw()
    {
      RedrawArena();
    }

    /// <summary>
    /// Нарисовать двигающиеся объекты
    /// </summary>
    protected virtual void DrawMovedObjects()
    {
      DrawStopWatch();
      DrawNewCycleHeads();
    }

    /// <summary>
    /// Нарисовать головы мотоциклов на новых координатах,
    /// заменив старые координаты на следы соответствующих мотоциклов
    /// </summary>
    protected virtual void DrawNewCycleHeads()
    {      
      for (int i = 0; i < CycleViews.Count; i++)
      {
        
        CycleViews[i].Draw();
        if (CycleViews[i].IsCrashed)
        {          
          CycleViews.RemoveAt(i);
          i--;
        }
      }
    }

    /// <summary>
    /// Перерисовать матрицу арены
    /// </summary>
    protected abstract void RedrawArena();

    /// <summary>
    /// Нарисовать секундомер
    /// </summary>
    protected abstract void DrawStopWatch();


    /// <summary>
    /// Обновить блок с ускорением
    /// </summary>
    protected abstract void UpdateSpeedUpBlock();

    /// <summary>
    /// Нарисовать сообщение о проигрыше
    /// </summary>
    public abstract void DrawLossMessage();

    /// <summary>
    /// Нарисовать сообщение о победе
    /// </summary>
    public abstract void DrawWinMessage();

    /// <summary>
    /// Отобразить текст о необходимости нажатия любой кнопки
    /// </summary>
    public abstract void DrawPressAnyKeyMessage();

  }
}
