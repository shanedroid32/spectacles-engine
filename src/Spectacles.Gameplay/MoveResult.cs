namespace Spectacles.Gameplay;

public readonly record struct MoveResult(int Requested, int Moved, bool Blocked);
