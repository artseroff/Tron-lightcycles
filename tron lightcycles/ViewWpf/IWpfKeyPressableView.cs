using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ViewWpf
{
  /// <summary>
  /// Интерфейс представления окна Wpf, принимающее нажатие клавиш
  /// </summary>
  public interface IWpfKeyPressableView
  {

    /// <summary>
    /// Подписаться на событие нажатия кнопки
    /// </summary>
    /// <param name="parOnKeyPressed">Метод, обрабатывающий нажатия кнопок</param>
    public void SubscribeOnKeyPressed(KeyEventHandler parOnKeyPressed)
    {
      Window window = SingletonWindow.Instance.Window;
      window.KeyDown += parOnKeyPressed;
    }

    /// <summary>
    /// Отписаться от события нажатия кнопки
    /// </summary>
    /// <param name="parOnKeyPressed">Метод, обрабатывающий нажатия кнопок</param>
    public void UnSubscribeOnKeyPressed(KeyEventHandler parOnKeyPressed)
    {
      Window window = SingletonWindow.Instance.Window;
      window.KeyDown -= parOnKeyPressed;
    }

  }
}
