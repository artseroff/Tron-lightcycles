using ViewConsole;
using Model.Game;
using Model.Game.Cycles;
using Model.Game.Level;
using System;
using System.Collections.Generic;
using System.Text;
using View.Game;

namespace ViewConsole.Game
{
  /// <summary>
  /// Консольное представление игры
  /// </summary>
  public class ViewGameConsole : ViewGameBase
  {
    /// <summary>
    /// Масштаб по Х
    /// </summary>
    public const int SCALE_X = 2;

    /// <summary>
    /// Отступ от матрицы арены
    /// </summary>
    public const int MARGIN_ARENA = 4;

    /// <summary>
    /// Отступ вправо от символа ускорения
    /// </summary>
    private const int MARGIN_LEFT_SPEED_UP_SYMBOL = 1;

    /// <summary>
    /// Пустая первая боковая клетка в арене
    /// (чтобы расстояние от рамки арены до арены
    /// казалось одинаковым)
    /// </summary>
    public const int X_SIDE_ARENA_EMPTY_CELL = 1;

    /// <summary>
    /// Х координата последнего доступного прямоугольника ускорения
    /// </summary>
    private int _lastFullSpeedUpSymbolX;

    /// <summary>
    /// Координата верхнего левого угла арены
    /// </summary>
    private Point _arenaPoint;  

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parGameModel">Модель игры</param>
    public ViewGameConsole(GameModel parGameModel) : base(parGameModel)
    {
      Init();
      Draw();
    }

    /// <summary>
    /// Отписаться от обновлений голов мотоциклов
    /// </summary>
    public void UnsubscribeOnHeadsUpdate()
    {
      GameModel.OnModelChanged -= DrawMovedObjects;
    }

    /// <summary>
    /// Отписаться от обновления полос ускорения
    /// </summary>
    public void UnsubscribeOnSpeedUpUpdate()
    {
      GameModel.OnSpeedUp -= UpdateSpeedUpBlock;
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Init()
    {
      Clear();
      _arenaPoint = new Point(MARGIN_ARENA * SCALE_X + X_SIDE_ARENA_EMPTY_CELL, MARGIN_ARENA);      
      InitCycleViews();
      DrawSpeedUpBlock();
    }

    /// <summary>
    /// Очистить окно игры
    /// </summary>
    public void Clear()
    {
      FastWriter.Clear();
      FastWriter.PrintBuffer();
    }

    /// <summary>
    /// Полная перерисовка окна игры
    /// </summary>
    public override void Draw()
    {
      DrawSpeedUpBlock();
      base.Draw();
      FastWriter.PrintBuffer();
    }

    /// <summary>
    /// Инициализация представлений мотоциклов
    /// </summary>
    protected override void InitCycleViews()
    {
      foreach (ICycle elCycle in GameModel.Cycles)
      {
        CycleViews.Add(new ViewCycleConsole(elCycle, _arenaPoint));
      }

    }

    /// <summary>
    /// Нарисовать блок с ускорением
    /// </summary>
    private void DrawSpeedUpBlock()
    {
      // Текст блока ускорений с надписью "ускорение" и тремя прямоугольниками ускорений
      string textSpeedUpBlock = SPEED_UP_TEXT;
      for (int i = 0; i < GameModel.LeftUserSpeedUps; i++)
      {
        textSpeedUpBlock += "".PadLeft(MARGIN_LEFT_SPEED_UP_SYMBOL, ' ')+ GameSymbols.FULL_SPEED_UP_SYMBOL;
      }
      FastWriter.WriteToBuffer(textSpeedUpBlock, _arenaPoint.X, Console.WindowHeight - 2, ConsoleColor.Cyan);
      _lastFullSpeedUpSymbolX = _arenaPoint.X + textSpeedUpBlock.Length;

      string emptySpeedUpsText = "";
      for (int i = GameModel.LeftUserSpeedUps; i < GameModel.INIT_SPEED_UPS_COUNT; i++)
      {
        emptySpeedUpsText += "".PadLeft(MARGIN_LEFT_SPEED_UP_SYMBOL, ' ') + GameSymbols.EMPTY_SPEED_UP_SYMBOL;
      }
      FastWriter.WriteToBuffer(emptySpeedUpsText, _lastFullSpeedUpSymbolX, Console.WindowHeight - 2);

      _lastFullSpeedUpSymbolX -= MARGIN_LEFT_SPEED_UP_SYMBOL + 1;
    }

    /// <summary>
    /// Перерисовать матрицу арены
    /// </summary>
    protected override void RedrawArena()
    {      
      DrawArenaBorder();
      DrawArenaMatrix();
    }

    /// <summary>
    /// Нарисовать рамку арены
    /// </summary>
    private void DrawArenaBorder()
    {      
      int borderWidth = Console.WindowWidth - 2 * MARGIN_ARENA * SCALE_X;
      string text = "┌" + "".PadRight(borderWidth, '─') + "┐";
      FastWriter.WriteToBuffer(text, _arenaPoint.X - X_SIDE_ARENA_EMPTY_CELL - 1, _arenaPoint.Y - 1);
      text = "└" + "".PadRight(borderWidth, '─') + "┘";
      FastWriter.WriteToBuffer(text, _arenaPoint.X - X_SIDE_ARENA_EMPTY_CELL - 1, Console.WindowHeight - (MARGIN_ARENA - 1) - 1);
      int rightVerticalLine = Console.WindowWidth - (MARGIN_ARENA * SCALE_X);
      for (int i = _arenaPoint.Y; i < Console.WindowHeight - (MARGIN_ARENA - 1) - 1; i++)
      {
        FastWriter.WriteToBuffer("│", _arenaPoint.X - X_SIDE_ARENA_EMPTY_CELL - 1, i);
        FastWriter.WriteToBuffer("│", rightVerticalLine, i);
      }
    }

    /// <summary>
    /// Нарисовать рамку арены
    /// </summary>
    private void DrawArenaMatrix()
    {
      int[][] matrix = GameModel.Arena.Matrix;
      for (int y = 0; y < matrix.Length; y++)
      {
        for (int x = 0; x < matrix[0].Length; x++)
        {
          int cellFill = matrix[y][x];
          if (cellFill != (int)CellFillEnum.Empty && !LevelUtils.IsHeadCell((CellFillEnum)cellFill))
          {            
            ConsoleColor color = ConsoleColorByCellFillCreator.GetColorByCellFill((CellFillEnum)cellFill);
            FastWriter.WriteToBuffer(new string(GameSymbols.FULL_BLOCK_SYMBOL, SCALE_X), _arenaPoint.X + x*SCALE_X, _arenaPoint.Y + y, color);
          }
        }
      }

    }

    /// <summary>
    /// Нарисовать двигающиеся объекты
    /// </summary>
    protected override void DrawMovedObjects()
    {
      base.DrawMovedObjects();
      FastWriter.PrintBuffer();
    }

    /// <summary>
    /// Нарисовать секундомер
    /// </summary>
    protected override void DrawStopWatch()
    {
      string stopwatchText = string.Format("{0:f2}", GameModel.StopwatchValue);
      FastWriter.WriteToBuffer(stopwatchText, Console.WindowWidth / 2 - (int)Math.Ceiling(stopwatchText.Length / 2.0), 1, ConsoleColor.Cyan);
    }

    /// <summary>
    /// Обновить блок с ускорением
    /// </summary>
    protected override void UpdateSpeedUpBlock()
    {
      string text = "".PadLeft(MARGIN_LEFT_SPEED_UP_SYMBOL, ' ') + GameSymbols.EMPTY_SPEED_UP_SYMBOL;
      FastWriter.WriteToBuffer(text, _lastFullSpeedUpSymbolX, Console.WindowHeight - 2);
      _lastFullSpeedUpSymbolX -= MARGIN_LEFT_SPEED_UP_SYMBOL + 1;
      FastWriter.PrintBuffer();
    }

    /// <summary>
    /// Нарисовать сообщение о проигрыше
    /// </summary>
    public override void DrawLossMessage()
    {
      DrawCenteredTextInPreLastRow($"{GAME_LOSS_TEXT}. {PRESS_ANY_KEY_TEXT}");
    }

    /// <summary>
    /// Нарисовать сообщение о победе
    /// </summary>
    public override void DrawWinMessage()
    {
      DrawCenteredTextInPreLastRow($"{GAME_WIN_TEXT}. {PRESS_ANY_KEY_TEXT}");
    }

    /// <summary>
    /// Отобразить текст о необходимости нажатия любой кнопки
    /// </summary>
    public override void DrawPressAnyKeyMessage()
    {
      DrawCenteredTextInPreLastRow(PRESS_ANY_KEY_TEXT);
    }

    /// <summary>
    /// Отобразить текст по центру предпоследней строки
    /// </summary>
    /// <param name="parText">Текст</param>
    private void DrawCenteredTextInPreLastRow(string parText)
    {
      FastWriter.WriteToBuffer("".PadRight(Console.WindowWidth, ' '), 0, Console.WindowHeight - 2);
      FastWriter.WriteToBuffer(parText, Console.WindowWidth / 2 - parText.Length / 2, Console.WindowHeight - 2);
      FastWriter.PrintBuffer();
    }
  }
}
