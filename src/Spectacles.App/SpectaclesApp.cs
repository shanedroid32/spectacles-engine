using Foster.Framework;
using Spectacles.Platform;

namespace Spectacles.App;

public sealed class SpectaclesApp : Foster.Framework.App
{
  private Runtime _runtime = null!;
  private Presentation _presentation = null!;

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
    _presentation = new Presentation(
      new Presentation.PresentationConfig(),
      windowWidth: 1280,
      windowHeight: 720
    );

    var clocks = new FakeClockSource();
    var input = new FakeInputSource();
    var content = new FakeContentStore();
    var world = new FakeWorldHost();
    var renderer = new FakeRenderHost();

    _runtime = new Runtime
    {
      Presentation = _presentation,
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
}