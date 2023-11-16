using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public abstract class TileAtlas
{
    public readonly MapTile[] Tiles;
    public readonly TileSet[] TilesSet = Array.Empty<TileSet>();

    protected TileAtlas(MapTile[] tiles)
    {
        Tiles = tiles;
    }
    protected TileAtlas(MapTile[] tiles, TileSet[] tilesSet)
    {
        Tiles = tiles;
        TilesSet = tilesSet;
    } 
    
    public byte[] ValidInitialTiles() => 
        Tiles.Select(tile => tile.id)
            .Concat(TilesSet.Select(tileSet => tileSet.tileIdGenerator))
            .ToArray();
}