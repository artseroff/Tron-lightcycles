using Model.Records;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;
using View;
using ViewWpf.Records;

namespace ViewWpf.Records
{
  /// <summary>
  /// Представление окна ввода нового рекорда Wpf
  /// </summary>
  public class ViewNewRecordInputWpf : View.Records.ViewNewRecordInputBase, IWpfKeyPressableView
  {

    /// <summary>
    /// Отступ от окна родительской панели
    /// сверху и снизу
    /// </summary>
    private const int MARGIN_PARENT_PANEL_UP_DOWN = 170;

    /// <summary>
    /// Родительская панель
    /// </summary>
    private UniformGrid _parentControl;

    /// <summary>
    /// Текст блок с сообщениями ошибок
    /// </summary>
    private TextBlock _errorTextBlock;

    /// <summary>
    /// Метка с введенным именем
    /// </summary>
    private Label _inputNamelabel;


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parInputModel">Модель ввода нового рекорда</param>
    public ViewNewRecordInputWpf(NewRecordInputModel parInputModel) : base(parInputModel)
    {            
      try
      {
        Window window = SingletonWindow.Instance.Window;
        window.Dispatcher.Invoke(() =>
        {
          _parentControl = new UniformGrid();
          window.Content = _parentControl;
          Init();
          Draw();
        });
      }
      catch (TaskCanceledException)
      {
      }
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Init()
    {
      _parentControl.Margin = new Thickness(0, MARGIN_PARENT_PANEL_UP_DOWN, 0, MARGIN_PARENT_PANEL_UP_DOWN);
      _parentControl.Rows = 4;
      _parentControl.VerticalAlignment = VerticalAlignment.Center;
      _parentControl.HorizontalAlignment = HorizontalAlignment.Center;

      Label infoAboutTimeLabel = new Label
      {
        HorizontalContentAlignment = HorizontalAlignment.Center,
        VerticalContentAlignment = VerticalAlignment.Bottom,
        Foreground = new SolidColorBrush(Colors.White),
        Width = ViewTableRecordsWpf.CELL_WIDTH,
        Content = $"{GAME_TIME_TEXT} {string.Format("{0:f2}", InputModel.Time)}"
      };

      _parentControl.Children.Add(infoAboutTimeLabel);

      Label infoAboutNameLabel = new Label
      {
        HorizontalContentAlignment = HorizontalAlignment.Center,
        VerticalContentAlignment = VerticalAlignment.Center,
        Foreground = new SolidColorBrush(Colors.White),
        Width = ViewTableRecordsWpf.CELL_WIDTH,
        Margin = new Thickness(0,0,0,10),
        Content = INPUT_NAME_TEXT
      };

      _parentControl.Children.Add(infoAboutNameLabel);

      _inputNamelabel = new Label
      {
        HorizontalContentAlignment = HorizontalAlignment.Center,
        VerticalContentAlignment = VerticalAlignment.Top,
        VerticalAlignment = VerticalAlignment.Top,
        Foreground = new SolidColorBrush(Colors.White),
        BorderBrush = new SolidColorBrush(Colors.White),
        BorderThickness = new Thickness(1),
        Width = ViewTableRecordsWpf.CELL_WIDTH,
        Height = 35,
      };
      
      _parentControl.Children.Add(_inputNamelabel);

      _errorTextBlock = new TextBlock();
      _errorTextBlock.Width = ViewTableRecordsWpf.CELL_WIDTH;
      _errorTextBlock.VerticalAlignment = VerticalAlignment.Top;
      _errorTextBlock.Height = 300;
      _errorTextBlock.Foreground = new SolidColorBrush(Colors.White);
      _errorTextBlock.TextWrapping = TextWrapping.Wrap;
      _errorTextBlock.TextAlignment = TextAlignment.Center;
      _parentControl.Children.Add(_errorTextBlock);

      _inputNamelabel.Focus();
    }

    /// <summary>
    /// Нарисовать представление класса
    /// </summary>
    public override void Draw()
    {      
      try
      {
        _parentControl.Dispatcher.Invoke(() =>
        {
          ClearErrors();
          _inputNamelabel.Content = InputModel.InputedName;
        });
      }
      catch (TaskCanceledException)
      {
      }
    }

    /// <summary>
    /// Отобразить ошибки
    /// </summary>
    /// <param name="parErrors">Ошибки</param>
    public override void DrawErrors(string parErrors)
    {
      
      try
      {
        _parentControl.Dispatcher.Invoke(() =>
        {
          _errorTextBlock.Text = parErrors;
        });
      }
      catch (TaskCanceledException)
      {
      }
    }

    /// <summary>
    /// Очистить ошибки
    /// </summary>
    protected override  void ClearErrors()
    {
      _errorTextBlock.Text = null;
    }
  }
}
