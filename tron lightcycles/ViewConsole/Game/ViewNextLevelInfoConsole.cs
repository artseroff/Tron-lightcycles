using ViewConsole;
using System;
using System.Collections.Generic;
using System.Text;
using View.Game;

namespace ViewConsole.Game
{
  /// <summary>
  /// Представление перехода
  /// на следующий уровень Console
  /// </summary>
  public class ViewNextLevelInfoConsole : View.Game.ViewNextLevelInfoBase
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parLevel">Номер уровня</param>
    public ViewNextLevelInfoConsole(int parLevel) : base(parLevel)
    {
      Draw();
    }

    /// <summary>
    /// Нарисовать представление класса
    /// </summary>
    public override void Draw()
    {
      Console.Clear();
      int maxWidth = ViewGameBase.PRESS_ANY_KEY_TEXT.Length;
      string text = $"{LEVEL_TEXT} {Level}";
      Console.SetCursorPosition(Console.WindowWidth / 2 - (int)Math.Ceiling(text.Length / 2.0), Console.WindowHeight / 2);
      Console.Write(text);
      Console.SetCursorPosition(Console.WindowWidth / 2 - (int)Math.Ceiling(maxWidth / 2.0), Console.WindowHeight / 2 + 1);
      Console.Write(ViewGameBase.PRESS_ANY_KEY_TEXT);      
    }
  }
}
