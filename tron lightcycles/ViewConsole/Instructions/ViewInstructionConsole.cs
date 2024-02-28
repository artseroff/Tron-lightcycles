using Model.Instructions;
using System;
using System.Collections.Generic;
using System.Text;
using View.Instructions;

namespace ViewConsole.Instructions
{
  /// <summary>
  /// Консольное представление инструкции
  /// </summary>
  public class ViewInstructionConsole : ViewInstructionBase
  {

    /// <summary>
    /// Отступ по бокам текста
    /// </summary>
    private const int SIDE_MARGIN = 5;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parInstruction">Модель инструкции</param>
    public ViewInstructionConsole(Instruction parInstruction) : base(parInstruction)
    {
      Draw();
    }

    /// <summary>
    /// Нарисовать представление класса
    /// </summary>
    public override void Draw()
    {
      Console.Clear();

      Width = Console.WindowWidth - 2 * SIDE_MARGIN;
      List<string> textLines = TextWrapper.GetLinesWithWidthWrappedBySpace(TextInstruction, Width);
      Y = Console.WindowHeight / 2 - textLines.Count / 2;
      X = Console.WindowWidth / 2 - Width / 2;
      for (int i = 0; i < textLines.Count; i++)
      {
        Console.SetCursorPosition(X, Y + i);
        Console.WriteLine(textLines[i]);
      }
    }
  }
}
