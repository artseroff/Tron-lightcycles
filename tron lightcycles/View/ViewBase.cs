using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
  /// <summary>
  /// Базовое представление
  /// </summary>
  public abstract class ViewBase
  {
    /// <summary>
    /// Координата левого верхнего угла по X
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Координата левого верхнего угла по Y
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Ширина
    /// </summary>
    public int Width { get; protected set; }

    /// <summary>
    /// Высота
    /// </summary>
    public int Height { get; protected set; }

    /// <summary>
    /// Нарисовать представление класса
    /// </summary>
    public abstract void Draw();
  }
}

