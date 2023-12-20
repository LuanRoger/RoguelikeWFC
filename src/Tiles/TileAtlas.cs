using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public abstract class TileAtlas(MapTile[] tiles, int atlasId, Dictionary<byte, float>? tileFrequency = null)
{
    public readonly int AtlasId = atlasId;
    public readonly MapTile[] Tiles = tiles;
    public readonly Dictionary<byte, float>? TileFrequency = tileFrequency;
    
    public MapTile GetAtlasTileById(byte id) => 
        Tiles.First(tile => tile.Id == id);
    
    public virtual byte[] ValidInitialTiles() => 
        Tiles.Select(tile => tile.Id)
            .ToArray();
}