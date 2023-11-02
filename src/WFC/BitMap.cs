namespace RoguelikeWFC.WFC;

public class BitMap
{
    /// <summary>
    /// Tile that can be used in this map
    /// </summary>
    public required MapTile[] tiles { get; init; }
    public required MapTile nullTile { get; init; }
    /// <summary>
    /// Map width
    /// </summary>
    public int width { get; }
    /// <summary>
    /// Map height
    /// </summary>
    public int height { get; }
    
    private Wave wave { get; }

    public BitMap(int width, int height)
    {
        this.width = width;
        this.height = height;
        wave = new(width, height);
    }
    
    public void Init()
    {
        foreach (WavePossition wavePossition in wave.wave)
            wavePossition.entropy = GetAllTilesId();
    }
    
    public void Wfc()
    {
        while (!wave.AllCollapsed())
            InterateWfcOnce();
    }
    public void InterateWfcOnce()
    {
        WavePossition possition = GetSmallerEntropyPossition();
        int tileId = GetRandomTileFromPossition(possition);
        //Collapse into one tile
        possition.entropy = new[] {tileId};
            
        PropagateState();
    }
    
    public void PropagateState()
    {
        for (int row = 0; row < height - 1; row++)
        {
            for (int col = 0; col < width - 1; col++)
            {
                WavePossition possition = wave.wave[col, row];
                if(!possition.collapsed)
                    continue;
                
                int tileId = possition.entropy[0];
                TileSocket socketMapTile = GetTileById(tileId)!.tileSocket;
                
                //Get surrounding tiles
                int left = col - 1;
                int right = col + 1;
                int top = row - 1;
                int bottom = row + 1;
                
                List<int> currentEntropy;
                List<int> newEntropy;
                IEnumerable<int> enumerable;
                if(left >= 0 && left < width)
                {
                    WavePossition leftPossition = wave.wave[left, row];
                    currentEntropy = leftPossition.entropy.ToList();
                    newEntropy = socketMapTile.canFit.ToList();
                    enumerable = currentEntropy.Intersect(newEntropy);
                    leftPossition.entropy = enumerable.ToArray();
                }
                
                if(right >= 0 && right < width)
                {
                    WavePossition rightPossition = wave.wave[right, row];
                    currentEntropy = rightPossition.entropy.ToList();
                    newEntropy = socketMapTile.canFit.ToList();
                    enumerable = currentEntropy.Intersect(newEntropy);
                    rightPossition.entropy = enumerable.ToArray();
                }
                
                if(top >= 0 && top < height)
                {
                    WavePossition topPossition = wave.wave[col, top];
                    currentEntropy = topPossition.entropy.ToList();
                    newEntropy = socketMapTile.canFit.ToList();
                    enumerable = currentEntropy.Intersect(newEntropy);
                    topPossition.entropy = enumerable.ToArray();   
                }
                
                // ReSharper disable once InvertIf
                if(bottom >= 0 && bottom < height)
                {
                    WavePossition bottomPossition = wave.wave[col, bottom];
                    currentEntropy = bottomPossition.entropy.ToList();
                    newEntropy = socketMapTile.canFit.ToList();
                    enumerable = currentEntropy.Intersect(newEntropy);
                    bottomPossition.entropy = enumerable.ToArray();
                }
            }
        }
    }

    private WavePossition GetSmallerEntropyPossition()
    {
        WavePossition smallerEntropy = wave.wave[0, 0];
        int smallerEntropyLength = int.MaxValue;
        foreach (WavePossition wavePossition in wave.wave)
        {
            if (wavePossition.collapsed ||
                wavePossition.entropy.Length >= smallerEntropyLength) 
                continue;
            
            smallerEntropy = wavePossition;
            smallerEntropyLength = wavePossition.entropy.Length;
        }

        return smallerEntropy;
    }

    private int GetRandomTileFromPossition(WavePossition possition)
    {
        Random rng = new();
        int tileIndex = rng.Next(0, possition.entropy.Length);
        return possition.entropy[tileIndex];
    }
    
    public MapTile? GetTileById(int id)
    {
        foreach (MapTile tile in tiles)
        {
            if(tile.id == id) 
                return tile;
        }
        
        return null;
    }
    
    public MapTile GetTileAtPossition(int columnIndex, int rowIndex)
    {
        WavePossition possition = wave.wave[columnIndex, rowIndex];
        if(!possition.collapsed)
            return new TextTile(possition.entropy.Length.ToString()[0]);
        
        int tileId = possition.entropy[0];
        return GetTileById(tileId) ?? nullTile;
    }
    
    private int[] GetAllTilesId() =>
        tiles.Select(tile => tile.id).ToArray();
}