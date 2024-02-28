namespace Model.Records
{
  /// <summary>
  /// Рекорд таблицы рекордов
  /// </summary>
  public class Record
  {
    /// <summary>
    /// Минимальная длина имени игрока
    /// </summary>
    public const int MIN_NAME_LENGTH = 3;

    /// <summary>
    /// Максимальная длина имени игрока
    /// </summary>
    public const int MAX_NAME_LENGTH = 15;

    /// <summary>
    /// Имя игрока
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// Время игры
    /// </summary>
    public float Time { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parName">Имя игрока</param>
    /// <param name="parTime">Время игры</param>
    public Record(string parName, float parTime)
    {
      Name = parName;
      Time = parTime;
    }

    /// <summary>
    /// Перевести в строковое представление
    /// </summary>
    /// <returns>Строковое представление класса</returns>
    public override string ToString()
    {
      return $"{Name}{TableRecords.RECORD_DELIMETER}{string.Format("{0:f2}", Time)}";
    }

    /// <summary>
    /// Проверить допустимость имени игрока
    /// </summary>
    /// <param name="parName">Имя</param>
    /// <param name="outMessage">Выходное сообщение об ошибке</param>
    /// <returns>True, если имя допустимое, иначе false</returns>
    public static bool ValidateName(string parName, out string outMessage)
    {      
      /*if (parName.Contains(TableRecords.RECORD_DELIMETER))
      {
        outMessage = ($"{TableRecords.RECORD_DELIMETER} не допустимый символ");
      }
      else*/
      if (string.IsNullOrWhiteSpace(parName))
      {
        outMessage = ("Имя не может быть пустым или состоять только из пробельных символов");
      }
      else
      if (parName.Length < MIN_NAME_LENGTH || parName.Length > MAX_NAME_LENGTH)
      {
        outMessage = ($"Длина имени должна составлять от {MIN_NAME_LENGTH} до {MAX_NAME_LENGTH} символов включительно");
      }
      else
      {
        outMessage = "";
        return true;
      }
      return false;
    }
  }


}