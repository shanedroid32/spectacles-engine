using Spectacles.World;
using Spectacles.Platform;

namespace Spectacles.Gameplay;

public sealed class AvatarController
{
  private const float HorizontalSpeed = 2f;

  public AvatarController(KinematicBody body)
  {
    Body = body;
  }

  public KinematicBody Body { get; }
  public Float2 Velocity { get; private set; }
  public SurfaceState LastSurfaceState { get; private set; }

  public void FixedUpdate(IKinematicWorld world, AvatarInputIntent intent = default)
  {
    Velocity = new Float2(intent.MoveX * HorizontalSpeed, Velocity.Y);
    Body.MoveH(world, (int)Velocity.X);
    LastSurfaceState = Body.ReadSurfaceState(world);
  }
}