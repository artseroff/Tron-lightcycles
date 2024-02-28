using Controller;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ControllerWpf
{
  /// <summary>
  /// Базовый wpf контроллер
  /// </summary>
  public abstract class WpfControllerBase : IController
  {
    /// <summary>
    /// Представление Wpf, принимающее нажатие клавиш
    /// </summary>
    protected ViewWpf.IWpfKeyPressableView KeyPressableView { get; set; }

    /// <summary>
    /// Родительский контроллер
    /// </summary>
    public WpfControllerBase ParentController { get; protected set; }


    /// <summary>
    /// Передать управление родительскому контроллеру
    /// </summary>
    public void StartParentController()
    {
      ParentController.Start();
    }

    /// <summary>
    /// Обработчик события нажатия на клавиатуру
    /// </summary>
    /// <param name="parSender">Источник события</param>
    /// <param name="parArgs">Объект KeyEventArgs, содержащий данные события</param>
    protected abstract void KeyEventHandler(object parSender, KeyEventArgs parArgs);

    /// <summary>
    /// Начать работу
    /// </summary>
    public virtual void Start()
    {
      KeyPressableView?.SubscribeOnKeyPressed(KeyEventHandler);
    }

    /// <summary>
    /// Закончить работу
    /// </summary>
    public virtual void Stop()
    {
      KeyPressableView?.UnSubscribeOnKeyPressed(KeyEventHandler);
    }
  }
}
