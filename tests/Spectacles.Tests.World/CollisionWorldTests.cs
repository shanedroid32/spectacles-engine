using Spectacles.Platform;
using Spectacles.World;

namespace Spectacles.Tests.World;

public sealed class CollisionWorldTests
{
  [Fact]
  public void OverlapsSolid_ReturnsFalseForEmptySpace()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);

    var overlaps = world.OverlapsSolid(new RectI(0, 0, 8, 8));

    Assert.False(overlaps);
  }

  [Fact]
  public void OverlapsSolid_ReturnsTrueWhenRectTouchesSolidTile()
  {
    var world = new CollisionWorld(width: 4, height: 4, tileSize: 16);
    world.SetSolid(tileX: 1, tileY: 0, solid: true);

    var overlaps = world.OverlapsSolid(new RectI(15, 0, 2, 8));

    Assert.True(overlaps);
  }
}