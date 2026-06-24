using Spectacles.World;
using Spectacles.Platform;

namespace Spectacles.Gameplay;

public sealed class AvatarController
{
  private const float HorizontalSpeed = 1.5f;
  private const float Gravity = 0.5f;

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
    Velocity = new Float2(intent.MoveX * HorizontalSpeed, Velocity.Y + Gravity);

    var moveX = ExtractWholePixels(Remainder.X + Velocity.X, out var nextRemainderX);
    var moveY = ExtractWholePixels(Remainder.Y + Velocity.Y, out var nextRemainderY);
    Remainder = new Float2(nextRemainderX, nextRemainderY);

    Body.MoveH(world, moveX);
    var verticalResult = Body.MoveV(world, moveY);

    if (verticalResult.Blocked && verticalResult.Direction == CollisionDirection.Down)
    {
      StopFalling();
    }

    LastSurfaceState = Body.ReadSurfaceState(world);
  }

  private void StopFalling()
  {
    Velocity = new Float2(Velocity.X, 0f);
    Remainder = new Float2(Remainder.X, 0f);
  }

  private static int ExtractWholePixels(float amount, out float remiander)
  {
    var wholePixels = (int)amount;
    remiander = amount - wholePixels;
    return wholePixels;
  }
}