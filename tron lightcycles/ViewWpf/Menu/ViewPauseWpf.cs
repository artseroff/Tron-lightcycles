using Model.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using View.Menu;

namespace ViewWpf.Menu
{
  /// <summary>
  /// Представление окна игровой паузы Wpf
  /// </summary>
  public class ViewPauseWpf : ViewPauseMenuBase, IWpfKeyPressableView
  {
    /// <summary>
    /// Число колонок таблицы
    /// </summary>
    private const int GRID_COLUMN_COUNT = 2;

    /// <summary>
    /// Число строк таблицы
    /// </summary>
    private const int GRID_ROWS_COUNT = 2;

    /// <summary>
    /// Ширина ячейки таблицы
    /// </summary>
    private const int GRID_CELL_WIDTH = 120;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenu">Меню паузы</param>
    public ViewPauseWpf(PauseMenu parMenu) : base(parMenu)
    {

      Init();
      Draw();
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Init()
    {
      Window window = SingletonWindow.Instance.Window;
      window.Content = null;
      Grid parentControl = new Grid();
      parentControl.VerticalAlignment = VerticalAlignment.Center;
      parentControl.HorizontalAlignment = HorizontalAlignment.Center;


      for (int j = 0; j < GRID_ROWS_COUNT; j++)
      {
        parentControl.RowDefinitions.Add(new RowDefinition());
      }

      for (int j = 0; j < GRID_COLUMN_COUNT; j++)
      {
        parentControl.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(GRID_CELL_WIDTH) });
      }
      

      TextBlock textBlock = new TextBlock();
      textBlock.Text = PAUSE_TEXT;
      textBlock.Foreground = new SolidColorBrush(Colors.Cyan);      
      textBlock.TextWrapping = TextWrapping.Wrap;
      textBlock.TextAlignment = TextAlignment.Center;
      Grid.SetColumnSpan(textBlock, 2);
      parentControl.Children.Add(textBlock);

      int columnCounter = 0;
      foreach (ViewMenuItemBase elItem in Items)
      {
        ((ViewMenuItemWpf)elItem).SetParentControl(parentControl);
        
        Grid.SetRow(((ViewMenuItemWpf)elItem).ItemLabel, GRID_ROWS_COUNT);
        Grid.SetColumn(((ViewMenuItemWpf)elItem).ItemLabel, columnCounter++);
      }

      window.Content = parentControl;

    }

    /// <summary>
    /// Создать представление пункта меню
    /// </summary>
    /// <param name="parItem">Модель пункта меню</param>
    /// <returns>Представление пункта меню</returns>
    protected override ViewMenuItemBase CreateMenuItem(Model.Menu.MenuItem parItem)
    {
      return new ViewMenuItemWpf(parItem);
    }
  }
}
