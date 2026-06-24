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

  public MoveResult MoveH(
      IKinematicWorld world,
      int amount,
      CollisionCallback? onBlocked = null)
  {
    var direction = amount < 0 ? CollisionDirection.Left : CollisionDirection.Right;
    return MoveByPixels(world, amount, new Int2(Math.Sign(amount), 0), direction, onBlocked);
  }

  public MoveResult MoveV(
      IKinematicWorld world,
      int amount,
      CollisionCallback? onBlocked = null)
  {
    var direction = amount < 0 ? CollisionDirection.Up : CollisionDirection.Down;
    return MoveByPixels(world, amount, new Int2(0, Math.Sign(amount)), direction, onBlocked);
  }

  private MoveResult MoveByPixels(
      IKinematicWorld world,
      int amount,
      Int2 step,
      CollisionDirection blockedDirection,
      CollisionCallback? onBlocked)
  {
    var moved = 0;

    for (var i = 0; i < Math.Abs(amount); i++)
    {
      var nextPosition = new Int2(Position.x + step.x, Position.y + step.y);
      var nextBounds = new RectI(nextPosition.x, nextPosition.y, Size.x, Size.y);

      if (world.OverlapsSolid(nextBounds))
      {
        var result = new MoveResult(
            amount,
            moved,
            Blocked: true,
            Direction: blockedDirection);

        onBlocked?.Invoke(result);
        return result;
      }

      Position = nextPosition;
      moved += step.x + step.y;
    }

    return new MoveResult(amount, moved, Blocked: false, Direction: CollisionDirection.None);
  }

  public bool IsGrounded(IKinematicWorld world)
  {
    return ReadSurfaceState(world).Grounded;
  }

  public SurfaceState ReadSurfaceState(IKinematicWorld world)
  {
    return new SurfaceState(
        Grounded: world.OverlapsSolid(OffsetBounds(0, 1)),
        TouchingCeiling: world.OverlapsSolid(OffsetBounds(0, -1)),
        TouchingLeftWall: world.OverlapsSolid(OffsetBounds(-1, 0)),
        TouchingRightWall: world.OverlapsSolid(OffsetBounds(1, 0)));
  }

  private RectI OffsetBounds(int offsetX, int offsetY)
  {
    return new RectI(Position.x + offsetX, Position.y + offsetY, Size.x, Size.y);
  }
}
