using Spectacles.World;

namespace Spectacles.Gameplay;

public sealed class AvatarController
{
  public AvatarController(KinematicBody body)
  {
    Body = body;
  }

  public KinematicBody Body { get; }
  public SurfaceState LastSurfaceState { get; private set; }

  public void FixedUpdate(IKinematicWorld world, AvatarInputIntent intent = default)
  {
    Body.MoveH(world, intent.MoveX);
    LastSurfaceState = Body.ReadSurfaceState(world);
  }
}