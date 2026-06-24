namespace Spectacles.Platform;

public sealed class Presentation
{
  public sealed record PresentationConfig
  {
    public int InternalWidth { get; init; } = 320;
    public int InternalHeight { get; init; } = 180;
    public ScaleMode ScaleMode { get; init; } = ScaleMode.IntegerFit;
  }

  public Int2 InternalResolution { get; }
  public RectI PresentationRect { get; private set; }

  public Presentation(PresentationConfig config, int windowWidth, int windowHeight)
  {
    InternalResolution = new Int2(config.InternalWidth, config.InternalHeight);
    Resize(windowWidth, windowHeight);
  }

  public void Resize(int windowWidth, int windowHeight)
  {
    var scaleX = windowWidth / InternalResolution.x;
    var scaleY = windowHeight / InternalResolution.y;
    var scale = Math.Max(1, Math.Min(scaleX, scaleY));

    var width = InternalResolution.x * scale;
    var height = InternalResolution.y * scale;
    var x = (windowWidth - width) / 2;
    var y = (windowHeight - height) / 2;

    PresentationRect = new RectI(x, y, width, height);
  }
}