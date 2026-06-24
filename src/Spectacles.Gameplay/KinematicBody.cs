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
    var direction = amount < 0 ? CollisionDirection.Left : CollisionDirection.Right;
    return MoveByPixels(world, amount, new Int2(Math.Sign(amount), 0), direction);
  }

  public MoveResult MoveV(IKinematicWorld world, int amount)
  {
    var direction = amount < 0 ? CollisionDirection.Up : CollisionDirection.Down;
    return MoveByPixels(world, amount, new Int2(0, Math.Sign(amount)), direction);
  }

  private MoveResult MoveByPixels(IKinematicWorld world, int amount, Int2 step, CollisionDirection blockedDirection)
  {
    var moved = 0;

    for (var i = 0; i < Math.Abs(amount); i++)
    {
      var nextPosition = new Int2(Position.x + step.x, Position.y + step.y);
      var nextBounds = new RectI(nextPosition.x, nextPosition.y, Size.x, Size.y);

      if (world.OverlapsSolid(nextBounds))
        return new MoveResult(amount, moved, Blocked: true, blockedDirection);

      Position = nextPosition;
      moved += step.x + step.y;
    }

    return new MoveResult(amount, moved, Blocked: false, CollisionDirection.None);
  }
}
