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

    var moveX = ExtractWholePixels(Remainder.X + Velocity.X, out var nextRemainderX);
    Remainder = new Float2(nextRemainderX, Remainder.Y);

    Body.MoveH(world, moveX);
    LastSurfaceState = Body.ReadSurfaceState(world);
  }

  private static int ExtractWholePixels(float amount, out float remiander)
  {
    var wholePixels = (int)amount;
    remiander = amount - wholePixels;
    return wholePixels;
  }
}