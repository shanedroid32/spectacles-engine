using Spectacles.Platform;

namespace Spectacles.Gameplay;

public readonly record struct AvatarInputIntent(int MoveX)
{
  public static AvatarInputIntent FromInput(InputSnapshot input)
  {
    var moveX = 0;

    if (input.IsDown(GameAction.MoveLeft))
      moveX--;

    if (input.IsDown(GameAction.MoveRight))
      moveX++;

    return new AvatarInputIntent(moveX);
  }
}