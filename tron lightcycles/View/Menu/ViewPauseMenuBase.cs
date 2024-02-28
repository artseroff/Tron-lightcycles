using Model.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace View.Menu
{
  /// <summary>
  /// Базовое представление меню паузы
  /// </summary>
  public abstract class ViewPauseMenuBase : ViewMenuBase
  {
    /// <summary>
    /// Текст паузы
    /// </summary>
    protected const string PAUSE_TEXT = "При выходе в главное меню игровой процесс сохранен не будет.\nВы точно хотите выйти?";

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenu">Меню паузы</param>
    public ViewPauseMenuBase(PauseMenu parMenu) : base(parMenu)
    {
    } 
  }
}
