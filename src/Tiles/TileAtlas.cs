using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public abstract class TileAtlas
{
    public readonly MapTile[] Tiles;

    protected TileAtlas(MapTile[] tiles)
    {
        Tiles = tiles;
    }
    
    public byte[] ValidInitialTiles() => 
        Tiles.Select(tile => tile.id)
            .ToArray();
}