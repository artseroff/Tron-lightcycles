using ControllerWpf;
using ControllerWpf.Menu;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfStarter
{
  /// <summary>
  /// Логика взаимодействия для App.xaml
  /// </summary>
  public partial class App : Application
  {

    /// <summary>
    /// Подписан на событие System.Windows.Application.Startup,
    /// вызывающееся при App.Run()
    /// </summary>
    /// <param name="parE">Содержит аргументы для события Startup</param>
    protected override void OnStartup(StartupEventArgs parE)
    {
      
      base.OnStartup(parE);
      new ControllerMenuMainWpf().Start();

    }

  }
}
