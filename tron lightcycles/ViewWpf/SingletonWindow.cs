using Model.Game;
using Model.Game.Level;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ViewWpf.Game;

namespace ViewWpf
{
  /// <summary>
  /// Глобальное окно wpf 
  /// (Синглтон с синхронизацией,
  /// используется Double Checked Locking)
  /// </summary>
  public class SingletonWindow
  {
    /// <summary>
    /// Объект синхронизации
    /// </summary>
    private static readonly object _syncRoot = new object();

    /// <summary>
    /// Экземпляр класса
    /// </summary>
    private static SingletonWindow _instance;

    /// <summary>
    /// Экземпляр окна
    /// </summary>
    public Window Window { get; private set; }

    /// <summary>
    /// Свойство экземпляр класса
    /// </summary>
    public static SingletonWindow Instance
    {
      get
      {
        if (_instance == null)
        {
          lock (_syncRoot)
          {
            if (_instance == null)
              _instance = new SingletonWindow();
          }
        }
        return _instance;
      }
    }

    /// <summary>
    /// Закрытый конструктор
    /// </summary>
    private SingletonWindow()
    {
      Window = new Window()
      {
        //39*16+по бокам смещение
        Width = LevelUtils.MATRIX_SIZE * ViewGameWpf.ARENA_CELL_SIZE + 2 * ViewGameWpf.MARGIN_ARENA,

        Height = LevelUtils.MATRIX_SIZE * ViewGameWpf.ARENA_CELL_SIZE + 2 * ViewGameWpf.MARGIN_ARENA,
        //ResizeMode = ResizeMode.NoResize,
        WindowStyle = WindowStyle.None,
        FontSize = 16,
        //FontSize = 14,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        WindowStartupLocation = WindowStartupLocation.CenterScreen,
        Background = new SolidColorBrush(Colors.Black),
        ShowActivated = true
      };

      Window.Show();
    }

  }
}
