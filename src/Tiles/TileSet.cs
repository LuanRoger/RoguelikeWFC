using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public abstract class TileSet
{
    public readonly MapTile[] Tiles;

    protected TileSet(MapTile[] tiles)
    {
        Tiles = tiles;
    }
    
    public abstract int[] ValidInitialTiles();
}