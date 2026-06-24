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
}