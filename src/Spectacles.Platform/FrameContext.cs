namespace Spectacles.Platform;

public readonly record struct FrameContext(
  int FrameINdex,
  float FixedDeltaSeconds,
  float VariableDeltaSeconds,
  float InterpolationAlpha
);