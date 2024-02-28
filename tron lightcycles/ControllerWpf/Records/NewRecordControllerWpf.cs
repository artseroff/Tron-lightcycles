using Model.Records;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewWpf.Game;
using ViewWpf.Records;

namespace ControllerWpf.Records
{
  /// <summary>
  /// Контроллер добавления нового рекорда Wpf
  /// </summary>
  public class NewRecordControllerWpf : WpfControllerBase
  {

    /// <summary>
    /// Модель ввода нового рекорда
    /// </summary>
    private NewRecordInputModel _inputModel;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parParentController">Родительский контроллер</param>
    /// <param name="parInputModel">Модель ввода нового рекорда</param>
    public NewRecordControllerWpf(WpfControllerBase parParentController, NewRecordInputModel parInputModel)
    {
      ParentController = parParentController;
      _inputModel = parInputModel;
      KeyPressableView = new ViewNewRecordInputWpf(_inputModel);
    }

    /// <summary>
    /// Обработчик события нажатия на клавиатуру
    /// </summary>
    /// <param name="parSender">Источник события</param>
    /// <param name="parArgs">Объект KeyEventArgs, содержащий данные события</param>
    protected override void KeyEventHandler(object parSender, KeyEventArgs parArgs)
    {

      if (parArgs.Key is Key.Back)
      {
        _inputModel.RemoveLastNameSymbol();
      }
      else
      if (parArgs.Key is Key.Enter)
      {
        if (!_inputModel.AddRecord(out string message))
        {
          ((ViewNewRecordInputWpf)KeyPressableView).DrawErrors(message);
        }
        else
        {
          Stop();
          ParentController.ParentController.Start();
        }
      }
      else
      if (parArgs.Key >= Key.A && parArgs.Key <= Key.Z)
      {
        _inputModel.AddSymbolToName((char)((int)parArgs.Key - (int)Key.A + 'A'));
      }
      else
      if (parArgs.Key >= Key.D0 && parArgs.Key <= Key.D9)
      {
        _inputModel.AddSymbolToName((char)((int)parArgs.Key - (int)Key.D0 + '0'));
      }
    }
  }
}
