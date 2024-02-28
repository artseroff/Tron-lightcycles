using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Model.Records
{
  /// <summary>
  /// Таблица рекордов
  /// </summary>
  public static class TableRecords
  {
    
    /// <summary>
    /// Разделитель полей рекорда
    /// </summary>
    public const char RECORD_DELIMETER = ';';

    /// <summary>
    /// Разделитель строк объектов рекорда в файле
    /// </summary>
    public const char RECORDS_LINE_DELIMETER = '\n';

    /// <summary>
    /// Название файла с рекордами
    /// </summary>
    private const string NAME_RECORDS_FILE = "Records.txt";

    /// <summary>
    /// Название игровой папки
    /// </summary>
    private const string DIRECTORY_NAME = "Tron";
    

    /// <summary>
    /// Абсолютный путь до AppData/Local/Tron
    /// </summary>
    private static readonly string ABS_DIRECTORY_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DIRECTORY_NAME);

    /// <summary>
    /// Абсолютный путь до AppData/Local/Tron/Records.txt
    /// </summary>
    private static readonly string ABS_FILE_PATH = Path.Combine(ABS_DIRECTORY_PATH, NAME_RECORDS_FILE);

    /// <summary>
    /// Максимальное число рекордов
    /// </summary>
    public const int MAX_RECORDS_COUNT = 10;    

    /// <summary>
    /// Прочитать файл рекордов
    /// </summary>
    /// <returns>Список рекордов</returns>
    public static List<Record> ReadRecordsFromFile()
    {
      string text = String.Empty;
      // Если уже есть, не создает папку
      Directory.CreateDirectory(ABS_DIRECTORY_PATH);
      if (File.Exists(ABS_FILE_PATH))
      {
        text = File.ReadAllText(ABS_FILE_PATH).Trim();
      }
      else
      {
        File.WriteAllText(ABS_FILE_PATH, "");
      }

      List<Record> records = new List<Record>();
      if (text == "")
      {
        return records;
      }

      foreach (string elRecordLine in text.Split(RECORDS_LINE_DELIMETER))
      {
        string[] recordFieldsArray = elRecordLine.Split(RECORD_DELIMETER);
        records.Add(new Record(recordFieldsArray[0], float.Parse(recordFieldsArray[1])));
      }


      return records;
    }

    /// <summary>
    /// Сохранить список рекордов в файл
    /// </summary>
    /// <param name="parRecords">Список рекордов</param>
    public static void SaveRecords(List<Record> parRecords)
    {
      Directory.CreateDirectory(ABS_DIRECTORY_PATH);
      File.WriteAllText(Path.Combine(ABS_DIRECTORY_PATH, NAME_RECORDS_FILE), ParseListRecordsToString(parRecords));
    }

    /// <summary>
    /// Преобразовать список рекордов в строку
    /// </summary>
    /// <param name="parRecords">Список рекордов</param>
    /// <returns>Строковое представление списка рекордов</returns>
    private static string ParseListRecordsToString(List<Record> parRecords)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (Record elRecord in parRecords)
      {
        stringBuilder.Append(elRecord.ToString()).Append(RECORDS_LINE_DELIMETER);
      }
      return stringBuilder.ToString();
    }

  }
}
