using RoguelikeWFC.Tiles;

namespace RoguelikeWFC.WFC;

public class WaveMap
{
    public int width { get; }
    public int height { get; }
    private readonly TileAtlas _tileAtlas;
    private IEnumerable<MapTile> tiles => _tileAtlas.Tiles;
    private Wave wave { get; set; }
    private readonly MapTile _nullTile = new NullTile();

    public WaveMap(int width, int height, TileAtlas tileAtlas)
    {
        this.width = width;
        this.height = height;
        _tileAtlas = tileAtlas;
        
        var wavePossitions = InitializeWavePossitions();
        wave = new(width, height, wavePossitions);
    }
    
    private WavePossition[,] InitializeWavePossitions()
    {
        var wavePossitions = new WavePossition[height, width];
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
                wavePossitions[row, col] = new(ValidInitialTiles());
        }
        
        return wavePossitions;
    }
    
    public byte[] ValidInitialTiles() => 
        _tileAtlas.ValidInitialTiles();
    
    public WavePossitionPoint GetSmallerEntropyPossition(bool includeConflicts = false)
    {
        int smallerEntropyRow = 0;
        int smallerEntropyCol = 0;
        int smallerEntropyLength = int.MaxValue;
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                WavePossition wavePossition = wave.wave[row, col];
                
                if (wavePossition.collapsed ||
                    !includeConflicts && wavePossition.hasConflict ||
                    wavePossition.Entropy.Length >= smallerEntropyLength) 
                    continue;
                
                smallerEntropyLength = wavePossition.Entropy.Length;
                smallerEntropyRow = row;
                smallerEntropyCol = col;
            }
        }

        return new(smallerEntropyRow, smallerEntropyCol);
    }
    
    public WavePossition GetPossitionAt(WavePossitionPoint point) =>
        wave.wave[point.row, point.column];
    public void UpdateEntropyAt(WavePossitionPoint possitionPoint, byte[] newEntropy) =>
        wave.UpdateEntropyAt(possitionPoint, newEntropy);
    public bool AllCollapsed() =>
        wave.AllCollapsed();
    
    public bool HasOnlyConflicts()
    {
        foreach (WavePossition wavePossition in wave.wave)
        {
            if (wavePossition is { collapsed: false, hasConflict: false })
                return false;
        }

        return true;
    }

    public byte GetRandomTileFromPossition(WavePossitionPoint possition)
    {
        Random rng = new();
        WavePossition wavePossition = GetPossitionAt(possition);
        
        int tileIndex = rng.Next(0, wavePossition.Entropy.Length);
        return wavePossition.Entropy[tileIndex];
    }
    
    public MapTile GetTileById(int id)
    {
        foreach (MapTile tile in tiles)
        {
            if(tile.Id == id) 
                return tile;
        }
        
        return _nullTile;
    }
    
    public MapTile GetTileAtPossition(int rowIndex, int columnIndex)
    {
        WavePossition possition = wave.wave[rowIndex, columnIndex];
        if(possition.hasConflict)
            return _nullTile;
        if(!possition.collapsed)
            return new TextTile(possition.Entropy.Length.ToString()[0]);
        
        int tileId = possition.Entropy[0];
        return GetTileById(tileId);
    }
    
    public void Reset()
    {
        var wavePossitions = InitializeWavePossitions();
        wave = new(width, height, wavePossitions);
    }
}