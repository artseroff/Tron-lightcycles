using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewConsole.Records;

namespace ControllerConsole.Records
{
  /// <summary>
  /// Контроллер таблицы рекордов
  /// </summary>
  public class ControllerTableRecordsConsole : ConsoleControllerBase
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    public ControllerTableRecordsConsole()
    {
      new ViewTableRecordsConsole();
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
