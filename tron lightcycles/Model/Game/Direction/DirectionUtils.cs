using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Game.Direction
{
  /// <summary>
  /// Вспомогательные утилиты для перечисления Direction
  /// </summary>
  public static class DirectionUtils
  {
    /// <summary>
    /// Получить противоположное направление
    /// </summary>
    /// <param name="parDirectionEnum">Текущее направление</param>
    /// <returns>Противоположное направление, 
    /// если parDirectionEnum == null, то
    /// возвращает null</returns>
    public static DirectionEnum? OppositeDirection(DirectionEnum? parDirectionEnum)
    {
      DirectionEnum? directionEnum;
      switch (parDirectionEnum)
      {
        case DirectionEnum.Up:
          {
            directionEnum = DirectionEnum.Down;
            break;
          }
        case DirectionEnum.Right:
          {
            directionEnum = DirectionEnum.Left;
            break;
          }
        case DirectionEnum.Down:
          {
            directionEnum = DirectionEnum.Up;
            break;
          }
        case DirectionEnum.Left:
          {
            directionEnum = DirectionEnum.Right;
            break;
          }
        default:
          {
            directionEnum = null;
            break;
          }
      }
      return directionEnum;
    }

  }
}
