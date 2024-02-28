using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Game;
using Model.Game.Cycles;
using Model.Game.Cycles.BestPath;
using Model.Game.Cycles.Decorator;
using Model.Game.Direction;
using Model.Game.Level;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TronLightCyclesTests
{
  /// <summary>
  /// Тесты декоратора мотоцикла с ИИ
  /// (с алгоритмом поиска наиболее 
  /// свободного направления)
  /// </summary>
  [TestClass()]
  public class CycleWithAIDecoratorTests
  {   

    /// <summary>
    /// Мотоцикл
    /// </summary>
    private CycleWithAIDecorator _cycle;

    /// <summary>
    /// Арена
    /// </summary>
    private Arena _arena;


    /// <summary>
    /// Конструктор
    /// </summary>
    public CycleWithAIDecoratorTests()
    {            
    }

    /// <summary>
    /// Получить скорость ConcreteCycle в клетках в сек
    /// </summary>
    /// <returns>Скорость ConcreteCycle в клетках в сек</returns>
    private float GetCycleSpeed()
    {
      ConcreteCycle concreteCycle = (ConcreteCycle)typeof(CycleWithAIDecorator)
        .BaseType.GetProperty("Cycle", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
        .GetValue(_cycle);
      
      float speed = ConcreteCycleTests.GetCycleSpeed(concreteCycle);
      return speed;
    }

    /// <summary>
    /// Инициалиация мотоцикла на арене первого уровня
    /// на координатах 0;0
    /// </summary>
    private void Init()
    {
      // Координаты мотоциклов из localCycles не отмечены в матрице арены
      LevelUtils.FormArenaAndCyclesFromFile(1, out Arena localArena, out List<ICycle> localCycles);
      _arena = localArena;     
      _cycle = new CycleWithAIDecorator(new ConcreteCycle(1, new Model.Game.Point(0,0), localArena));      
     
    }

    /// <summary>
    /// Поиск величины свободного пути вправо и вниз,
    /// если мотоцикл находится в первой ячейке
    /// первой строки и нет препятствий на арене, 
    /// кроме ее границ
    /// </summary>
    [TestMethod()]
    public void FindFreeDirectionRightAndDownNoWallsExceptMatrixBordersTest()
    {
      Init();     
      PossiblePath possiblePath = BestPathFinder.FindBestPath(_arena, _cycle.CurrentDirection, _cycle.Head);
      Assert.AreEqual(LevelUtils.MATRIX_SIZE - 1, possiblePath.Length);
      Assert.IsTrue(possiblePath.Direction is DirectionEnum.Right || possiblePath.Direction is DirectionEnum.Down);
    }


    /// <summary>
    /// Поиск величины свободного пути вверх и влево,
    /// если мотоцикл находится в последней ячейке
    /// последней строки и нет препятствий на арене, 
    /// кроме ее границ
    /// </summary>
    [TestMethod()]
    public void FindFreeDirectionUpAndLeftNoWallsExceptMatrixBordersTest()
    {

      Init();
      _cycle.Head.X = LevelUtils.MATRIX_SIZE - 1;
      _cycle.Head.Y = LevelUtils.MATRIX_SIZE - 1;
      PossiblePath possiblePath = BestPathFinder.FindBestPath(_arena, _cycle.CurrentDirection, _cycle.Head);
      Assert.AreEqual(LevelUtils.MATRIX_SIZE - 1, possiblePath.Length);
      Assert.IsTrue(possiblePath.Direction is DirectionEnum.Up || possiblePath.Direction is DirectionEnum.Left);
    }

    /// <summary>
    /// Поиск величины свободного пути вверх и вправо, если мотоцикл находится 
    /// в первой ячейки последней строки, стены находятся 
    /// в первой ячейке первой строки и в последней ячейке последней
    /// строки матрицы
    /// </summary>
    [TestMethod()]
    public void FindFreeDirectionUpAndRightWallsOnLastCellsOfPathTest()      
    {
      Init();
      _cycle.Head.X = 0;
      _cycle.Head.Y = LevelUtils.MATRIX_SIZE - 1;
      _arena.Matrix[0][0] = (int)CellFillEnum.Wall;
      _arena.Matrix[^1][^1] = (int)CellFillEnum.Wall;
      PossiblePath possiblePath = BestPathFinder.FindBestPath(_arena, _cycle.CurrentDirection, _cycle.Head);
      Assert.AreEqual(LevelUtils.MATRIX_SIZE - 2, possiblePath.Length);
      Assert.IsTrue(possiblePath.Direction is DirectionEnum.Right || possiblePath.Direction is DirectionEnum.Up);
    }

    /// <summary>
    /// Поиск величины свободного пути вниз и влево, если мотоцикл находится 
    /// в последней ячейке первой строки, стены находятся 
    /// в первой ячейке первой строки и в последней ячейке последней
    /// строки матрицы
    /// </summary>
    [TestMethod()]
    public void FindFreeDirectionDownAndLeftWallsOnLastCellsOfPathTest()
    {
      Init();
      _cycle.Head.X = LevelUtils.MATRIX_SIZE - 1;
      _cycle.Head.Y = 0;
      _arena.Matrix[0][0] = (int)CellFillEnum.Wall;
      _arena.Matrix[^1][^1] = (int)CellFillEnum.Wall;
      PossiblePath possiblePath = BestPathFinder.FindBestPath(_arena, _cycle.CurrentDirection, _cycle.Head);
      Assert.AreEqual(LevelUtils.MATRIX_SIZE - 2, possiblePath.Length);
      Assert.IsTrue(possiblePath.Direction is DirectionEnum.Left || possiblePath.Direction is DirectionEnum.Down);
    }

    /// <summary>
    /// Найти наиболее свободный путь,
    /// если мотоцикл находится в первой ячейке
    /// первой строки матрицы, в соседних клетках 
    /// стены, мотоцикл в тупике
    [TestMethod()]
    public void CycleOnFirstXFirstYArenaCoordsInNearbyCellsWalls()
    {
      Init();
      _arena.Matrix[0][1] = (int)CellFillEnum.Wall;
      _arena.Matrix[1][0] = (int)CellFillEnum.Wall;
      PossiblePath possiblePath = BestPathFinder.FindBestPath(_arena, _cycle.CurrentDirection, _cycle.Head);
      Assert.AreEqual(possiblePath, BestPathFinder.DeadEndPath);
    }

    /// <summary>
    /// Проверяет, что изначально направление - null,
    /// а при первом перемещении алгоритмом поиска наилучшего пути, 
    /// устанавливается направление пути вправо или вниз.
    /// Проверяется, что никаких исключений не возникает
    /// </summary>
    [TestMethod()]
    public void InFirstMovingDirectionRightOrDownSetByAIAlgorythmTest()
    {
      Init();
      Assert.IsTrue(_cycle.CurrentDirection == null);
      _cycle.Move(GameModel.EPS);
      Assert.IsTrue(_cycle.CurrentDirection is DirectionEnum.Right || _cycle.CurrentDirection is DirectionEnum.Down);

    }

    /// <summary>
    /// Двигать мотоцикл на две клетки вправо
    /// проверяет изменилась ли координата головы
    /// и количество оставшихся клеток до смены направления
    /// </summary>
    [TestMethod()]
    public void MoveCycleOnTwoCellsRightCheckIsHeadAndCounterRemainedCellsUpdated()
    {
      Init();
      int lengthTestedPath = 2;
      // Небольшое перемещение для первого запуска алгоритма поиска пути
      // и установки значения _counterRemainedCellsInCurrentDirection
      _cycle.Move(GameModel.EPS);
      Assert.AreEqual(DirectionEnum.Right, _cycle.CurrentDirection);
      int initCounterRemainedCells = (int)GetPrivateFieldValue("_counterRemainedCellsInCurrentDirection", _cycle);
      // Время за которое мотоцикл пройдет одну клетку
      float time = 1f / GetCycleSpeed();
      for (int i = 0; i < lengthTestedPath; i++)
      {
        _cycle.Move(time);
      }
      Assert.AreEqual(lengthTestedPath, _cycle.Head.X);

      int lastValueCounterRemainedCells = (int)GetPrivateFieldValue("_counterRemainedCellsInCurrentDirection", _cycle);
      Assert.AreEqual(lengthTestedPath, initCounterRemainedCells - lastValueCounterRemainedCells);
    }

    /// <summary>
    /// Получить значение закрытого поля
    /// </summary>
    /// <param name="parNameField">Имя поля</param>
    /// <param name="parCycleWithAI">Экземпляр, на котором будет получено
    /// значение</param>
    /// <returns>Значение поля</returns>
    private object GetPrivateFieldValue(string parNameField, CycleWithAIDecorator parCycleWithAI)
    {
      return parCycleWithAI.GetType()
       .GetField(parNameField, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
       .GetValue(parCycleWithAI);
    }

    /// <summary>
    /// Сменит ли мотоцикл свое направление через
    /// _counterRemainedCellsInCurrentDirection, значение которого
    /// установлено после первого перемещения
    /// </summary>
    [TestMethod()]
    public void WillCycleChangeDirectionAfterCounterRemainedCellsValueTest()
    {
      Init();
      // Небольшое перемещение для первого запуска алгоритма поиска пути
      // и установки значения _counterRemainedCellsInCurrentDirection
      _cycle.Move(GameModel.EPS);
      Assert.AreEqual(DirectionEnum.Right, _cycle.CurrentDirection);
      int initCounterRemainedCells = (int)GetPrivateFieldValue("_counterRemainedCellsInCurrentDirection", _cycle);
      // Время за которое мотоцикл пройдет одну клетку
      float time = 1f / GetCycleSpeed();
      for (int i = 0; i < initCounterRemainedCells+1; i++)
      {
        _cycle.Move(time);
      }
      Assert.AreNotEqual(DirectionEnum.Right, _cycle.CurrentDirection);      
    }

           
  }
}
