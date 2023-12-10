using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public abstract class TileAtlas
{
    public readonly int AtlasId;
    public readonly MapTile[] Tiles;

    protected TileAtlas(MapTile[] tiles, int atlasId)
    {
        Tiles = tiles;
        AtlasId = atlasId;
    }
    
    public byte[] ValidInitialTiles() => 
        Tiles.Select(tile => tile.Id)
            .ToArray();
}