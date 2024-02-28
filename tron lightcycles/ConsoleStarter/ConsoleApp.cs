using ControllerConsole;
using ControllerConsole.Menu;
using System;
using System.Collections.Generic;

namespace ConsoleStarter
{
  /// <summary>
  /// Класс запускающий консольное приложение
  /// </summary>
  public class ConsoleApp
  {
    /// <summary>
    /// Точка входа в консольное приложение
    /// </summary>
    public static void Main()
    {            
      new ControllerMenuMainConsole().Start();
    }
  }

}
