using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Menu
{
  /// <summary>
  /// Перечисление типов пунктов главного меню
  /// </summary>
  public enum MenuMainIds : int
  {
    /// <summary>
    /// Новая игра
    /// </summary>
    New,

    /// <summary>
    /// Инструкция
    /// </summary>
    Info,

    /// <summary>
    /// Рекорды
    /// </summary>
    Records,

    /// <summary>
    /// Выход
    /// </summary>
    Exit
  }
}
