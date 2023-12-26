using LawGen.Core.Tiling;
using LawGen.Core.Tiling.Internals;

namespace LawGen.WFC;

public class WorldMap
{
    public readonly int Width;
    public readonly int Height;
    public readonly int AtlasId;
    public MapTile[,] tiles { get; }

    public WorldMap(int width, int height)
    {
        Width = width;
        Height = height;
        tiles = new MapTile[width, height];
        InitializeEmptyMap();
    }
    
    public WorldMap(int width, int height, MapTile[,] tiles, int atlasId)
    {
        Width = width;
        Height = height;
        this.tiles = tiles;
        AtlasId = atlasId;
    }
    
    private void InitializeEmptyMap()
    {
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
                tiles[row, col] = EmptyTile.SystemInstance;
        }
    } 
}