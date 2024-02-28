using Model.Records;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ViewWpf.Records
{
  /// <summary>
  /// Wpf представление таблицы рекордов
  /// </summary>
  public class ViewTableRecordsWpf : View.Records.ViewTableRecordsBase, IWpfKeyPressableView
  {
    /// <summary>
    /// Ширина ячейки
    /// </summary>
    public const int CELL_WIDTH = 200;

    /// <summary>
    /// Родительская панель окна
    /// </summary>
    private Panel _parentControl;

    /// <summary>
    /// Белая кисть
    /// </summary>
    private Brush _whiteBrush = new SolidColorBrush(Colors.White);

    /// <summary>
    /// Конструктор
    /// </summary>
    public ViewTableRecordsWpf() : base()
    {
      Init();
      Draw();
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    public void Init()
    {
      Window window = SingletonWindow.Instance.Window;
      _parentControl = new WrapPanel();

      _parentControl.VerticalAlignment = VerticalAlignment.Center;
      _parentControl.HorizontalAlignment = HorizontalAlignment.Center;
      window.Content = _parentControl;
    }

    /// <summary>
    /// Нарисовать представление класса
    /// </summary>    
    public override void Draw()
    {
      if (base.Records.Count == 0)
      {
        DrawTitleNoRecords();
        return;
      }
      _parentControl.Width = 2 * CELL_WIDTH;
      Thickness borderNameCell = new Thickness(1, 1, 0, 0);
      Thickness borderTimeCell = new Thickness(1, 1, 1, 0);

      // Шапка
      Label columnName = FormLabelWithContent(LEFT_COL_NAME, borderNameCell);
      _parentControl.Children.Add(columnName);

      Label columnTime = FormLabelWithContent(RIGHT_COL_NAME, borderTimeCell);
      _parentControl.Children.Add(columnTime);

      // Основное содержимое таблицы

      for (int i = 0; i < Records.Count; i++)
      {
        // Нижняя граница подвала
        if (i == Records.Count - 1)
        {
          borderNameCell.Bottom = 1;
          borderTimeCell.Bottom = 1;
        }
        Label labelName = FormLabelWithContent(Records[i].Name, borderNameCell);
        _parentControl.Children.Add(labelName);

        Label labelTime = FormLabelWithContent(string.Format("{0:f2}", Records[i].Time), borderTimeCell);
        _parentControl.Children.Add(labelTime);
      }


    }

    /// <summary>
    /// Возвращает новый Label с заданными параметрами
    /// </summary>
    /// <param name="parText">Текст</param>
    /// <param name="parThickness">Толщина границ</param>
    /// <returns>Label с заданными параметрами</returns>
    private Label FormLabelWithContent(String parText, Thickness parThickness)
    {
      Label label = new Label
      {
        Width = CELL_WIDTH,
        Content = parText,
        HorizontalContentAlignment = HorizontalAlignment.Center,
        Foreground = _whiteBrush,
        BorderBrush = _whiteBrush,
        BorderThickness = parThickness
      };
      return label;
    }

    /// <summary>
    /// Нарисовать заголовок об отсутствии рекордов
    /// </summary>
    protected override void DrawTitleNoRecords()
    {
      _parentControl.Children.Add(new Label { Content = NO_RECORDS, Foreground = _whiteBrush });
    }

  }
}
