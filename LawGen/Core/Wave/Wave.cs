using LawGen.Core.Tiling;
using LawGen.Core.Tiling.Internals;
using LawGen.Core.Wave.Types;
using LawGen.Exceptions;
using LawGen.WFC.Utils;

namespace LawGen.Core.Wave;

public partial class Wave
{
    public readonly int Width;
    public readonly int Height;
    private readonly TileAtlas _tileAtlas;
    public byte AtalsId => _tileAtlas.AtlasId;
    private IEnumerable<MapTile> tiles => _tileAtlas.Tiles;
    private WaveMatrix waveMatrix { get; set; }
    public int WaveLength => waveMatrix.wave.Length;

    public Wave(int width, int height, TileAtlas tileAtlas)
    {
        Width = width;
        Height = height;
        _tileAtlas = tileAtlas;
        
        var wavePossitions = InitializeWavePossitions();
        waveMatrix = new(width, height, wavePossitions);
    }
    
    public byte[] ValidInitialTiles() => 
        _tileAtlas.ValidInitialTiles();
    
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