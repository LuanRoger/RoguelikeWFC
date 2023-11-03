using RoguelikeWFC.Tiles;

namespace RoguelikeWFC.WFC;

public class BitMap
{
    private readonly TileSet _tileSet;
    private MapTile[] tiles => _tileSet.Tiles;
    /// <summary>
    /// Map width
    /// </summary>
    private int width { get; }
    /// <summary>
    /// Map height
    /// </summary>
    private int height { get; }
    
    private Wave wave { get; }
    private readonly MapTile _nullTile = new NullTile();

    public BitMap(int width, int height, TileSet tileSet)
    {
        this.width = width;
        this.height = height;
        wave = new(width, height);
        _tileSet = tileSet;
    }
    
    public void Init()
    {
        foreach (WavePossition wavePossition in wave.wave)
            wavePossition.entropy = _tileSet.ValidInitialTiles();
    }
    
    public void Wfc()
    {
        while (!wave.AllCollapsed())
            InterateWfcOnce();
    }
    public void InterateWfcOnce()
    {
        WavePossition possition = GetSmallerEntropyPossition();
        byte tileId = GetRandomTileFromPossition(possition);
        possition.entropy = new[] { tileId };

        do
        {
            PropagateState();
        } while (UnpropagateNonCollapsed());
    }
    
    public bool UnpropagateNonCollapsed()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                WavePossition possition = wave.wave[col, row];
                if(possition.entropy.Any())
                    continue;
                
                possition.entropy = _tileSet.ValidInitialTiles();
                
                int top = row - 1;
                int right = col + 1;
                int bottom = row + 1;
                int left = col - 1;
                
                if(left >= 0 && left < width)
                {
                    WavePossition leftPossition = wave.wave[left, row];
                    leftPossition.entropy = _tileSet.ValidInitialTiles();
                }
                if(right >= 0 && right < width)
                {
                    WavePossition rightPossition = wave.wave[right, row];
                    rightPossition.entropy = _tileSet.ValidInitialTiles();
                }
                if(top >= 0 && top < height)
                {
                    WavePossition topPossition = wave.wave[col, top];
                    topPossition.entropy = _tileSet.ValidInitialTiles();   
                }
                // ReSharper disable once InvertIf
                if(bottom >= 0 && bottom < height)
                {
                    WavePossition bottomPossition = wave.wave[col, bottom];
                    bottomPossition.entropy = _tileSet.ValidInitialTiles();
                }
                
                return true;
            }
        }
        
        return false;
    }
    
    public void PropagateState()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                WavePossition possition = wave.wave[col, row];
                if(!possition.collapsed)
                    continue;
                
                byte tileId = possition.entropy[0];
                TileSocket socketMapTile = GetTileById(tileId)!.tileSocket;
                
                int top = row - 1;
                int right = col + 1;
                int bottom = row + 1;
                int left = col - 1;
                
                List<byte> currentEntropy;
                List<byte> newEntropy;
                IEnumerable<byte> enumerable;
                if(left >= 0 && left < width)
                {
                    WavePossition leftPossition = wave.wave[left, row];
                    currentEntropy = leftPossition.entropy.ToList();
                    newEntropy = socketMapTile.fitLeft.ToList();
                    enumerable = currentEntropy.Intersect(newEntropy);
                    leftPossition.entropy = enumerable.ToArray();
                }
                LeftEndPropagation:
                
                if(right >= 0 && right < width)
                {
                    WavePossition rightPossition = wave.wave[right, row];
                    currentEntropy = rightPossition.entropy.ToList();
                    newEntropy = socketMapTile.fitRight.ToList();
                    enumerable = currentEntropy.Intersect(newEntropy);
                    rightPossition.entropy = enumerable.ToArray();
                }
                RightEndPropagation:
                
                if(top >= 0 && top < height)
                {
                    WavePossition topPossition = wave.wave[col, top];
                    currentEntropy = topPossition.entropy.ToList();
                    newEntropy = socketMapTile.fitTop.ToList();
                    enumerable = currentEntropy.Intersect(newEntropy);
                    topPossition.entropy = enumerable.ToArray();   
                }
                TopEndPropagation:
                
                // ReSharper disable once InvertIf
                if(bottom >= 0 && bottom < height)
                {
                    WavePossition bottomPossition = wave.wave[col, bottom];
                    currentEntropy = bottomPossition.entropy.ToList();
                    newEntropy = socketMapTile.fitBottom.ToList();
                    enumerable = currentEntropy.Intersect(newEntropy);
                    bottomPossition.entropy = enumerable.ToArray();
                }
                BottomEndPropagation:;
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

    private static byte GetRandomTileFromPossition(WavePossition possition)
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
        return GetTileById(tileId) ?? _nullTile;
    }
}