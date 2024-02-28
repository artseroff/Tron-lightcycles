using Model.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace View.Records
{
  /// <summary>
  /// Абстрактное предстваление таблицы рекордов
  /// </summary>
  public abstract class ViewTableRecordsBase : ViewBase
  {
    /// <summary>
    /// Название левого столбца
    /// </summary>
    protected const string LEFT_COL_NAME = "Игрок";

    /// <summary>
    /// Название правого столбца
    /// </summary>
    protected const string RIGHT_COL_NAME = "Время";

    /// <summary>
    /// Информация об отсутствии рекордов
    /// </summary>
    protected const string NO_RECORDS = "Рекордов пока еще нет";

    /// <summary>
    /// Список рекордов
    /// </summary>
    public List<Record> Records { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public ViewTableRecordsBase()
    {
      Records = TableRecords.ReadRecordsFromFile();
    }

    /// <summary>
    /// Нарисовать заголовок об отсутствии рекордов
    /// </summary>
    protected abstract void DrawTitleNoRecords();

  }
}
