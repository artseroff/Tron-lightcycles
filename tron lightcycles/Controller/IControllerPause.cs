using Model.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
  /// <summary>
  /// Базовый контроллер меню паузы
  /// </summary>
  public interface IControllerPause
  {
    /// <summary>
    /// Модель главного меню
    /// </summary>
    PauseMenu PauseMenu { get; set; }

    /// <summary>
    /// Подписаться на события пунктов меню паузы
    /// </summary>
    public void SubcribeOnPauseMenuItemsEvents()
    {
      PauseMenu = new PauseMenu();
      PauseMenu[(int)PauseMenuIds.No].Enter += Resume;
      PauseMenu[(int)PauseMenuIds.Yes].Enter += Exit;
    }

    /// <summary>
    /// Продолжить игру
    /// </summary>
    protected void Resume();

    /// <summary>
    /// Выйти в главное меню
    /// </summary>
    protected void Exit();
  }
}
