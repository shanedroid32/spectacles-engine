namespace Spectacles.Gameplay;

public readonly record struct SurfaceState(
    bool Grounded,
    bool TouchingCeiling,
    bool TouchingLeftWall,
    bool TouchingRightWall);