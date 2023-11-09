using RoguelikeWFC.Tiles;

namespace RoguelikeWFC.WFC;

public class WorldGenerator
{
    private readonly WaveMap _waveMap;
    private Wave wave => _waveMap.wave;
    private int width => _waveMap.width;
    private int height => _waveMap.height;
    
    private WorldMap? _worldMapIntance;
    public bool updateMapInstance { get; set; } = true;

    public WorldMap? worldMap
    {
        get
        {
            if(!wave.AllCollapsed() || !updateMapInstance)
                return null;
        
            if(_worldMapIntance is not null)
                return _worldMapIntance;
        
            var mapTiles = new MapTile[height, width];
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    WavePossition wavePossition = wave.wave[row, col];
                    int tileId = wavePossition.entropy[0];
                    MapTile tile = _waveMap.GetTileById(tileId);
            
                    mapTiles[row, col] = tile;
                }
            }
        
            _worldMapIntance = new(width, height, mapTiles);
            return _worldMapIntance;
        }
    }

    public WorldGenerator(WaveMap waveMap)
    {
        _waveMap = waveMap;
        if(!_waveMap.initialized)
            _waveMap.Init();
    }
    
    public WorldGenerator(int width, int height, TileSet tileSet)
    {
        _waveMap = new(width, height, tileSet);
        if(!_waveMap.initialized)
            _waveMap.Init();
    }
    
    public void Wfc()
    {
        if(!_waveMap.initialized)
            return;
        
        while (!wave.AllCollapsed())
            InterateWfcOnce();
    }
    public void InterateWfcOnce()
    {
        if(!_waveMap.initialized)
            return;
        
        WavePossition possition = _waveMap.GetSmallerEntropyPossition();
        byte tileId = _waveMap.GetRandomTileFromPossition(possition);
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
                TileSocket socketMapTile = _waveMap.GetTileById(tileId)!.tileSocket;
                
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
                
                possition.entropy = _waveMap.ValidInitialTiles();
                
                int top = row - 1;
                int right = col + 1;
                int bottom = row + 1;
                int left = col - 1;
                
                if(left >= 0 && left < width)
                {
                    WavePossition leftPossition = wave.wave[row, left];
                    leftPossition.entropy = _waveMap.ValidInitialTiles();
                }
                if(right >= 0 && right < width)
                {
                    WavePossition rightPossition = wave.wave[row, right];
                    rightPossition.entropy = _waveMap.ValidInitialTiles();
                }
                if(top >= 0 && top < height)
                {
                    WavePossition topPossition = wave.wave[top, col];
                    topPossition.entropy = _waveMap.ValidInitialTiles();   
                }
                // ReSharper disable once InvertIf
                if(bottom >= 0 && bottom < height)
                {
                    WavePossition bottomPossition = wave.wave[bottom, col];
                    bottomPossition.entropy = _waveMap.ValidInitialTiles();
                }
                
                return true;
            }
        }
        
        return false;
    }
    
    public MapTile GetTileAtPossition(int rowIndex, int columnIndex) =>
        _waveMap.GetTileAtPossition(rowIndex, columnIndex);
    public bool AllCollapsed() => 
        wave.AllCollapsed();
    
    public void ResetMap() => _waveMap.Init();
}