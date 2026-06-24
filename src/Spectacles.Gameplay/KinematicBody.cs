using System;
using Spectacles.Platform;
using Spectacles.World;

namespace Spectacles.Gameplay;

public sealed class KinematicBody
{
  public Int2 Position { get; set; }
  public Int2 Size { get; }

  public KinematicBody(Int2 position, Int2 size)
  {
    Position = position;
    Size = size;
  }

  public RectI Bounds => new(Position.x, Position.y, Size.x, Size.y);

  public MoveResult MoveH(IKinematicWorld world, int amount)
  {
    var step = Math.Sign(amount);
    var moved = 0;

    for (int i = 0; i < Math.Abs(amount); i++)
    {
      var nextPosition = new Int2(Position.x + step, Position.y);
      var nextBounds = new RectI(nextPosition.x, nextPosition.y, Size.x, Size.y);

      if (world.OverlapsSolid(nextBounds))
      {
        return new MoveResult(amount, moved, Blocked: true);
      }

      Position = nextPosition;
      moved += step;
    }

    return new MoveResult(amount, moved, Blocked: false);
  }

  public MoveResult MoveV(IKinematicWorld world, int amount)
  {
    var step = Math.Sign(amount);
    var moved = 0;

    for (int i = 0; i < Math.Abs(amount); i++)
    {
      var nextPosition = new Int2(Position.x, Position.y + step);
      var nextBounds = new RectI(nextPosition.x, nextPosition.y, Size.x, Size.y);

      if (world.OverlapsSolid(nextBounds))
        return new MoveResult(amount, moved, Blocked: true);

      Position = nextPosition;
      moved += step;
    }

    return new MoveResult(amount, moved, Blocked: false);
  }
}
