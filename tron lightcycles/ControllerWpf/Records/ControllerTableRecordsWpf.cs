using Controller;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewWpf.Records;

namespace ControllerWpf.Records
{
  /// <summary>
  /// Wpf контроллер таблицы рекордов
  /// </summary>
  public class ControllerTableRecordsWpf : WpfControllerBase
  {

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parParentController">Родительский контроллер</param>
    public ControllerTableRecordsWpf(WpfControllerBase parParentController)
    {
      ParentController = parParentController;
    }

    /// <summary>
    /// Обработчик события нажатия на клавиатуру
    /// </summary>
    /// <param name="parSender">Источник события</param>
    /// <param name="parArgs">Объект KeyEventArgs, содержащий данные события</param>
    protected override void KeyEventHandler(object parSender, KeyEventArgs parArgs)
    {
      switch (parArgs.Key)
      {
        case Key.Escape:
          Stop();
          break;
      }
    }

    /// <summary>
    /// Начать работу
    /// </summary>
    public override void Start()
    {
      KeyPressableView = new ViewTableRecordsWpf();
      base.Start();
    }

    /// <summary>
    /// Закончить работу
    /// </summary>
    public override void Stop()
    {
      base.Stop();
      StartParentController();
    }
  }
}
