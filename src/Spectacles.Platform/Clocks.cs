namespace Spectacles.Platform;

public sealed class Clocks
{
  private float _accumulatorSeconds;

  public int FixedStepHz { get; }
  public float FixedDeltaSeconds { get; }
  public float VariableDeltaSeconds { get; private set; }
  public float InterpolationAlpha { get; private set; }
  public int MaxFixedStepsPerFrame { get; }
  public float MaxAccumulatorSeconds { get; }

  public Clocks(
    int fixedStepHz,
    float maxAccumulatorSeconds = 0.250f,
    int maxFixedStepsPerFrame = 8)
  {
    FixedStepHz = fixedStepHz;
    FixedDeltaSeconds = 1f / fixedStepHz;
    MaxAccumulatorSeconds = maxAccumulatorSeconds;
    MaxFixedStepsPerFrame = maxFixedStepsPerFrame;
  }

  public int BeginFrame(float realDeltaSeconds)
  {
    VariableDeltaSeconds = realDeltaSeconds;
    _accumulatorSeconds += realDeltaSeconds;
    _accumulatorSeconds = Math.Min(_accumulatorSeconds, MaxAccumulatorSeconds);

    var steps = 0;
    while (_accumulatorSeconds >= FixedDeltaSeconds && steps < MaxFixedStepsPerFrame)
    {
      _accumulatorSeconds -= FixedDeltaSeconds;
      steps++;
    }

    InterpolationAlpha = _accumulatorSeconds / FixedDeltaSeconds;
    return steps;
  }

  public void SetVisualFrame(float variableDeltaSeconds, float interpolationAlpha)
  {
    VariableDeltaSeconds = variableDeltaSeconds;
    InterpolationAlpha = interpolationAlpha;
  }
}