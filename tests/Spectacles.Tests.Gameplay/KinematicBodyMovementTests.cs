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
    Assert.Equal(new MoveResult(Requested: 8, Moved: 4, Blocked: true, Direction: CollisionDirection.None),
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
    Assert.Equal(new MoveResult(Requested: 8, Moved: 4, Blocked: true, Direction: CollisionDirection.None),
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
    Assert.Equal(new MoveResult(Requested: -8, Moved: 0, Blocked: true, Direction: CollisionDirection.None),
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
}