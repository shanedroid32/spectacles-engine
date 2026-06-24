using Spectacles.Platform;

namespace Spectacles.Gameplay;

public sealed class KinematicBody
{
  public Int2 Position { get; set; }
  public Int2 Size { get; }

  public KinematicBody(Int2 position, Int2 size)
  {
    Position = position;
    Size = size;
  }

  public RectI Bounds => new(Position.x, Position.y, Size.x, Size.y);
}