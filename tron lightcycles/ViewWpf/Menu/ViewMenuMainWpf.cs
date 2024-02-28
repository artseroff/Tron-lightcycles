using Model.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using View.Menu;

namespace ViewWpf.Menu
{
  /// <summary>
  /// Wpf представление главного меню
  /// </summary>
  public class ViewMenuMainWpf : ViewMenuBase, IWpfKeyPressableView
  {
    /// <summary>
    /// Метка названия игры
    /// </summary>
    private Label _labelGameName;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenu">Циклическое меню</param>
    public ViewMenuMainWpf(CyclicMenu parMenu) : base(parMenu)
    {
      if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
      {
        
        try
        {
          SingletonWindow.Instance.Window.Dispatcher.Invoke(() => {
            Init();
            Draw();
          });
        }
        catch (TaskCanceledException)
        {
          return;
        }
      } 
      else
      {
        Init();
        Draw();
      }
      
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Init()
    {
      Window window = SingletonWindow.Instance.Window;
      window.Content = null;      
      Panel parentControl = new StackPanel();

      parentControl.VerticalAlignment = VerticalAlignment.Center;
      parentControl.HorizontalAlignment = HorizontalAlignment.Center;

      InitGameName();

      parentControl.Children.Add(_labelGameName);
      foreach (ViewMenuItemBase elItem in Items)
      {
        ((ViewMenuItemWpf)elItem).SetParentControl(parentControl);
      }

      window.Content = parentControl;
    }

    /// <summary>
    /// Нарисовать представление класса
    /// </summary>
    public override void Draw()
    {
      if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
      {
        
        try
        {
          SingletonWindow.Instance.Window.Dispatcher.Invoke(() =>
          {
            base.Draw();
          });
        }
        catch (TaskCanceledException)
        {
          return;
        }

      }
      else
      {
        base.Draw();
      }
        
    }

    /// <summary>
    /// Инициализация метки навания игры
    /// </summary>
    public void InitGameName()
    {
      _labelGameName = new Label
      {
        Foreground = new SolidColorBrush(Colors.Cyan),
        FontSize = 150,
        FontStyle = FontStyles.Italic,
        FontWeight = FontWeights.Light,
        FontFamily = new FontFamily("Bauhaus 93"),
        // GameName не в модели, так как текст от вью зависит
        Content = Properties.Resources.GameName
      };      
    } 

    /// <summary>
    /// Закрыть окно
    /// </summary>
    public void Close()
    {
      SingletonWindow.Instance.Window.Close();
    }

    

    /// <summary>
    /// Создать представление пункта меню
    /// </summary>
    /// <param name="parItem">Модель пункта меню</param>
    /// <returns>Представление пункта меню</returns>
    protected override ViewMenuItemBase CreateMenuItem(Model.Menu.MenuItem parItem)
    {
      ViewMenuItemWpf viewMenuItemWpf = null; 
      if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
      {
        
        try
        {
          SingletonWindow.Instance.Window.Dispatcher.Invoke(() =>
          {
            viewMenuItemWpf = new ViewMenuItemWpf(parItem);
          });
        }
        catch (TaskCanceledException)
        {
          
        }

      }
      else
      {
        viewMenuItemWpf = new ViewMenuItemWpf(parItem);
      }
      return viewMenuItemWpf;

    }
  }
}
