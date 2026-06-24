namespace Spectacles.Platform;

public sealed class InputBindings
{
  private readonly Dictionary<RawKey, GameAction> _keyToAction = new()
  {
    [RawKey.A] = GameAction.MoveLeft,
    [RawKey.D] = GameAction.MoveRight,
    [RawKey.Space] = GameAction.Jump,
    [RawKey.E] = GameAction.Interact,
  };

  public IReadOnlyCollection<GameAction> MapKeys(IEnumerable<RawKey> keysDown)
  {
    var actions = new HashSet<GameAction>();

    foreach (var key in keysDown)
    {
      if (_keyToAction.TryGetValue(key, out var action))
      {
        actions.Add(action);
      }
    }

    return actions;
  }
}