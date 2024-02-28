using Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerConsole
{
  /// <summary>
  /// Базовый консольный контроллер
  /// </summary>
  public abstract class ConsoleControllerBase : IController
  {
        
    /// <summary>
    /// Нужно закончить работу
    /// </summary>
    public bool NeedExit { get; protected set; }    

    /// <summary>
    /// Начать работу
    /// </summary>
    public virtual void Start()
    {
      NeedExit = false;
    }

    /// <summary>
    /// Закончить работу
    /// </summary>
    public void Stop()
    {
      NeedExit = true;
    }
  }
}
