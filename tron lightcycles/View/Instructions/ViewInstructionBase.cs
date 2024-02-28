using Model.Instructions;
using System;
using System.Collections.Generic;
using System.Text;

namespace View.Instructions
{
  /// <summary>
  /// Представление инструкции
  /// </summary>
  public abstract class ViewInstructionBase : ViewBase
  {
    /// <summary>
    /// Текст инструкции
    /// </summary>
    protected string TextInstruction { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parInstruction">Модель инструкции</param>
    public ViewInstructionBase(Instruction parInstruction)
    {
      TextInstruction = parInstruction.TextInstruction;
    }
  }
}
