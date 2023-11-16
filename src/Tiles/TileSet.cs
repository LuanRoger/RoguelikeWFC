using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public abstract class TileSet
{
    public readonly MapTile[] Tiles;
    public abstract byte tileIdGenerator { get; }
    
    protected TileSet(MapTile[] tiles)
    {
        Tiles = tiles;
    }
    
    public MapTile GetMapTileFromID(byte id) => 
        Tiles.First(tile => tile.id == id);
}