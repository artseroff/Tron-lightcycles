using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Game;
using Model.Game.Cycles;
using Model.Game.Direction;
using Model.Game.Level;
using System;
using System.Collections.Generic;
using System.Text;

namespace TronLightCyclesTests
{
  /// <summary>
  /// Тесты конкретного мотоцикла
  /// </summary>
  [TestClass()]
  public class ConcreteCycleTests
  {

    /// <summary>
    /// Мотоцикл
    /// </summary>
    private ConcreteCycle _cycle;

    /// <summary>
    /// Арена
    /// </summary>
    private Arena _arena;

    /// <summary>
    /// (Статический) Получить скорость ConcreteCycle в клетках в сек
    /// </summary>
    /// <returns>Скорость ConcreteCycle в клетках в сек</returns>
    public static float GetCycleSpeed(ConcreteCycle parConcreteCycle)
    {
      float speed = (float)typeof(ConcreteCycle)
        .GetField("SPEED", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
        .GetValue(parConcreteCycle);
      return speed;
    }

    /// <summary>
    /// (Получает по объекту этого класса)
    /// Получить скорость ConcreteCycle в клетках в сек
    /// </summary>
    /// <returns>Скорость ConcreteCycle в клетках в сек</returns>
    private float GetCycleSpeed()
    {
      return GetCycleSpeed(_cycle);
    }

    /// <summary>
    /// Инициализация мотоцикла на арене первого уровня
    /// на координатах 0;0
    /// </summary>
    private void Init()
    {
      // Координаты мотоциклов из localCycles не отмечены в матрице арены
      LevelUtils.FormArenaAndCyclesFromFile(1, out Arena localArena, out List<ICycle> localCycles);
      _arena = localArena;
      _cycle = new ConcreteCycle(1, new Point(0, 0), localArena, DirectionEnum.Right);

    }    

    /// <summary>
    /// Тест, передающий в конструктор ConcreteCycle
    /// null Arena.
    /// Ожидает исключение ArgumentNullException
    /// </summary>    
    [TestMethod()]
    [ExpectedException(typeof(ArgumentNullException),
    "Арена не должна быть null")]
    public void NullArenaInConstructorTest()
    {
      Init();
      new ConcreteCycle(1, new Point(-1, 1), null);
    }

    /// <summary>
    /// Тест, передающий в конструктор ConcreteCycle
    /// null Point как координату головы.
    /// Ожидает исключение ArgumentNullException
    /// </summary>    
    [TestMethod()]
    [ExpectedException(typeof(ArgumentNullException),
    "Координата головы не должна быть null")]
    public void NullHeadPointInConstructorTest()
    {
      Init();
      new ConcreteCycle(1, null, _arena);
    }

    /// <summary>
    /// Тест, передающий в конструктор ConcreteCycle
    /// координаты вне матрицы арены.
    /// Ожидает исключение ArgumentException
    /// </summary>    
    [TestMethod()]
    [ExpectedException(typeof(ArgumentException),
    "Координаты вне матрицы арены")]
    public void PointOutsideArenaMatrixInConstructorTest()
    {
      Init();
      new ConcreteCycle(1, new Point(-1, 1), _arena);
    }

    /// <summary>
    /// Параметризированный тест,
    /// передающий в конструктор ConcreteCycle
    /// отрицательный или нулевой Id.
    /// Ожидает исключение ArgumentException
    /// </summary>
    [DataRow(-5)]
    [DataRow(-1)]
    [DataRow(0)]
    [DataTestMethod]
    [ExpectedException(typeof(ArgumentException),
    "Id должно быть больше 0")]
    public void NegativeOrZeroIdInConstructorTest(int parId)
    {
      Init();
      new ConcreteCycle(parId, new Point(0, 0), _arena);
    }

    /// <summary>
    /// Тест, создающий ConcreteCycle без указания
    /// начального направления.
    /// Ожидает исключение InvalidOperationException в методе Move
    /// </summary>    
    [TestMethod()]
    [ExpectedException(typeof(InvalidOperationException),
    "CurrentDirection должно быть присвоено" +
          " значение, перед вызовом Move()")]
    public void MoveToNullDirectionTest()
    {
      Init();
      //Объект мотоцикла без указания начального направления.
      //Такие объекты используются для оборачивания декоратором мотоцикл с ИИ
      ConcreteCycle localCycle = new ConcreteCycle(1, new Point(0, 0), _arena);
      localCycle.Move(1);
    }

    /// <summary>
    /// Установить мотоциклу противоположное направление
    /// </summary>
    [TestMethod()]
    public void SetOppositeDirectionTest()
    {
      Init();
      ConcreteCycle localCycle = new ConcreteCycle(1, new Point(2, 2), _arena, DirectionEnum.Up);
      DirectionEnum initDirection = (DirectionEnum)localCycle.CurrentDirection;      
      _cycle.CheckAndSetNewDirection((DirectionEnum)DirectionUtils.OppositeDirection(initDirection));
      Assert.AreEqual(initDirection, localCycle.CurrentDirection);    
    }



    /// <summary>
    /// Двигать мотоцикл на одну клетку вправо
    /// проверяет изменилась ли координата головы
    /// и попала ли прошлая координата головы в след
    /// </summary>
    [TestMethod()]
    public void MoveCycleOnOneCellRightCheckIsHeadAndTrailUpdatedTest()
    {
      Init();
      // Время за которое мотоцикл пройдет одну клетку
      float time = 1f / GetCycleSpeed();
      _cycle.Move(time);
      Assert.AreEqual(1, _cycle.Head.X);
      Assert.AreEqual(new Point(0, 0), _cycle.Trail.Peek());
    }

    /// <summary>
    /// Двигать мотоцикл на одну клетку вниз.
    /// Проверяет равно ли частичное перемещение мотоцикла
    /// по Y = 0 и флаг переехал ли мотоцикл в новую клетку
    /// </summary>
    [TestMethod()]
    public void MoveCycleOnOneCellDownCheckFlagIsMovedInNewCellTest()
    {
      Init();
      _cycle.CheckAndSetNewDirection(DirectionEnum.Down);
      // Время за которое мотоцикл пройдет одну клетку
      float time = 1f / GetCycleSpeed();
      _cycle.Move(time);
      Assert.IsTrue(_cycle.PartCellY < GameModel.EPS);
      Assert.IsTrue(_cycle.IsCycleMovedInNewCell);
    }

    /// <summary>
    /// Двигать мотоцикл на одну клетку вправо.
    /// Проверяет равно ли частичное перемещение мотоцикла
    /// по Y = 0 и флаг переехал ли мотоцикл в новую клетку
    /// </summary>
    [TestMethod()]
    public void MoveCycleOnOneCellRightCheckFlagIsMovedInNewCellTest()
    {
      Init();
      _cycle.CheckAndSetNewDirection(DirectionEnum.Right);
      // Время за которое мотоцикл пройдет одну клетку
      float time = 1f / GetCycleSpeed();
      _cycle.Move(time);
      Assert.IsTrue(_cycle.PartCellX < GameModel.EPS);
      Assert.IsTrue(_cycle.IsCycleMovedInNewCell);
    }

    /// <summary>
    /// Проверяет добавление координат в след,
    /// проверяет добавленные точки в следе мотоцикла
    /// и то, что в матрице эти координаты помечены как след
    /// </summary>
    [TestMethod()]
    public void CheckAddingPointsToTrail()
    {
      Init();
      int lengthTestedPath = 10;
      // Время за которое мотоцикл пройдет одну клетку
      float time = (1f / GetCycleSpeed());

      for (int i = 0; i < lengthTestedPath; i++)
      {
        _cycle.Move(time);
      }

      Assert.AreEqual(lengthTestedPath, _cycle.Trail.Count);

      List<Point> trailList = new List<Point>(_cycle.Trail);
      bool properPoints = true;
      for (int i = 0; i < lengthTestedPath; i++)
      {
        // Заполнитель ячейки матрицы не соответствует id объекта _cycle
        if (LevelUtils.GetCycleIdByCellFiller((CellFillEnum)_arena.Matrix[0][i]) != _cycle.Id
          // и в следе мотоцикла лежит правильная координата
          && !trailList[trailList.Count - (i+1)].Equals(new Point(i,0)))
        {
          properPoints = false;
        }
      }
      Assert.IsTrue(properPoints);
    }

    /// <summary>
    /// Создать аварию мотоцикла на X координате,
    /// равной значению parLengthTestedPath
    /// </summary>
    /// <param name="parLengthTestedPath">X координата стены</param>
    private void CrashCycleInWall(int parLengthTestedPath)
    {
      Init();
      _arena.Matrix[0][parLengthTestedPath] = (int)CellFillEnum.Wall;
      // Время за которое мотоцикл пройдет одну клетку
      float time = (1f / GetCycleSpeed());

      for (int i = 0; i < parLengthTestedPath; i++)
      {
        _cycle.Move(time);
      }
    }

    /// <summary>
    /// Тест разобъется ли мотоцикл при аварии в стену
    /// </summary>
    [TestMethod()]
    public void CrashCycleInWallTest()
    {
      int lengthTestedPath = 2;
      CrashCycleInWall(lengthTestedPath);

      int lastCyclePoint = lengthTestedPath - 1;
      Assert.AreEqual(lastCyclePoint, _cycle.Head.X);
      Assert.IsTrue(_cycle.IsCrashed);
    }

    /// <summary>
    /// Тест почищены ли координаты мотоцикла в матрице арены после аварии
    /// </summary>
    [TestMethod()]
    public void CheckIsNoChrashedCyclePointsOnArenaMatrixTest()
    {
      int lengthTestedPath = 5;
      CrashCycleInWall(lengthTestedPath);

      bool noCycleCoords = true;
      for (int i = lengthTestedPath - 1; i >= 0; i--)
      {
        if (_arena.Matrix[0][i] != 0)
        {
          noCycleCoords = false;
          break;
        }
      }
      Assert.IsTrue(noCycleCoords);
    }

    /// <summary>
    /// Тест будет ли перемещаться разбитый мотоцикл
    /// </summary>
    [TestMethod()]
    public void CheckIsChrashedCycleMoveTest()
    {
      int lengthTestedPath = 2;
      CrashCycleInWall(lengthTestedPath);
      int headXBeforeTryMoving = _cycle.Head.X;
      // Время за которое мотоцикл пройдет одну клетку
      float time = (1f / GetCycleSpeed());

      for (int i = 0; i < lengthTestedPath; i++)
      {
        _cycle.Move(time);
      }

      Assert.AreEqual(headXBeforeTryMoving, _cycle.Head.X);
    }

    /// <summary>
    /// Создать аварию мотоцикла с нижней границей арены
    /// </summary>
    [TestMethod()]
    public void CrashCycleInBottomArenaBorderTest()
    {
      Init();
      _cycle.CheckAndSetNewDirection(DirectionEnum.Down);
      // Время за которое мотоцикл пройдет одну клетку
      float time = (1f / GetCycleSpeed());
      for (int i = 0; i < _arena.Matrix.Length+1; i++)
      {
        _cycle.Move(time);
      }
      Assert.IsTrue(_cycle.IsCrashed);
    }

  }
}
