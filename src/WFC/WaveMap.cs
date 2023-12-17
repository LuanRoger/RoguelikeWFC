using RoguelikeWFC.Tiles;
using RoguelikeWFC.Utils;

namespace RoguelikeWFC.WFC;

public class WaveMap
{
    public int width { get; }
    public int height { get; }
    private readonly TileAtlas _tileAtlas;
    public int AtalsId => _tileAtlas.AtlasId;
    private IEnumerable<MapTile> tiles => _tileAtlas.Tiles;
    private Wave wave { get; set; }
    public int waveLength => wave.wave.Length;
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
                    !includeConflicts && wavePossition.conflict ||
                    wavePossition.Entropy.Length >= smallerEntropyLength) 
                    continue;
                
                smallerEntropyLength = wavePossition.Entropy.Length;
                smallerEntropyRow = row;
                smallerEntropyCol = col;
            }
        }

        return new(smallerEntropyRow, smallerEntropyCol);
    }
    
    public WavePossition GetPossitionAtPoint(WavePossitionPoint point) =>
        wave.wave[point.row, point.column];
    
    public void UpdateEntropyAt(WavePossitionPoint possitionPoint, byte[] newEntropy) =>
        wave.UpdateEntropyAt(possitionPoint, newEntropy);
    
    public bool IsTileIsolation(ref WavePossitionPoint tilePossition)
    {
        WavePossition possition = GetPossitionAtPoint(tilePossition);
        MapTile tileAtPossition = GetTileAtPossition(tilePossition.row, tilePossition.column);
        
        if(tileAtPossition.CanBeIsolated)
            return true;

        WavePossitionArea possitionArea = new(tilePossition);
        
        try
        {
            WavePossition onTopPossition = GetPossitionAtPoint(possitionArea.Top);
            if (onTopPossition.collapsed && onTopPossition.Entropy[0] == tileAtPossition.Id)
                return false;
        }
        catch (IndexOutOfRangeException) { /* ignored */ }

        try
        {
            WavePossition onRightPossition = GetPossitionAtPoint(possitionArea.Right);
            if (onRightPossition.collapsed && onRightPossition.Entropy[0] == tileAtPossition.Id)
                return false;
        }
        catch (IndexOutOfRangeException) { /* ignored */ }

        try
        {
            WavePossition onBottomPossition = GetPossitionAtPoint(possitionArea.Bottom);
            if (onBottomPossition.collapsed && onBottomPossition.Entropy[0] == tileAtPossition.Id)
                return false;
        }
        catch (IndexOutOfRangeException) { /* ignored */ }

        try
        {
            WavePossition onLeftPossition = GetPossitionAtPoint(possitionArea.Left);
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if(onLeftPossition.collapsed && onLeftPossition.Entropy[0] == tileAtPossition.Id)
                return false;
        }
        catch (IndexOutOfRangeException) { /* ignored */ }

        return true;
    }
    
    public bool AllCollapsed() =>
        wave.AllCollapsed();
    
    public int GetCountOfCollapsedTiles()
    {
        int count = 0;
        foreach (WavePossition wavePossition in wave.wave)
        {
            if(wavePossition is { collapsed: true, conflict: false })
                count++;
        }
        
        return count;
    }
    
    public bool HasOnlyConflicts()
    {
        foreach (WavePossition wavePossition in wave.wave)
        {
            if (wavePossition is { collapsed: false, conflict: false })
                return false;
        }

        return true;
    }

    public bool HasTileIsolation()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                WavePossitionPoint possitionPoint = new(row, col);
                if (!IsTileIsolation(ref possitionPoint))
                    return false;
            }
        }

        return true;
    }
    
    public int GetCountOfConflicts()
    {
        int count = 0;
        foreach (WavePossition wavePossition in wave.wave)
        {
            if(wavePossition.conflict)
                count++;
        }
        
        return count;
    }

    public byte GetRandomTileFromPossition(WavePossitionPoint possition)
    {
        Random rng = new();
        
        WavePossition wavePossition = GetPossitionAtPoint(possition);
        
        if (_tileAtlas.TileFrequency is not null)
        {
            var filteredFrequencyByEntropy = _tileAtlas.TileFrequency
                .Where(p => wavePossition.Entropy.Contains(p.Key))
                .ToDictionary();
            return TileFrequencyBased.PickTileIdByFrequency(ref rng, filteredFrequencyByEntropy);
        }
        
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
        if(possition.conflict)
            return _nullTile;
        if(!possition.collapsed)
            return new TextTile(possition.Entropy.Length.ToString()[0]);
        
        int tileId = possition.Entropy[0];
        return GetTileById(tileId);
    }
    public MapTile GetTileAtPossition(WavePossitionPoint possitionPoint) =>
        GetTileAtPossition(possitionPoint.row, possitionPoint.column);
    
    public void Reset()
    {
        var wavePossitions = InitializeWavePossitions();
        wave = new(width, height, wavePossitions);
    }
}