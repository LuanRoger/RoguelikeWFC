using LawGen.Core.Tiling;
using LawGen.Core.Tiling.Internals;
using LawGen.Core.Wave.Types;
using LawGen.Exceptions;
using LawGen.WFC.Utils;

namespace LawGen.Core.Wave;

public partial class WaveMap
{
    public readonly int Width;
    public readonly int Height;
    private readonly TileAtlas _tileAtlas;
    public byte AtalsId => _tileAtlas.AtlasId;
    private IEnumerable<MapTile> tiles => _tileAtlas.Tiles;
    private WaveMatrix waveMatrix { get; set; }
    public int WaveLength => waveMatrix.wave.Length;

    public WaveMap(int width, int height, TileAtlas tileAtlas)
    {
        Width = width;
        Height = height;
        _tileAtlas = tileAtlas;
        
        var wavePossitions = InitializeWavePossitions();
        waveMatrix = new(width, height, wavePossitions);
    }
    
    private WavePossition[,] InitializeWavePossitions()
    {
        var wavePossitions = new WavePossition[Height, Width];
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
                wavePossitions[row, col] = new(_tileAtlas.ValidInitialTiles());
        }
        
        return wavePossitions;
    }
    
    public byte[] ValidInitialTiles() => 
        _tileAtlas.ValidInitialTiles();
    
    public bool IsTileIsolation(ref WavePossitionPoint tilePossition)
    {
        WavePossition possition = GetPossitionAtPoint(tilePossition);
        MapTile tileAtPossition = GetTileAtPossition(tilePossition.row, tilePossition.column);
        
        if(possition.conflict || !possition.collapsed || tileAtPossition.IsolationGroup is null)
            return true;

        WavePossitionArea possitionArea = new(tilePossition);
        byte[] allowedSideTilesByIsolationRule = tileAtPossition.IsolationGroup;
        
        try
        {
            WavePossition onTopPossition = GetPossitionAtPoint(possitionArea.Top);
            byte topTileId = onTopPossition.Entropy[0];
            bool isAllowedTopTile = allowedSideTilesByIsolationRule.Contains(topTileId);
            
            if (onTopPossition.collapsed && isAllowedTopTile)
                return true;
        }
        catch (IndexOutOfRangeException) { /* ignored */ }

        try
        {
            WavePossition onRightPossition = GetPossitionAtPoint(possitionArea.Right);
            byte rightTileId = onRightPossition.Entropy[0];
            bool isAllowedRightTile = allowedSideTilesByIsolationRule.Contains(rightTileId);
            
            if (onRightPossition.collapsed && isAllowedRightTile)
                return true;
        }
        catch (IndexOutOfRangeException) { /* ignored */ }

        try
        {
            WavePossition onBottomPossition = GetPossitionAtPoint(possitionArea.Bottom);
            byte bottomTileId = onBottomPossition.Entropy[0];
            bool isAllowedBottomTile = allowedSideTilesByIsolationRule.Contains(bottomTileId);
            
            if (onBottomPossition.collapsed && isAllowedBottomTile)
                return true;
        }
        catch (IndexOutOfRangeException) { /* ignored */ }

        try
        {
            WavePossition onLeftPossition = GetPossitionAtPoint(possitionArea.Left);
            byte leftTileId = onLeftPossition.Entropy[0];
            bool isAllowedLeftTile = allowedSideTilesByIsolationRule.Contains(leftTileId);
            
            if(onLeftPossition.collapsed && isAllowedLeftTile)
                return true;
        }
        catch (IndexOutOfRangeException) { /* ignored */ }

        return false;
    }
    
    /// <summary>
    /// Check if the offset area around the tile is out of bound.
    /// </summary>
    /// <param name="possitionArea">The tile area</param>
    /// <param name="top">The top side is out of bounds</param>
    /// <param name="right">The right side is out of bounds</param>
    /// <param name="bottom">The bottom side is out of bounds</param>
    /// <param name="left">The left side is out of bounds</param>
    /// <returns>Return <c>true</c> if one of the sides is out of bound</returns>
    public bool CheckAreaOutOfBound(in WavePossitionArea possitionArea, out bool top, out bool right, out bool bottom, out bool left)
    {
        top = possitionArea.TopRaw < 0 || possitionArea.TopRaw >= Height;
        right = possitionArea.RightRaw < 0 || possitionArea.RightRaw >= Width;
        bottom = possitionArea.BottomRaw < 0 || possitionArea.BottomRaw >= Height;
        left = possitionArea.LeftRaw < 0 || possitionArea.LeftRaw >= Width;

        return top || right || bottom || left;
    }

    public byte GetRandomTileFromPossition(WavePossitionPoint possition)
    {
        Random rng = new();
        
        WavePossition wavePossition = GetPossitionAtPoint(possition);
        
        if (_tileAtlas.TileFrequency is not null)
        {
            var filteredFrequencyByEntropy = _tileAtlas.TileFrequency
                .Where(p => wavePossition.Entropy.Contains(p.TileId))
                .ToArray();
            return TileFrequencyBased.PickTileIdByFrequency(ref rng, filteredFrequencyByEntropy);
        }
        
        int tileIndex = rng.Next(0, wavePossition.Entropy.Length);
        return wavePossition.Entropy[tileIndex];
    }
    
    public MapTile GetTileById(byte id)
    {
        foreach (MapTile tile in tiles)
        {
            if(tile.Id == id) 
                return tile;
        }

        throw new NoTileWithSuchId(id);
    }
    
    public MapTile GetTileAtPossition(int rowIndex, int columnIndex)
    {
        WavePossition possition = waveMatrix.wave[rowIndex, columnIndex];
        if(possition.conflict || !possition.collapsed)
        {
            return new SuperTile
            {
                Entropy = possition.Entropy
            };
        }
        
        byte tileId = possition.Entropy[0];
        return GetTileById(tileId);
    }
    public MapTile GetTileAtPossition(WavePossitionPoint possitionPoint) =>
        GetTileAtPossition(possitionPoint.row, possitionPoint.column);
    
    public void Reset()
    {
        var wavePossitions = InitializeWavePossitions();
        waveMatrix = new(Width, Height, wavePossitions);
    }
}