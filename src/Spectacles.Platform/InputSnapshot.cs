namespace Spectacles.Platform;

public sealed class InputSnapshot
{
  private readonly HashSet<GameAction> _down = [];
  private readonly HashSet<GameAction> _pressed = [];
  private readonly HashSet<GameAction> _released = [];

  public bool IsDown(GameAction action) => _down.Contains(action);
  public bool WasPressed(GameAction action) => _pressed.Contains(action);
  public bool WasReleased(GameAction action) => _released.Contains(action);

  public void BeginFrame(IEnumerable<GameAction> actionsDownThisFrame)
  {
    var previousDown = _down.ToHashSet();
    var currentDown = actionsDownThisFrame.ToHashSet();

    _down.Clear();
    _pressed.Clear();
    _released.Clear();

    foreach (var action in currentDown)
    {
      _down.Add(action);

      if (!previousDown.Contains(action))
      {
        _pressed.Add(action);
      }
    }

    foreach (var action in previousDown)
    {
      if (!currentDown.Contains(action))
      {
        _released.Add(action);
      }
    }
  }
}