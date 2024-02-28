using Controller;
using Model.Instructions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewWpf.Instructions;
using ViewWpf.Menu;

namespace ControllerWpf.instructions
{
  /// <summary>
  /// Wpf контрллер инструкций
  /// </summary>
  public class ControllerInstructionsWpf : WpfControllerBase
  {  

    /// <summary>
    /// Модель инструкций
    /// </summary>
    private Instruction _instruction;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parController">Родительский контроллер</param>
    public ControllerInstructionsWpf(WpfControllerBase parController) : base()
    {
      ParentController = parController;
      _instruction = new Instruction();
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
      KeyPressableView = new ViewInstructionWpf(_instruction);
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
