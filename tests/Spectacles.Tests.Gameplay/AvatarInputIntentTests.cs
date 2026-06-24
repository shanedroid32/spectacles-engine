using Spectacles.Gameplay;
using Spectacles.Platform;
using Spectacles.World;

namespace Spectacles.Tests.Gameplay;

public sealed class AvatarInputIntentTests
{
  [Fact]
  public void FromInput_ReturnsRightWhenMoveRightIsDown()
  {
    var input = new InputSnapshot();
    input.BeginFrame([GameAction.MoveRight]);

    var intent = AvatarInputIntent.FromInput(input);

    Assert.Equal(new AvatarInputIntent(MoveX: 1), intent);
  }

  [Fact]
  public void FromInput_CancelsOppositeHorizontalActions()
  {
    var input = new InputSnapshot();
    input.BeginFrame([GameAction.MoveLeft, GameAction.MoveRight]);

    var intent = AvatarInputIntent.FromInput(input);

    Assert.Equal(new AvatarInputIntent(MoveX: 0), intent);
  }

  [Fact]
  public void FixedUpdate_WithRightIntentMovesBodyRightOnePixel()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    var body = new KinematicBody(new Int2(4, 4), new Int2(8, 8));
    var controller = new AvatarController(body);

    controller.FixedUpdate(world, new AvatarInputIntent(MoveX: 1));

    Assert.Equal(new Int2(5, 4), body.Position);
  }

  [Fact]
  public void FixedUpdate_WithRightIntentStopsAtWall()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    world.SetSolid(tileX: 1, tileY: 0, solid: true);
    var body = new KinematicBody(new Int2(8, 4), new Int2(8, 8));
    var controller = new AvatarController(body);

    controller.FixedUpdate(world, new AvatarInputIntent(MoveX: 1));

    Assert.Equal(new Int2(8, 4), body.Position);
  }
}