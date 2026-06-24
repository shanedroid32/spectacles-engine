namespace Spectacles.Platform;

public readonly record struct RectI(
  int X,
  int Y,
  int Width,
  int Height
)
{
  public int Left => X;
  public int Top => Y;
  public int Right => X + Width;
  public int Bottom => Y + Height;
}