using Spectacles.Platform;

namespace Spectacles.Tests.Platform;

public sealed class InputSnapshotTests
{
  [Fact]
  public void BeginFrame_DetectsPressedAction()
  {
    var input = new InputSnapshot();

    input.BeginFrame([GameAction.Jump]);

    Assert.True(input.IsDown(GameAction.Jump));
    Assert.True(input.WasPressed(GameAction.Jump));
    Assert.False(input.WasReleased(GameAction.Jump));
  }

  [Fact]
  public void BeginFrame_DetectsReleasedAction()
  {
    var input = new InputSnapshot();

    input.BeginFrame([GameAction.Jump]);
    input.BeginFrame([]);

    Assert.False(input.IsDown(GameAction.Jump));
    Assert.False(input.WasPressed(GameAction.Jump));
    Assert.True(input.WasReleased(GameAction.Jump));
  }
}