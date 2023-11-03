using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public abstract class TileSet
{
    public readonly MapTile[] Tiles;

    protected TileSet(MapTile[] tiles)
    {
        Tiles = tiles;
    }
    
    public byte[] ValidInitialTiles() => 
        Tiles.Select(tile => tile.id).ToArray();
}