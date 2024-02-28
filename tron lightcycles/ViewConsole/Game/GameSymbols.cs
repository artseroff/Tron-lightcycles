using System;
using System.Collections.Generic;
using System.Text;

namespace ViewConsole.Game
{
  /// <summary>
  /// Символы, использующиеся в игре
  /// </summary>
  public static class GameSymbols
  {
    /// <summary>
    /// Символ полного ускорения
    /// </summary>
    public const char FULL_SPEED_UP_SYMBOL = '█';

    /// <summary>
    /// Символ использованного ускорения
    /// </summary>
    public const char EMPTY_SPEED_UP_SYMBOL = '░';

    /// <summary>
    /// Символ верхней половины строки
    /// </summary>
    public const char UPPER_HALF_BLOCK_SYMBOL = '▀';
    
    /// <summary>
    /// Символ нижней половины строки
    /// </summary>
    public const char LOWER_HALF_BLOCK_SYMBOL = '▄';        

    /// <summary>
    /// Символ полной ячейки
    /// </summary>
    public const char FULL_BLOCK_SYMBOL = '█';

    /// <summary>
    /// Отмасштабированная верхняя половина строки
    /// </summary>
    public static string ScaledUpperHalfBlock { get; } = new string(UPPER_HALF_BLOCK_SYMBOL, ViewGameConsole.SCALE_X);

    /// <summary>
    /// Отмасштабированная нижняя половина строки
    /// </summary>
    public static string ScaledLowerHalfBlock { get; } = new string(LOWER_HALF_BLOCK_SYMBOL, ViewGameConsole.SCALE_X);

    /// <summary>
    /// Отмасштабированная полная клетка головы/следа
    /// </summary>
    public static string ScaledFullBlock { get; } = new string(FULL_BLOCK_SYMBOL, ViewGameConsole.SCALE_X);

  }
}
