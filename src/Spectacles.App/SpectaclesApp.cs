using Foster.Framework;
using Spectacles.Platform;

namespace Spectacles.App;

public sealed class SpectaclesApp : Foster.Framework.App
{
  private Runtime _runtime = null!;
  private Presentation _presentation = null!;
  private int _frameIndex;

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

    var clocks = new Clocks(fixedStepHz: 120);
    var input = new FakeInputSource();
    var content = new FakeContentStore();
    var world = new FakeWorldHost();
    var renderer = new FakeRenderHost();

    _runtime = new Runtime
    {
      Presentation = _presentation,
      Clocks = clocks,
      Input = new InputSnapshot(),
      Content = content,
      World = world,
      Renderer = renderer,
    };
  }

  protected override void Update()
  {
    var fixedSteps = _runtime.Clocks.BeginFrame(realDeltaSeconds: 120);

    for (int i = 0; i < fixedSteps; i++)
    {
      var fixedFrame = CreateFrameContext();
      FixedUpdate(fixedFrame);
    }

    var renderFrame = CreateFrameContext();
    Render(renderFrame);
  }

  private void FixedUpdate(FrameContext frame)
  {
    Console.WriteLine($"fixed dt: {frame.FixedDeltaSeconds}");
  }

  private void Render(FrameContext frame)
  {
    Console.WriteLine($"alpha: {frame.InterpolationAlpha}");
  }

  protected override void Render()
  {

  }

  protected override void Shutdown()
  {

  }

  private FrameContext CreateFrameContext()
  {
    return new FrameContext(
      FrameINdex: _frameIndex++,
      FixedDeltaSeconds: _runtime.Clocks.FixedDeltaSeconds,
      VariableDeltaSeconds: _runtime.Clocks.VariableDeltaSeconds,
      InterpolationAlpha: _runtime.Clocks.InterpolationAlpha
    );
  }
}