using Model.Records;
using System;
using System.Collections.Generic;
using System.Text;
using ViewConsole.Records;

namespace ControllerConsole.Records
{
  /// <summary>
  /// Консольный контроллер добавления нового рекорда
  /// </summary>
  public class NewRecordControllerConsole : ConsoleControllerBase
  {
    /// <summary>
    /// Модель ввода нового рекорда
    /// </summary>
    private NewRecordInputModel _inputModel;

    /// <summary>
    /// Представление окна ввода нового рекорда
    /// </summary>
    private ViewNewRecordInputConsole _viewNewRecordInputConsole;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parInputModel">Модель ввода нового рекорда</param>
    public NewRecordControllerConsole(NewRecordInputModel parInputModel)
    {
      _inputModel = parInputModel;
      _viewNewRecordInputConsole = new ViewNewRecordInputConsole(parInputModel);
    }

    /// <summary>
    /// Начать работу
    /// </summary>
    public override void Start()
    {      
      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        if (keyInfo.Key is ConsoleKey.Backspace)
        {
          _inputModel.RemoveLastNameSymbol();
        }
        else
        if (keyInfo.Key is ConsoleKey.Enter)
        {
          if (!_inputModel.AddRecord(out string message))
          {
            _viewNewRecordInputConsole.DrawErrors(message);
          }
          else
          {
            Stop();
          }
        }
        else
        if (keyInfo.Key >= ConsoleKey.A && keyInfo.Key <= ConsoleKey.Z)
        {
          // Если добавлять к имени keyInfo.KeyChar - символ зависит от раскладки
          _inputModel.AddSymbolToName((char)(keyInfo.Key - ConsoleKey.A + 'A'));
        }
        else
        if (keyInfo.Key >= ConsoleKey.D0 && keyInfo.Key <= ConsoleKey.D9)
        {
          _inputModel.AddSymbolToName((char)(keyInfo.Key - ConsoleKey.D0 + '0'));
        }

      }
      while (!NeedExit);
    }

  }
}
