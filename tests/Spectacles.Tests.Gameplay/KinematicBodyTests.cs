using Spectacles.Gameplay;
using Spectacles.Platform;

namespace Spectacles.Tests.Gameplay;

public sealed class KinematicBodyTests
{
  [Fact]
  public void Bounds_UsePositionAndSize()
  {
    var body = new KinematicBody(
        position: new Int2(12, 20),
        size: new Int2(8, 14));

    Assert.Equal(new RectI(12, 20, 8, 14), body.Bounds);
  }

  [Fact]
  public void Bounds_UpdateWhenPositionChanges()
  {
    var body = new KinematicBody(
        position: new Int2(12, 20),
        size: new Int2(8, 14));

    body.Position = new Int2(13, 20);

    Assert.Equal(new RectI(13, 20, 8, 14), body.Bounds);
  }
}