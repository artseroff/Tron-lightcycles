using Model.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace View.Records
{
  /// <summary>
  /// Базовое представление окна ввода нового рекорда
  /// </summary>
  public abstract class ViewNewRecordInputBase : ViewBase
  {
    /// <summary>
    /// Текст введите ваше имя
    /// </summary>
    protected const string INPUT_NAME_TEXT = "Введите ваше имя";

    /// <summary>
    /// Текст время игры
    /// </summary>
    protected const string GAME_TIME_TEXT = "Время игры:";

    /// <summary>
    /// Модель ввода нового рекорда
    /// </summary>
    protected NewRecordInputModel InputModel { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parInputModel">Модель ввода нового рекорда</param>
    public ViewNewRecordInputBase(NewRecordInputModel parInputModel)
    {
      InputModel = parInputModel;
      InputModel.OnNameChanged += Draw;
    }

    /// <summary>
    /// Отобразить ошибки
    /// </summary>
    /// <param name="parErrors">Ошибки</param>
    public abstract void DrawErrors(string parErrors);

    /// <summary>
    /// Очистить ошибки
    /// </summary>
    protected abstract void ClearErrors();
  }
}
