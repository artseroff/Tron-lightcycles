using Model.Game;
using Model.Game.Cycles;
using Model.Game.Cycles.Decorator;
using Model.Game.Level;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using View.Game;

namespace ViewWpf.Game
{
  /// <summary>
  /// Wpf представление игры 
  /// </summary>
  public class ViewGameWpf : ViewGameBase, IWpfKeyPressableView
  {

    /// <summary>
    /// Размер ячейки арены
    /// </summary>
    public const int ARENA_CELL_SIZE = 16;

    /// <summary>
    /// Отступ от матрицы арены
    /// </summary>
    public const int MARGIN_ARENA = 3 * ARENA_CELL_SIZE;

    /// <summary>
    /// Число строк грида
    /// </summary>
    private const int GRID_ROWS_COUNT = 3;

    /// <summary>
    /// Число колонок грида
    /// </summary>
    private const int GRID_COLUMNS_COUNT = 3;

    /// <summary>
    /// Отступ влево от прямоугольников - полосок ускорений
    /// </summary>
    private const int MARGIN_LEFT_SPEED_UP_RECTANGLES = 5;

    /// <summary>
    /// Высота прямоугольников - полосок ускорений
    /// </summary>
    private const int HEIGHT_SPEED_UP_RECTANGLE = 24;

    /// <summary>
    /// Ширина прямоугольников - полосок ускорений
    /// </summary>
    private const int WIDTH_SPEED_UP_RECTANGLE = 8;

    /// <summary>
    /// Радиус скруглений прямоугольников - полосок ускорений
    /// </summary>
    private const int RADUIS_SPEED_UP_RECTANGLE = 2;

    /// <summary>
    /// Отступ нижних элементов
    /// </summary>
    private readonly Thickness _bottomMargin = new Thickness(0, 0, 0, 5);

    /// <summary>
    /// Label секундомера
    /// </summary>
    private Label _stopWatchLabel;

    /// <summary>
    /// Холст арены
    /// </summary>
    private Canvas _arenaCanvas;

    /// <summary>
    /// Родительская панель
    /// </summary>
    private Grid _parentControl;

    /// <summary>
    /// Список прямоугольников - полосок ускорений
    /// </summary>
    private List<Rectangle> _speedUpsRectangles;

    /// <summary>
    /// Нижняя панель с ускорением 
    /// </summary>
    private UniformGrid _bottomUniformGrid;

    /// <summary>
    /// Метка с сообщениями о победе, проигрыше,
    /// необходимости нажатия любой кнопки
    /// </summary>
    private Label _pressAnyKeylabel;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parGameModel">Модель игры</param>
    public ViewGameWpf(GameModel parGameModel) : base(parGameModel)
    {
      try
      {
        Window window = SingletonWindow.Instance.Window;
        window.Dispatcher.Invoke(() =>
        {
          Init();
          Draw();
          window.Content = _parentControl;
        });
      }
      catch (TaskCanceledException)
      {
        return;
      }
    }

    /// <summary>
    /// Полная перерисовка окна игры
    /// </summary>
    public override void Draw()
    {
      /*if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
      {
        Window window = SingletonWindow.Instance.Window;
        window.Dispatcher.Invoke(() =>
        {
          
          window.Content = _parentControl;
          base.Draw();
        });
      }
      else
      {
        Window window = SingletonWindow.Instance.Window;
        window.Content = _parentControl;
        base.Draw();
      }*/
      Window window = SingletonWindow.Instance.Window;
      window.Content = _parentControl;
      base.Draw();

    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Init()
    {
      InitGridParentControl();
      InitStopwatchLabelAndArenaCanvas();
      InitBottomBlock();
      InitCycleViews();
    }

    /// <summary>
    /// Инициализация родительской панели как grid
    /// </summary>
    private void InitGridParentControl()
    {
      Window window = SingletonWindow.Instance.Window;
      _parentControl = new Grid();

      _parentControl.Height = window.Height;
      _parentControl.Width = window.Width;
      _parentControl.VerticalAlignment = VerticalAlignment.Center;
      _parentControl.HorizontalAlignment = HorizontalAlignment.Center;
      

      for (int i = 0; i < GRID_ROWS_COUNT; i++)
      {
        _parentControl.RowDefinitions.Add(new RowDefinition { Height = new GridLength(MARGIN_ARENA) });
      }
      _parentControl.RowDefinitions[1].Height = new GridLength(window.Height - 2 * MARGIN_ARENA);

      for (int i = 0; i < GRID_COLUMNS_COUNT; i++)
      {
        _parentControl.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(MARGIN_ARENA) });
      }
      _parentControl.ColumnDefinitions[1].Width = new GridLength(window.Height - 2 * MARGIN_ARENA);
    }

    /// <summary>
    /// Инициализация label секундомера и
    /// холста арены с рамкой
    /// </summary>
    private void InitStopwatchLabelAndArenaCanvas()
    {
      // Секундомер
      _stopWatchLabel = new Label();
      _stopWatchLabel.HorizontalAlignment = HorizontalAlignment.Center;
      _stopWatchLabel.VerticalAlignment = VerticalAlignment.Center;
      _stopWatchLabel.Foreground = new SolidColorBrush(Colors.Cyan);
      _parentControl.Children.Add(_stopWatchLabel);
      Grid.SetRow(_stopWatchLabel, 0);
      Grid.SetColumn(_stopWatchLabel, 1);

      // Арена
      _arenaCanvas = new Canvas();
      _arenaCanvas.Background = new SolidColorBrush(Colors.Black);
      _arenaCanvas.ClipToBounds = true;
      Grid.SetRow(_arenaCanvas, 1);
      Grid.SetColumn(_arenaCanvas, 1);
      _parentControl.Children.Add(_arenaCanvas);

      Brush whiteBrush = new SolidColorBrush(Colors.White);

      // Белая рамка для канваса
      Rectangle rectangleCanvasBorder = new Rectangle();
      rectangleCanvasBorder.HorizontalAlignment = HorizontalAlignment.Stretch;
      rectangleCanvasBorder.VerticalAlignment = VerticalAlignment.Stretch;
      rectangleCanvasBorder.Stroke = whiteBrush;
      rectangleCanvasBorder.Fill = new SolidColorBrush(Colors.Transparent);
      Grid.SetRow(rectangleCanvasBorder, 1);
      Grid.SetColumn(rectangleCanvasBorder, 1);
      _parentControl.Children.Add(rectangleCanvasBorder);

    }

    /// <summary>
    /// Инициализация нижней панели,
    /// с блоком полосок ускорений 
    /// и надписью о проигрыше
    /// </summary>
    private void InitBottomBlock()
    {
      Brush whiteBrush = new SolidColorBrush(Colors.White);
      Brush cyanBrush = new SolidColorBrush(Colors.Cyan);
      // Нижняя панель
      _bottomUniformGrid = new UniformGrid();
      _bottomUniformGrid.Columns = 1;
      _bottomUniformGrid.HorizontalAlignment = HorizontalAlignment.Left;
      _bottomUniformGrid.VerticalAlignment = VerticalAlignment.Stretch;
      _bottomUniformGrid.Margin = _bottomMargin;
      _parentControl.Children.Add(_bottomUniformGrid);
      Grid.SetRow(_bottomUniformGrid, 2);
      Grid.SetColumn(_bottomUniformGrid, 1);

      WrapPanel speedUpBlockWrapPanel = new WrapPanel();
      speedUpBlockWrapPanel.HorizontalAlignment = HorizontalAlignment.Center;
      speedUpBlockWrapPanel.VerticalAlignment = VerticalAlignment.Center;
      _bottomUniformGrid.Children.Add(speedUpBlockWrapPanel);


      Label speedUpLabel = new Label();
      speedUpLabel.Content = SPEED_UP_TEXT;
      speedUpLabel.Foreground = whiteBrush;
      speedUpBlockWrapPanel.Children.Add(speedUpLabel);

      _speedUpsRectangles = new List<Rectangle>();
      Thickness thickness = new Thickness(MARGIN_LEFT_SPEED_UP_RECTANGLES, 0, 0, 0);
      for (int i = 0; i < GameModel.INIT_SPEED_UPS_COUNT; i++)
      {
        Rectangle rectangle = new Rectangle
        {
          Fill = cyanBrush,
          Width = WIDTH_SPEED_UP_RECTANGLE,
          Height = HEIGHT_SPEED_UP_RECTANGLE,
          RadiusX = RADUIS_SPEED_UP_RECTANGLE,
          RadiusY = RADUIS_SPEED_UP_RECTANGLE,
          Margin = thickness

        };
        _speedUpsRectangles.Add(rectangle);
        speedUpBlockWrapPanel.Children.Add(rectangle);
      }      

    }

    /// <summary>
    /// Инициализация представлений мотоциклов
    /// </summary>
    protected override void InitCycleViews()
    {
      foreach (ICycle elCycle in GameModel.Cycles)
      {
        CycleViews.Add(new ViewCycleWpf(elCycle, _arenaCanvas));
      }
    }

    /// <summary>
    /// Перерисовать матрицу арены
    /// </summary>
    protected override void RedrawArena()
    {
      int[][] matrix = GameModel.Arena.Matrix;
      for (int y = 0; y < matrix.Length; y++)
      {
        for (int x = 0; x < matrix[0].Length; x++)
        {
          int cellFill = matrix[y][x]; 
          // Только стены
          if (cellFill == (int)CellFillEnum.Wall)
          {
            Rectangle cellRectangle = new Rectangle();
            cellRectangle.Width = ARENA_CELL_SIZE;
            cellRectangle.Height = ARENA_CELL_SIZE;
            cellRectangle.Fill = WpfBrushColorByCellFillCreator.GetBrushByCellFill((CellFillEnum)cellFill);
            _arenaCanvas.Children.Add(cellRectangle);
            Canvas.SetTop(cellRectangle, y * ARENA_CELL_SIZE);
            Canvas.SetLeft(cellRectangle, x * ARENA_CELL_SIZE);
          }
        }
      }
    }

    /// <summary>
    /// Нарисовать секундомер
    /// </summary>
    protected override void DrawStopWatch()
    {
      _stopWatchLabel.Content = string.Format("{0:f2}", GameModel.StopwatchValue);
    }

    /// <summary>
    /// Нарисовать блок с ускорением
    /// </summary>
    protected override void UpdateSpeedUpBlock()
    {
      Rectangle rectangle = _speedUpsRectangles[GameModel.LeftUserSpeedUps];
      rectangle.Fill = new SolidColorBrush(Colors.Transparent);
      rectangle.Stroke = new SolidColorBrush(Colors.White);
    }

    /// <summary>
    /// Нарисовать сообщение о проигрыше
    /// </summary>
    public override void DrawLossMessage()
    {      
      DrawPressAnyKeyMessage();
      try
      {
        _pressAnyKeylabel?.Dispatcher.Invoke(() =>
        {
          _pressAnyKeylabel.Content = $"{GAME_LOSS_TEXT}. {PRESS_ANY_KEY_TEXT}";
        });
      }
      catch (TaskCanceledException)
      {
        return;
      }
    }

    /// <summary>
    /// Нарисовать сообщение о победе
    /// </summary>
    public override void DrawWinMessage()
    {
      DrawPressAnyKeyMessage();
      try
      {
        _pressAnyKeylabel?.Dispatcher.Invoke(() =>
        {
          _pressAnyKeylabel.Content = $"{GAME_WIN_TEXT}. {PRESS_ANY_KEY_TEXT}";
        });
      }
      catch (TaskCanceledException)
      {
        return;
      }
    }

    /// <summary>
    /// Отобразить текст о необходимости нажатия любой кнопки
    /// </summary>
    public override void DrawPressAnyKeyMessage()
    {
      try
      {
        _parentControl.Dispatcher.Invoke(() =>
        {           
          _parentControl.Children.Remove(_bottomUniformGrid);
          _pressAnyKeylabel = new Label();
          _parentControl.Children.Add(_pressAnyKeylabel);
          _pressAnyKeylabel.HorizontalAlignment = HorizontalAlignment.Center;
          _pressAnyKeylabel.VerticalAlignment = VerticalAlignment.Center;
          _pressAnyKeylabel.Foreground = new SolidColorBrush(Colors.White);
          _pressAnyKeylabel.HorizontalContentAlignment = HorizontalAlignment.Center;
          _pressAnyKeylabel.Content = PRESS_ANY_KEY_TEXT;
          _pressAnyKeylabel.Margin = _bottomMargin;
          Grid.SetRow(_pressAnyKeylabel, 2);
          Grid.SetColumn(_pressAnyKeylabel, 1);
          Grid.SetColumnSpan(_pressAnyKeylabel, 2);
        });
      }
      catch (TaskCanceledException)
      {
        return;
      }
    }


    /// <summary>
    /// Нарисовать двигающиеся объекты
    /// </summary>
    protected override void DrawMovedObjects()
    {
      try
      {
        _parentControl.Dispatcher.Invoke(() =>
        {
          base.DrawMovedObjects();
        });
      }
      catch (TaskCanceledException)
      {
        return;
      }
    }
  }
}
