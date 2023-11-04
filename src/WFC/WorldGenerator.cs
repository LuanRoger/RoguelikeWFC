using RoguelikeWFC.Tiles;

namespace RoguelikeWFC.WFC;

public class WorldGenerator
{
    private readonly BitMap _bitMap;
    private Wave wave => _bitMap.wave;
    private int width => _bitMap.width;
    private int height => _bitMap.height;

    public WorldGenerator(BitMap bitMap)
    {
        _bitMap = bitMap;
        if(!_bitMap.initialized)
            _bitMap.Init();
    }
    
    public WorldGenerator(int width, int height, TileSet tileSet)
    {
        _bitMap = new(width, height, tileSet);
        if(!_bitMap.initialized)
            _bitMap.Init();
    }
    
    public void Wfc()
    {
        if(!_bitMap.initialized)
            return;
        
        while (!wave.AllCollapsed())
            InterateWfcOnce();
    }
    public void InterateWfcOnce()
    {
        if(!_bitMap.initialized)
            return;
        
        WavePossition possition = _bitMap.GetSmallerEntropyPossition();
        byte tileId = _bitMap.GetRandomTileFromPossition(possition);
        possition.entropy = new[] { tileId };

        do
        {
            PropagateState();
        } while (UnpropagateNonCollapsed());
    }
    
    private void PropagateState()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                WavePossition possition = wave.wave[row, col];
                if(!possition.collapsed)
                    continue;
                
                byte tileId = possition.entropy[0];
                TileSocket socketMapTile = _bitMap.GetTileById(tileId)!.tileSocket;
                
                int top = row - 1;
                int right = col + 1;
                int bottom = row + 1;
                int left = col - 1;
                
                List<byte> currentEntropy;
                List<byte> newEntropy;
                IEnumerable<byte> enumerable;
                if(left >= 0 && left < width)
                {
                    WavePossition leftPossition = wave.wave[row, left];
                    currentEntropy = leftPossition.entropy.ToList();
                    newEntropy = socketMapTile.fitLeft.ToList();
                    enumerable = currentEntropy.Intersect(newEntropy);
                    leftPossition.entropy = enumerable.ToArray();
                }

                if(right >= 0 && right < width)
                {
                    WavePossition rightPossition = wave.wave[row, right];
                    currentEntropy = rightPossition.entropy.ToList();
                    newEntropy = socketMapTile.fitRight.ToList();
                    enumerable = currentEntropy.Intersect(newEntropy);
                    rightPossition.entropy = enumerable.ToArray();
                }

                if(top >= 0 && top < height)
                {
                    WavePossition topPossition = wave.wave[top, col];
                    currentEntropy = topPossition.entropy.ToList();
                    newEntropy = socketMapTile.fitTop.ToList();
                    enumerable = currentEntropy.Intersect(newEntropy);
                    topPossition.entropy = enumerable.ToArray();   
                }

                // ReSharper disable once InvertIf
                if(bottom >= 0 && bottom < height)
                {
                    WavePossition bottomPossition = wave.wave[bottom, col];
                    currentEntropy = bottomPossition.entropy.ToList();
                    newEntropy = socketMapTile.fitBottom.ToList();
                    enumerable = currentEntropy.Intersect(newEntropy);
                    bottomPossition.entropy = enumerable.ToArray();
                }
            }
        }
    }
    
    private bool UnpropagateNonCollapsed()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                WavePossition possition = wave.wave[row, col];
                if(possition.entropy.Any())
                    continue;
                
                possition.entropy = _bitMap.ValidInitialTiles();
                
                int top = row - 1;
                int right = col + 1;
                int bottom = row + 1;
                int left = col - 1;
                
                if(left >= 0 && left < width)
                {
                    WavePossition leftPossition = wave.wave[row, left];
                    leftPossition.entropy = _bitMap.ValidInitialTiles();
                }
                if(right >= 0 && right < width)
                {
                    WavePossition rightPossition = wave.wave[row, right];
                    rightPossition.entropy = _bitMap.ValidInitialTiles();
                }
                if(top >= 0 && top < height)
                {
                    WavePossition topPossition = wave.wave[top, col];
                    topPossition.entropy = _bitMap.ValidInitialTiles();   
                }
                // ReSharper disable once InvertIf
                if(bottom >= 0 && bottom < height)
                {
                    WavePossition bottomPossition = wave.wave[bottom, col];
                    bottomPossition.entropy = _bitMap.ValidInitialTiles();
                }
                
                return true;
            }
        }
        
        return false;
    }
    
    public MapTile GetTileAtPossition(int rowIndex, int columnIndex) =>
        _bitMap.GetTileAtPossition(rowIndex, columnIndex);
    
    public void ResetMap() => _bitMap.Init();
}