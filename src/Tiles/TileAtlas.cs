using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public abstract class TileAtlas(MapTile[] tiles, int atlasId)
{
    public readonly int AtlasId = atlasId;
    public readonly MapTile[] Tiles = tiles;

    public byte[] ValidInitialTiles() => 
        Tiles.Select(tile => tile.Id)
            .ToArray();
    public MapTile GetAtlasTileById(byte id) => 
        Tiles.First(tile => tile.Id == id);
}