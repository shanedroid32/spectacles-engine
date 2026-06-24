using Spectacles.Platform;

namespace Spectacles.World;

public sealed class CollisionWorld
{
  private readonly bool[,] _solidTiles;

  public int Width { get; }
  public int Height { get; }
  public int TileSize { get; }

  public CollisionWorld(int width, int height, int tileSize)
  {
    Width = width;
    Height = height;
    TileSize = tileSize;
    _solidTiles = new bool[width, height];
  }

  public void SetSolid(int tileX, int tileY, bool solid)
  {
    _solidTiles[tileX, tileY] = solid;
  }

  public bool IsSolidAt(int tileX, int tileY)
  {
    if (tileX < 0 || tileY < 0 || tileX >= Width || tileY >= Height)
    {
      return true;
    }

    return _solidTiles[tileX, tileY];
  }

  public bool OverlapsSolid(RectI rect)
  {
    var leftTile = rect.Left / TileSize;
    var rightTile = (rect.Right - 1) / TileSize;
    var topTile = rect.Top / TileSize;
    var bottomTile = (rect.Bottom - 1) / TileSize;

    for (int y = topTile; y <= bottomTile; y++)
    {
      for (int x = leftTile; x <= rightTile; x++)
      {
        if (IsSolidAt(x, y))
        {
          return true;
        }
      }
    }

    return false;
  }
}