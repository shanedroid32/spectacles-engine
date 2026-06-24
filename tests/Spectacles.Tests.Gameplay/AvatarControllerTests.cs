using Spectacles.Gameplay;
using Spectacles.Platform;
using Spectacles.World;

namespace Spectacles.Tests.Gameplay;

public sealed class AvatarControllerTests
{
  [Fact]
  public void FixedUpdate_StoresCurrentSurfaceState()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    world.SetSolid(tileX: 0, tileY: 1, solid: true);
    var body = new KinematicBody(new Int2(4, 8), new Int2(8, 8));
    var controller = new AvatarController(body);

    controller.FixedUpdate(world);

    Assert.True(controller.LastSurfaceState.Grounded);
  }

  [Fact]
  public void FixedUpdate_RefreshesSurfaceStateEachTick()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    world.SetSolid(tileX: 0, tileY: 1, solid: true);
    var body = new KinematicBody(new Int2(4, 8), new Int2(8, 8));
    var controller = new AvatarController(body);

    controller.FixedUpdate(world);
    body.Position = new Int2(4, 4);
    controller.FixedUpdate(world);

    Assert.False(controller.LastSurfaceState.Grounded);
  }

  [Fact]
  public void FixedUpdate_WithRightIntentMovesBodyRightBySpeed()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    var body = new KinematicBody(new Int2(4, 4), new Int2(8, 8));
    var controller = new AvatarController(body);

    controller.FixedUpdate(world, new AvatarInputIntent(MoveX: 1));

    Assert.Equal(new Int2(5, 4), body.Position);
  }

  [Fact]
  public void FixedUpdate_WithLeftIntentSetsHorizontalVelocity()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    var body = new KinematicBody(new Int2(8, 4), new Int2(8, 8));
    var controller = new AvatarController(body);

    controller.FixedUpdate(world, new AvatarInputIntent(MoveX: -1));

    Assert.Equal(new Float2(-1.5f, 0f), controller.Velocity);
  }

  [Fact]
  public void FixedUpdate_WithNoHorizontalIntentClearsHorizontalVelocity()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    var body = new KinematicBody(new Int2(8, 4), new Int2(8, 8));
    var controller = new AvatarController(body);

    controller.FixedUpdate(world, new AvatarInputIntent(MoveX: 1));
    controller.FixedUpdate(world, new AvatarInputIntent(MoveX: 0));

    Assert.Equal(new Float2(0f, 0f), controller.Velocity);
  }

  [Fact]
  public void FixedUpdate_AccumulatesHorizontalSubpixels()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    var body = new KinematicBody(new Int2(4, 4), new Int2(8, 8));
    var controller = new AvatarController(body);

    controller.FixedUpdate(world, new AvatarInputIntent(MoveX: 1));

    Assert.Equal(new Int2(5, 4), body.Position);
    Assert.Equal(new Float2(0.5f, 0f), controller.Remainder);

    controller.FixedUpdate(world, new AvatarInputIntent(MoveX: 1));

    Assert.Equal(new Int2(7, 4), body.Position);
    Assert.Equal(new Float2(0f, 0f), controller.Remainder);
  }

  [Fact]
  public void FixedUpdate_KeepsBodyPositionIntegerAligned()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    var body = new KinematicBody(new Int2(4, 4), new Int2(8, 8));
    var controller = new AvatarController(body);

    controller.FixedUpdate(world, new AvatarInputIntent(MoveX: 1));

    Assert.Equal(5, body.Position.x);
    Assert.Equal(4, body.Position.y);
    Assert.Equal(new Float2(0.5f, 0f), controller.Remainder);
  }

  [Fact]
  public void FixedUpdate_WithNoIntentDoesNotCreateHorizontalRemainder()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    var body = new KinematicBody(new Int2(4, 4), new Int2(8, 8));
    var controller = new AvatarController(body);

    controller.FixedUpdate(world, new AvatarInputIntent(MoveX: 0));

    Assert.Equal(new Int2(4, 4), body.Position);
    Assert.Equal(new Float2(0f, 0f), controller.Velocity);
    Assert.Equal(new Float2(0f, 0f), controller.Remainder);
  }

  [Fact]
  public void FixedUpdate_AccumulatesLeftwardHorizontalSubpixels()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    var body = new KinematicBody(new Int2(8, 4), new Int2(8, 8));
    var controller = new AvatarController(body);

    controller.FixedUpdate(world, new AvatarInputIntent(MoveX: -1));

    Assert.Equal(new Int2(7, 4), body.Position);
    Assert.Equal(new Float2(-0.5f, 0f), controller.Remainder);

    controller.FixedUpdate(world, new AvatarInputIntent(MoveX: -1));

    Assert.Equal(new Int2(5, 4), body.Position);
    Assert.Equal(new Float2(0f, 0f), controller.Remainder);
  }
}
