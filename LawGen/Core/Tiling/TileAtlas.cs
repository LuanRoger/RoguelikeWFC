namespace LawGen.Core.Tiling;

public abstract class TileAtlas(MapTile[] tiles, byte atlasId, CollapseFrequency[]? tileFrequency = null)
{
    public readonly byte AtlasId = atlasId;
    public readonly MapTile[] Tiles = tiles;
    public readonly CollapseFrequency[]? TileFrequency = tileFrequency;
    
    public MapTile GetAtlasTileById(byte id) => 
        Tiles.First(tile => tile.Id == id);
    
    public virtual byte[] ValidInitialTiles() => 
        Tiles.Select(tile => tile.Id)
            .ToArray();
}