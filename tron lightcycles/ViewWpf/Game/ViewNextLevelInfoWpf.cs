using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using View.Game;

namespace ViewWpf.Game
{
  /// <summary>
  /// Представление перехода
  /// на следующий уровень Wpf
  /// </summary>
  public class ViewNextLevelInfoWpf : View.Game.ViewNextLevelInfoBase, IWpfKeyPressableView
  {
    /// <summary>
    /// Родительская панель
    /// </summary>
    private Panel _parentControl;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parLevel">Номер уровня</param>
    public ViewNextLevelInfoWpf(int parLevel) : base(parLevel)
    {
      try
      {
        Window window = SingletonWindow.Instance.Window;
        window.Dispatcher.Invoke(() =>
        {
          _parentControl = new StackPanel();
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
    /// Нарисовать представление класса
    /// </summary>
    public override void Draw()
    {
      TextBlock label = new TextBlock();
      label.Text = $"{LEVEL_TEXT} {Level}\n{ViewGameBase.PRESS_ANY_KEY_TEXT}";
      label.Foreground = new SolidColorBrush(Colors.White);
      label.TextAlignment = TextAlignment.Center;
      _parentControl.Children.Add(label);
      _parentControl.HorizontalAlignment = HorizontalAlignment.Center;
      _parentControl.VerticalAlignment = VerticalAlignment.Center;
    }
  }
}
