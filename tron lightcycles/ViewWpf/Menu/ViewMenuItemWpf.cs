using Model.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using View.Menu;

namespace ViewWpf.Menu
{
  /// <summary>
  /// Wpf представление пункта меню
  /// </summary>
  public class ViewMenuItemWpf : ViewMenuItemBase
  {

    /// <summary>
    /// Отступ вниз от пункта меню
    /// </summary>
    private const int MARGIN_DOWN = 10;
    
    /// <summary>
    /// Родительская панель окна
    /// </summary>
    private Panel _parentControl;

    /// <summary>
    /// Метка с названием пункта меню
    /// </summary>
    public Label ItemLabel { get; private set; }


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenuItemBase">Модель пункта меню</param>
    public ViewMenuItemWpf(Model.Menu.MenuItem parMenuItemBase) : base(parMenuItemBase)
    {      
      ItemLabel = new Label
      {
        Content = parMenuItemBase.Name
      };

      parMenuItemBase.NeedRedraw += Draw;
    }

    /// <summary>
    /// Нарисовать представление класса
    /// </summary>
    public override void Draw()
    {
            
      SolidColorBrush labelTextBrush = null;
      switch (this.MenuItem.Status)
      {
        case Model.Menu.MenuItemStatuses.Focused:
        case Model.Menu.MenuItemStatuses.Selected:
          Console.ForegroundColor = ConsoleColor.White;
          labelTextBrush = new SolidColorBrush(Colors.White);
          break;
        case Model.Menu.MenuItemStatuses.None:
          labelTextBrush = new SolidColorBrush(Colors.Cyan);
          break;
      }

      ItemLabel.Margin = new Thickness(0, MARGIN_DOWN, 0, 0);
      ItemLabel.Foreground = labelTextBrush;
      ItemLabel.VerticalContentAlignment = VerticalAlignment.Center;
      ItemLabel.HorizontalAlignment = HorizontalAlignment.Center;

    }

    /// <summary>
    /// Установить родительский элемент управления
    /// </summary>
    /// <param name="parControl">Родительская панель</param>
    public void SetParentControl(Panel parControl)
    {
      if (_parentControl == null)
      {
        _parentControl = parControl;
        parControl.Children.Add(ItemLabel);
      }
    }

  }
}
