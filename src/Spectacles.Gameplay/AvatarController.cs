using Spectacles.World;
using Spectacles.Platform;

namespace Spectacles.Gameplay;

public sealed class AvatarController
{
  private const float HorizontalSpeed = 1.5f;

  public AvatarController(KinematicBody body)
  {
    Body = body;
  }

  public KinematicBody Body { get; }
  public Float2 Velocity { get; private set; }
  public Float2 Remainder { get; private set; }
  public SurfaceState LastSurfaceState { get; private set; }

  public void FixedUpdate(IKinematicWorld world, AvatarInputIntent intent = default)
  {
    Velocity = new Float2(intent.MoveX * HorizontalSpeed, Velocity.Y);

    var horizontal = Remainder.X + Velocity.X;
    var moveX = (int)horizontal;
    Remainder = new Float2(horizontal - moveX, Remainder.Y);

    Body.MoveH(world, moveX);
    LastSurfaceState = Body.ReadSurfaceState(world);
  }
}