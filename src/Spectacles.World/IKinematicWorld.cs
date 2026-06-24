using Spectacles.Platform;

namespace Spectacles.World;

public interface IKinematicWorld
{
  bool OverlapsSolid(RectI rect);
}
