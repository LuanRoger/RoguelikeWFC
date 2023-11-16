using RoguelikeWFC.Extensions;
using RoguelikeWFC.Tiles;

namespace RoguelikeWFC.WFC;

public class WorldGenerator
{
    private readonly WaveMap _waveMap;
    private int width => _waveMap.width;
    private int height => _waveMap.height;
    private WorldMap? _worldMapIntance;
    private bool _updateMapInstance = true;
    
    public bool allCollapsed => _waveMap.AllCollapsed();

    public WorldMap? worldMap
    {
        get
        {
            if(!_waveMap.AllCollapsed() || !_updateMapInstance)
                return null;
        
            if(_worldMapIntance is not null)
                return _worldMapIntance;
        
            var mapTiles = new MapTile[height, width];
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    WavePossition wavePossition = _waveMap.GetPossitionAt(new(row, col));
                    int tileId = wavePossition.Entropy[0];
                    MapTile tile = _waveMap.GetTileById(tileId);
            
                    mapTiles[row, col] = tile;
                }
            }
        
            _worldMapIntance = new(width, height, mapTiles);
            _updateMapInstance = false;
            return _worldMapIntance;
        }
    }
    
    public WorldGenerator(int width, int height, TileAtlas tileAtlas)
    {
        _waveMap = new(width, height, tileAtlas);
    }
    
    public void Wfc()
    {
        while (!_waveMap.AllCollapsed())
            InterateWfcOnce();
    }
    public void InterateWfcOnce()
    {
        if(_waveMap.HasOnlyConflicts())
        {
            do
            {
                PropagateState();
            } while (UnpropagateNonCollapsed());
        }
        
        WavePossitionPoint possitionPoint = _waveMap.GetSmallerEntropyPossition();
        byte tileId = _waveMap.GetRandomTileFromPossition(possitionPoint);
        _waveMap.UpdateEntropyAt(possitionPoint, new[] { tileId });

        PropagateState();
    }
    
    private void PropagateState()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                WavePossitionPoint possitionPoint = new(row, col);
                WavePossition possition = _waveMap.GetPossitionAt(possitionPoint);
                if(!possition.collapsed)
                    continue;
                
                byte tileId = possition.Entropy[0];
                TileSocket socketMapTile = _waveMap.GetTileById(tileId).tileSocket;
                
                int topRaw = row - 1;
                int rightRaw = col + 1;
                int bottomRaw = row + 1;
                int leftRaw = col - 1;
                
                WavePossitionPoint top = new(topRaw, col);
                WavePossitionPoint right = new(row, rightRaw);
                WavePossitionPoint bottom = new(bottomRaw, col);
                WavePossitionPoint left = new(row, leftRaw);
                
                if(leftRaw >= 0 && leftRaw < width)
                {
                    WavePossition leftPossition = _waveMap.GetPossitionAt(left);
                    byte[] newEntropyLeft = leftPossition.Entropy
                        .Intersect(socketMapTile.fitLeft);
                    _waveMap.UpdateEntropyAt(left, newEntropyLeft);
                }

                if(rightRaw >= 0 && rightRaw < width)
                {
                    WavePossition rightPossition = _waveMap.GetPossitionAt(right);
                    byte[] newEntropyRight = rightPossition.Entropy
                        .Intersect(socketMapTile.fitRight);
                    _waveMap.UpdateEntropyAt(right, newEntropyRight);
                }

                if(topRaw >= 0 && topRaw < height)
                {
                    WavePossition topPossition = _waveMap.GetPossitionAt(top);
                    byte[] newEntropyTop = topPossition.Entropy
                        .Intersect(socketMapTile.fitTop);
                    _waveMap.UpdateEntropyAt(top, newEntropyTop);
                }

                // ReSharper disable once InvertIf
                if(bottomRaw >= 0 && bottomRaw < height)
                {
                    WavePossition bottomPossition = _waveMap.GetPossitionAt(bottom);
                    byte[] newEntropyBottom = bottomPossition.Entropy
                        .Intersect(socketMapTile.fitBottom);
                    _waveMap.UpdateEntropyAt(bottom, newEntropyBottom);
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
                WavePossitionPoint possitionPoint = new(row, col);
                WavePossition possition = _waveMap.GetPossitionAt(possitionPoint);
                if(!possition.hasConflict)
                    continue;
                
                _waveMap.UpdateEntropyAt(possitionPoint, _waveMap.ValidInitialTiles());
                
                int topRaw = row - 1;
                int rightRaw = col + 1;
                int bottomRaw = row + 1;
                int leftRaw = col - 1;
                
                WavePossitionPoint top = new(row - 1, col);
                WavePossitionPoint right = new(row, col + 1);
                WavePossitionPoint bottom = new(row + 1, col);
                WavePossitionPoint left = new(row, col - 1);
                
                if(leftRaw >= 0 && leftRaw < width)
                {
                    _waveMap.UpdateEntropyAt(left, _waveMap.ValidInitialTiles());
                }
                if(rightRaw >= 0 && rightRaw < width)
                {
                    _waveMap.UpdateEntropyAt(right, _waveMap.ValidInitialTiles());
                }
                if(topRaw >= 0 && topRaw < height)
                {
                    _waveMap.UpdateEntropyAt(top, _waveMap.ValidInitialTiles());
                }
                // ReSharper disable once InvertIf
                if(bottomRaw >= 0 && bottomRaw < height)
                {
                    _waveMap.UpdateEntropyAt(bottom, _waveMap.ValidInitialTiles());
                }
                
                return true;
            }
        }
        
        return false;
    }
    
    public MapTile GetTileAtPossition(int rowIndex, int columnIndex) =>
        _waveMap.GetTileAtPossition(rowIndex, columnIndex);
    
    public void ResetMap()
    {
        _waveMap.Reset();
        _updateMapInstance = true;
    }
}