using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{ 
 
  /// <summary>
  /// Интерфейс контроллера
  /// </summary>
  public interface IController
  {    

    /// <summary>
    /// Начать работу
    /// </summary>
    void Start();

    /// <summary>
    /// Закончить работу
    /// </summary>
    void Stop();
  }
}
