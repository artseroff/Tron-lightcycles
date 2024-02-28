using Model.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using View.Menu;

namespace Controller
{
  /// <summary>
  /// Интерфейс контроллера главного меню
  /// </summary>
  public interface IControllerMainMenu
  {
    /// <summary>
    /// Модель главного меню
    /// </summary>
    MenuMain MenuMain { get; set; }

    /// <summary>
    /// Подписаться на события пунктов главного меню
    /// </summary>
    public void SubcribeOnMainMenuItemsEvents()
    {

      MenuMain[(int)MenuMainIds.New].Enter += StartGame;
      MenuMain[(int)MenuMainIds.Records].Enter += CallRecords;
      MenuMain[(int)MenuMainIds.Info].Enter += CallInstruction;
      MenuMain[(int)MenuMainIds.Exit].Enter += Exit;
      
    }

    /// <summary>
    /// Начать игру 
    /// </summary>
    protected void StartGame();

    /// <summary>
    /// Вызвать инструкции
    /// </summary>
    void CallInstruction();

    /// <summary>
    /// Вызвать таблицу рекордов
    /// </summary>
    protected void CallRecords();    

    /// <summary>
    /// Выйти из главного меню
    /// </summary>
    protected void Exit();
    
  }
}
