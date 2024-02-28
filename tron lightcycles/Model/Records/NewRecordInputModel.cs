using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Records
{
  /// <summary>
  /// Модель ввода нового рекорда
  /// </summary>
  public class NewRecordInputModel
  {
    /// <summary>
    /// Список рекордов
    /// </summary>
    private List<Record> _records = new List<Record>();

    /// <summary>
    /// Максимальная длина имени
    /// </summary>
    private const int MAX_NAME_LENGTH = 15;

    /// <summary>
    /// Событие изменения введенного имени игрока
    /// </summary>
    public Action OnNameChanged;

    /// <summary>
    /// Вводимое имя игрока
    /// </summary>
    public string InputedName { get; private set; } = "";

    /// <summary>
    /// Время игры
    /// </summary>
    public float Time { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parTime">Время игры</param>
    public NewRecordInputModel(float parTime)
    {
      Time = parTime;
    }

    /// <summary>
    /// Проверить меньше ли время игры чем
    /// наихудший рекорд из 10 лучших
    /// </summary>
    /// <returns>True, если время игры меньше, чем наихудший рекорд, иначе false</returns>
    public bool IsTimeLessThanWorst()
    {
      _records = TableRecords.ReadRecordsFromFile();
      if (_records.Count == 0)
      {
        return true;
      }
      else
      {
        // Список рекордов отсортирован
        // Если время игры меньше последнего рекорда
        // или количество рекордов меньше максимального,
        // то время попадает в список 10 лучших
        return Time < _records[^1].Time || _records.Count < TableRecords.MAX_RECORDS_COUNT;
      }
    }

    /// <summary>
    /// Сортировка рекордов
    /// </summary>
    private void SortRecords()
    {
      _records.Sort((parFirstRec, parSecondRec) => parFirstRec.Time.CompareTo(parSecondRec.Time));
    }

    
    /// <summary>
    /// Добавить рекорд в таблицу рекордов 
    /// </summary>
    /// <param name="outMessage">Сообщение об ошибке</param>
    /// <returns>True, если имя допустимое и рекорд добавлен, иначе false</returns>
    public bool AddRecord(out string outMessage)
    {
      if (!Record.ValidateName(InputedName, out outMessage))
      {
        return false;
      }
      _records.Add(new Record(InputedName, Time));
      SortRecords();
      if (_records.Count > TableRecords.MAX_RECORDS_COUNT)
      {
        _records.RemoveAt(_records.Count-1);
      }
      TableRecords.SaveRecords(_records);
      return true;

    }

    /// <summary>
    /// Добавить символ к имени игрока
    /// </summary>
    /// <param name="parSymbol">Добавляемый символ</param>
    public void AddSymbolToName(char parSymbol)
    {
      if (InputedName.Length < MAX_NAME_LENGTH)
      {
        InputedName += parSymbol;
        OnNameChanged?.Invoke();
      }
    }

    /// <summary>
    /// Удалить последний символ из введенного имени
    /// </summary>
    public void RemoveLastNameSymbol()
    {
      if (InputedName.Length > 0)
      {
        InputedName = InputedName.Substring(0, InputedName.Length - 1);
        OnNameChanged?.Invoke();
      }
    }


  }
}
