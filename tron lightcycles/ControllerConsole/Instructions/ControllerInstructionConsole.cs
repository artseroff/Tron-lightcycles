using Model.Instructions;
using System;
using System.Collections.Generic;
using System.Text;
using ViewConsole.Instructions;

namespace ControllerConsole.Instructions
{
  /// <summary>
  /// Консольный контроллер инструкций
  /// </summary>
  public class ControllerInstructionConsole : ConsoleControllerBase
  {
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public ControllerInstructionConsole() : base()
    {
      new ViewInstructionConsole(new Instruction());
    }

    /// <summary>
    /// Начать работу
    /// </summary>
    public override void Start()
    {
      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);


        if (keyInfo.Key is ConsoleKey.Escape)
        {
          Stop();
        }

      } while (!NeedExit);
    }

  }
}
