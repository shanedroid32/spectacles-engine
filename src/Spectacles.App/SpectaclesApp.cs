using Foster.Framework;
using Spectacles.Platform;

namespace Spectacles.App;

public sealed class SpectaclesApp : Foster.Framework.App
{
  private Runtime _runtime = null!;

  public SpectaclesApp() : base(new AppConfig()
  {
    ApplicationName = "Spectacles",
    WindowTitle = "Spectacles",
    Width = 320,
    Height = 180,
    Resizable = true
  })
  {

  }

  protected override void Startup()
  {
    var clocks = new FakeClockSource();
    var input = new FakeInputSource();
    var content = new FakeContentStore();
    var world = new FakeWorldHost();
    var renderer = new FakeRenderHost();

    _runtime = new Runtime
    {
      Clocks = clocks,
      Input = input,
      Content = content,
      World = world,
      Renderer = renderer,
    };
  }

  protected override void Update()
  {

  }

  protected override void Render()
  {

  }

  protected override void Shutdown()
  {

  }

  public static RectI CalculatePresentationRect(
    int windowWidth,
    int windowHeight,
    int internalWidth = 320,
    int internalHeight = 180
  )
  {
    var scaleX = windowWidth / internalWidth;
    var scaleY = windowHeight / internalHeight;
    var scale = Math.Max(1, Math.Min(scaleX, scaleY));

    var width = internalWidth * scale;
    var height = internalHeight * scale;
    var x = (windowWidth - width) / 2;
    var y = (windowHeight - height) / 2;

    return new RectI(x, y, width, height);
  }
}