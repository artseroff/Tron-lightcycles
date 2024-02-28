using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace Model.Instructions
{
  /// <summary>
  /// Класс с информацией об инструкции
  /// </summary>
  public class Instruction
  {
    /// <summary>
    /// Текст инструкции
    /// </summary>
    public string TextInstruction { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public Instruction()
    {    
      TextInstruction = Properties.Resources.TextInstruction;
    }
  }
}
