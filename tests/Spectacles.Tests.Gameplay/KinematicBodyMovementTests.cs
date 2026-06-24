using Spectacles.Gameplay;
using Spectacles.Platform;
using Spectacles.World;

namespace Spectacles.Tests.Gameplay;

public sealed class KinematicBodyMovementTests
{
  [Fact]
  public void MoveH_MovesRequestedPixelsWhenUnblocked()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    var body = new KinematicBody(
        position: new Int2(4, 4),
        size: new Int2(8, 8));

    var result = body.MoveH(world, amount: 3);

    Assert.Equal(new Int2(7, 4), body.Position);
    Assert.Equal(new MoveResult(Requested: 3, Moved: 3, Blocked: false, Direction: CollisionDirection.None),
    result);
  }

  [Fact]
  public void MoveH_StopsBeforeSolidTile()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    world.SetSolid(tileX: 1, tileY: 0, solid: true);

    var body = new KinematicBody(
        position: new Int2(4, 4),
        size: new Int2(8, 8));

    var result = body.MoveH(world, amount: 8);

    Assert.Equal(new Int2(8, 4), body.Position);
    Assert.Equal(new MoveResult(Requested: 8, Moved: 4, Blocked: true, Direction: CollisionDirection.Right),
    result);
  }

  [Fact]
  public void MoveV_MovesRequestedPixelsWhenUnblocked()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    var body = new KinematicBody(
        position: new Int2(4, 4),
        size: new Int2(8, 8));

    var result = body.MoveV(world, amount: 3);

    Assert.Equal(new Int2(4, 7), body.Position);
    Assert.Equal(new MoveResult(Requested: 3, Moved: 3, Blocked: false, Direction: CollisionDirection.None),
    result);
  }

  [Fact]
  public void MoveV_StopsBeforeFloor()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    world.SetSolid(tileX: 0, tileY: 1, solid: true);

    var body = new KinematicBody(
        position: new Int2(4, 4),
        size: new Int2(8, 8));

    var result = body.MoveV(world, amount: 8);

    Assert.Equal(new Int2(4, 8), body.Position);
    Assert.Equal(new MoveResult(Requested: 8, Moved: 4, Blocked: true, Direction: CollisionDirection.Down),
    result);
  }

  [Fact]
  public void MoveV_StopsBeforeCeiling()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    world.SetSolid(tileX: 0, tileY: 0, solid: true);

    var body = new KinematicBody(
        position: new Int2(4, 16),
        size: new Int2(8, 8));

    var result = body.MoveV(world, amount: -8);

    Assert.Equal(new Int2(4, 16), body.Position);
    Assert.Equal(new MoveResult(Requested: -8, Moved: 0, Blocked: true, Direction: CollisionDirection.Up),
    result);
  }

  [Fact]
  public void MoveH_WithZeroAmountDoesNotMove()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    var body = new KinematicBody(
        position: new Int2(4, 4),
        size: new Int2(8, 8));

    var result = body.MoveH(world, amount: 0);

    Assert.Equal(new Int2(4, 4), body.Position);
    Assert.Equal(new MoveResult(Requested: 0, Moved: 0, Blocked: false, Direction: CollisionDirection.None),
    result);
  }

  [Fact]
  public void MoveH_WhenBlockedReportsRight()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    world.SetSolid(tileX: 1, tileY: 0, solid: true);
    var body = new KinematicBody(new Int2(4, 4), new Int2(8, 8));

    var result = body.MoveH(world, amount: 8);

    Assert.Equal(CollisionDirection.Right, result.Direction);
  }

  [Fact]
  public void MoveV_WhenBlockedReportsDown()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    world.SetSolid(tileX: 0, tileY: 1, solid: true);
    var body = new KinematicBody(new Int2(4, 4), new Int2(8, 8));

    var result = body.MoveV(world, amount: 8);

    Assert.Equal(CollisionDirection.Down, result.Direction);
  }

  [Fact]
  public void MoveH_WhenBlockedRunsCallbackWithResult()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    world.SetSolid(tileX: 1, tileY: 0, solid: true);
    var body = new KinematicBody(new Int2(4, 4), new Int2(8, 8));
    MoveResult? callbackResult = null;

    var result = body.MoveH(world, amount: 8, onBlocked: hit => callbackResult = hit);

    Assert.Equal(result, callbackResult);
    Assert.Equal(CollisionDirection.Right, callbackResult?.Direction);
  }

  [Fact]
  public void MoveH_WhenUnblockedDoesNotRunCallback()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    var body = new KinematicBody(new Int2(4, 4), new Int2(8, 8));
    var callbackCount = 0;

    body.MoveH(world, amount: 3, onBlocked: _ => callbackCount++);

    Assert.Equal(0, callbackCount);
  }

  [Fact]
  public void IsGrounded_ReturnsTrueWhenSolidIsOnePixelBelow()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    world.SetSolid(tileX: 0, tileY: 1, solid: true);
    var body = new KinematicBody(new Int2(4, 8), new Int2(8, 8));

    var grounded = body.IsGrounded(world);

    Assert.True(grounded);
  }

  [Fact]
  public void IsGrounded_ReturnsFalseWhenSpaceBelowIsEmpty()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    var body = new KinematicBody(new Int2(4, 8), new Int2(8, 8));

    var grounded = body.IsGrounded(world);

    Assert.False(grounded);
  }

  [Fact]
  public void IsGrounded_DoesNotChangePosition()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    world.SetSolid(tileX: 0, tileY: 1, solid: true);
    var body = new KinematicBody(new Int2(4, 8), new Int2(8, 8));

    body.IsGrounded(world);

    Assert.Equal(new Int2(4, 8), body.Position);
  }

  [Fact]
  public void ReadSurfaceState_ReturnsContactFacts()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    world.SetSolid(tileX: 0, tileY: 1, solid: true);
    world.SetSolid(tileX: 1, tileY: 0, solid: true);
    var body = new KinematicBody(new Int2(8, 8), new Int2(8, 8));

    var state = body.ReadSurfaceState(world);

    Assert.Equal(
        new SurfaceState(
            Grounded: true,
            TouchingCeiling: false,
            TouchingLeftWall: false,
            TouchingRightWall: true),
        state);
  }

  [Fact]
  public void ReadSurfaceState_ReturnsFalseWhenNothingIsTouched()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    var body = new KinematicBody(new Int2(8, 8), new Int2(8, 8));

    var state = body.ReadSurfaceState(world);

    Assert.Equal(
        new SurfaceState(
            Grounded: false,
            TouchingCeiling: false,
            TouchingLeftWall: false,
            TouchingRightWall: false),
        state);
  }
}
