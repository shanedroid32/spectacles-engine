namespace Spectacles.App;

public readonly record struct FrameContext(
  int FrameINdex,
  float FixedDelta,
  float VisualDelta,
  float InterpolationAlpha
);