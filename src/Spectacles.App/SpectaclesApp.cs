using Spectacles.Platform;

namespace Spectacles.App;

public sealed class SpectaclesApp
{
  private Runtime _runtime = null!;

  public void Startup()
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

  public void FixedUpdate(FrameContext frame) { }
  public void VariableUpdate(FrameContext frame) { }
  public void Render(FrameContext frame) { }
  public void Shutdown() { }
}