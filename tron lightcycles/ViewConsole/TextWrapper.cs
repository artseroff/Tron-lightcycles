using System;
using System.Collections.Generic;
using System.Text;

namespace ViewConsole
{
  /// <summary>
  /// Класс, переносящий текст
  /// </summary>
  public class TextWrapper
  {

    /// <summary>
    /// Перенести текст по пробелу с заданной шириной.
    /// Если на указанной ширине в какой-то строке
    /// находится слово, то переносит по последнему
    /// встретившемуся пробелу. Если в строке был \n,
    /// переносит по нему.
    /// </summary>
    /// <param name="parText">Текст</param>
    /// <param name="parWidth">Требуемая ширина строк</param>
    /// <returns>Список строк, перенесенных по пробелу</returns>
    public static List<string> GetLinesWithWidthWrappedBySpace(string parText, int parWidth)
    {
      parText = parText.Trim();
      List<string> textLines = new List<string>();

      int sumWidth = 0;
      while (sumWidth + parWidth + 1 < parText.Length)
      {

        string tempText = parText[sumWidth..(sumWidth + parWidth + 1)];
        int indexWrap = tempText.IndexOf('\n');
        if (indexWrap != -1)
        {
          textLines.Add(tempText[0..(indexWrap+1)]);
        }
        else
        {
          if (tempText[^1] != ' ')
          {
            textLines.Add(tempText[0..(tempText.LastIndexOf(' ') + 1)]);
          }
          else
          {
            textLines.Add(tempText);
          }
        }
        
        sumWidth += textLines[^1].Length;
      }

      textLines.Add(parText[sumWidth..parText.Length]);
      return textLines;
    }
  }
}