using Spectacles.App;

namespace Spectacles.Platform;

public sealed class Runtime
{
  public required IClockSource Clocks { get; init; }
  public required IInputSource Input { get; init; }
  public required IContentStore Content { get; init; }
  public required IWorldHost World { get; init; }
  public required IRenderHost Renderer { get; init; }
}