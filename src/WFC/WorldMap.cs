﻿using RoguelikeWFC.Tiles;

namespace RoguelikeWFC.WFC;

public class WorldMap
{
    public readonly int width;
    public readonly int height;
    public MapTile[,] tiles { get; }
    private readonly MapTile _nullTile = new NullTile();

    public WorldMap(int width, int height)
    {
        this.width = width;
        this.height = height;
        tiles = new MapTile[width, height];
        InitializeEmptyMap();
    }
    
    public WorldMap(int width, int height, MapTile[,] tiles)
    {
        this.width = width;
        this.height = height;
        this.tiles = tiles;
    }
    
    private void InitializeEmptyMap()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                tiles[row, col] = _nullTile;
            }
        }
    } 
}