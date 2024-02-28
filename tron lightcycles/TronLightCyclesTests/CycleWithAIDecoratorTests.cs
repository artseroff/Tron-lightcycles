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
  /// ����� ���������� ��������� � ��
  /// (� ���������� ������ �������� 
  /// ���������� �����������)
  /// </summary>
  [TestClass()]
  public class CycleWithAIDecoratorTests
  {   

    /// <summary>
    /// ��������
    /// </summary>
    private CycleWithAIDecorator _cycle;

    /// <summary>
    /// �����
    /// </summary>
    private Arena _arena;


    /// <summary>
    /// �����������
    /// </summary>
    public CycleWithAIDecoratorTests()
    {            
    }

    /// <summary>
    /// �������� �������� ConcreteCycle � ������� � ���
    /// </summary>
    /// <returns>�������� ConcreteCycle � ������� � ���</returns>
    private float GetCycleSpeed()
    {
      ConcreteCycle concreteCycle = (ConcreteCycle)typeof(CycleWithAIDecorator)
        .BaseType.GetProperty("Cycle", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
        .GetValue(_cycle);
      
      float speed = ConcreteCycleTests.GetCycleSpeed(concreteCycle);
      return speed;
    }

    /// <summary>
    /// ������������ ��������� �� ����� ������� ������
    /// �� ����������� 0;0
    /// </summary>
    private void Init()
    {
      // ���������� ���������� �� localCycles �� �������� � ������� �����
      LevelUtils.FormArenaAndCyclesFromFile(1, out Arena localArena, out List<ICycle> localCycles);
      _arena = localArena;     
      _cycle = new CycleWithAIDecorator(new ConcreteCycle(1, new Model.Game.Point(0,0), localArena));      
     
    }

    /// <summary>
    /// ����� �������� ���������� ���� ������ � ����,
    /// ���� �������� ��������� � ������ ������
    /// ������ ������ � ��� ����������� �� �����, 
    /// ����� �� ������
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
    /// ����� �������� ���������� ���� ����� � �����,
    /// ���� �������� ��������� � ��������� ������
    /// ��������� ������ � ��� ����������� �� �����, 
    /// ����� �� ������
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
    /// ����� �������� ���������� ���� ����� � ������, ���� �������� ��������� 
    /// � ������ ������ ��������� ������, ����� ��������� 
    /// � ������ ������ ������ ������ � � ��������� ������ ���������
    /// ������ �������
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
    /// ����� �������� ���������� ���� ���� � �����, ���� �������� ��������� 
    /// � ��������� ������ ������ ������, ����� ��������� 
    /// � ������ ������ ������ ������ � � ��������� ������ ���������
    /// ������ �������
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
    /// ����� �������� ��������� ����,
    /// ���� �������� ��������� � ������ ������
    /// ������ ������ �������, � �������� ������� 
    /// �����, �������� � ������
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
    /// ���������, ��� ���������� ����������� - null,
    /// � ��� ������ ����������� ���������� ������ ���������� ����, 
    /// ��������������� ����������� ���� ������ ��� ����.
    /// �����������, ��� ������� ���������� �� ���������
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
    /// ������� �������� �� ��� ������ ������
    /// ��������� ���������� �� ���������� ������
    /// � ���������� ���������� ������ �� ����� �����������
    /// </summary>
    [TestMethod()]
    public void MoveCycleOnTwoCellsRightCheckIsHeadAndCounterRemainedCellsUpdated()
    {
      Init();
      int lengthTestedPath = 2;
      // ��������� ����������� ��� ������� ������� ��������� ������ ����
      // � ��������� �������� _counterRemainedCellsInCurrentDirection
      _cycle.Move(GameModel.EPS);
      Assert.AreEqual(DirectionEnum.Right, _cycle.CurrentDirection);
      int initCounterRemainedCells = (int)GetPrivateFieldValue("_counterRemainedCellsInCurrentDirection", _cycle);
      // ����� �� ������� �������� ������� ���� ������
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
    /// �������� �������� ��������� ����
    /// </summary>
    /// <param name="parNameField">��� ����</param>
    /// <param name="parCycleWithAI">���������, �� ������� ����� ��������
    /// ��������</param>
    /// <returns>�������� ����</returns>
    private object GetPrivateFieldValue(string parNameField, CycleWithAIDecorator parCycleWithAI)
    {
      return parCycleWithAI.GetType()
       .GetField(parNameField, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
       .GetValue(parCycleWithAI);
    }

    /// <summary>
    /// ������ �� �������� ���� ����������� �����
    /// _counterRemainedCellsInCurrentDirection, �������� ��������
    /// ����������� ����� ������� �����������
    /// </summary>
    [TestMethod()]
    public void WillCycleChangeDirectionAfterCounterRemainedCellsValueTest()
    {
      Init();
      // ��������� ����������� ��� ������� ������� ��������� ������ ����
      // � ��������� �������� _counterRemainedCellsInCurrentDirection
      _cycle.Move(GameModel.EPS);
      Assert.AreEqual(DirectionEnum.Right, _cycle.CurrentDirection);
      int initCounterRemainedCells = (int)GetPrivateFieldValue("_counterRemainedCellsInCurrentDirection", _cycle);
      // ����� �� ������� �������� ������� ���� ������
      float time = 1f / GetCycleSpeed();
      for (int i = 0; i < initCounterRemainedCells+1; i++)
      {
        _cycle.Move(time);
      }
      Assert.AreNotEqual(DirectionEnum.Right, _cycle.CurrentDirection);      
    }

           
  }
}
