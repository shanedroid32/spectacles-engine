using Spectacles.Platform;

namespace Spectacles.Tests.Platform;

public sealed class InputBindingsTests
{
  [Fact]
  public void MapKeys_MapsSpaceToJump()
  {
    var bindings = new InputBindings();

    var actions = bindings.MapKeys([RawKey.Space]);

    Assert.Contains(GameAction.Jump, actions);
  }

  [Fact]
  public void MapKeys_MapsMultipleKeysToMultipleActions()
  {
    var bindings = new InputBindings();

    var actions = bindings.MapKeys([RawKey.A, RawKey.Space]);

    Assert.Contains(GameAction.MoveLeft, actions);
    Assert.Contains(GameAction.Jump, actions);
  }
}