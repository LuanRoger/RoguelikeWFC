using LawGen.Core.Extensions;
using LawGen.Core.Tiling;
using LawGen.Core.Wave;
using LawGen.Core.Wave.Types;
using LawGen.Information;
using LawGen.WFC.Enum;

namespace LawGen.World;

public class WorldGenerator(int width, int height, TileAtlas tileAtlas)
{
    private WaveMap _waveMap = new(width, height, tileAtlas);
    private int width => _waveMap.Width;
    private int height => _waveMap.Height;
    private WorldMap? _worldMapIntance;
    private bool _updateMapInstance = true;
    private GenerationStepState _generationStepStepState = GenerationStepState.Idle;

    private bool allCollapsed => _waveMap.AllCollapsed();
    private bool clean { get; set; }

    public GenerationStepState generationStepState
    {
        get => _generationStepStepState;
        private set
        {
            if(value <= _generationStepStepState)
                return;
            _generationStepStepState = value;
            OnGenerationStepStateChange?.Invoke(value);
        }
    }
    
    public delegate void OnGenerationStepStateChangeEventHandler(GenerationStepState state);
    public event OnGenerationStepStateChangeEventHandler? OnGenerationStepStateChange;

    public WorldMap? worldMap
    {
        get
        {
            if(_worldMapIntance is not null)
                return _worldMapIntance;
            
            if(!_waveMap.AllCollapsed() || !_updateMapInstance)
                return null;
        
            var mapTiles = new MapTile[height, width];
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    WavePossition wavePossition = _waveMap.GetPossitionAtPoint(new(row, col));
                    byte tileId = wavePossition.Entropy[0];
                    MapTile tile = _waveMap.GetTileById(tileId);
            
                    mapTiles[row, col] = tile;
                }
            }
        
            _worldMapIntance = new(width, height, mapTiles, _waveMap.AtalsId);
            _updateMapInstance = false;
            return _worldMapIntance;
        }
    }

    public void Wfc(WfcCallKind callKind = WfcCallKind.Interation)
    {
        generationStepState = GenerationStepState.WaveCollapse;
        
        switch (callKind)
        {
            case WfcCallKind.Complete:
                WfcComplete();
                break;
            case WfcCallKind.Interation:
                InterateWfcOnce();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(callKind), callKind, null);
        }
        
        if(allCollapsed && clean)
            generationStepState = GenerationStepState.Finished;
    }
    private void WfcComplete()
    {
        while (!allCollapsed && !clean)
            InterateWfcOnce();
    }
    private void InterateWfcOnce()
    {
        WavePossitionPoint possitionPoint = _waveMap.GetSmallerEntropyPossition();
        byte tileId = _waveMap.GetRandomTileFromPossition(possitionPoint);
        _waveMap.UpdateEntropyAt(possitionPoint, [tileId]);

        generationStepState = GenerationStepState.Propagation;
        PropagateState();
        
        if(_waveMap.HasOnlyConflicts || _generationStepStepState == GenerationStepState.PosGenerationProcessing)
            PosMapGenerationClean();
    }
    private void PosMapGenerationClean()
    {
        generationStepState = GenerationStepState.PosGenerationProcessing;
        
        bool noConflincts = true;
        bool noIsolation = true;
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                WavePossitionPoint possitionPoint = new(row, col);
                WavePossition possition = _waveMap.GetPossitionAtPoint(possitionPoint);
                
                bool conflictUncollapsed = UncollapseConflictTiles(possitionPoint, possition);
                bool isolationFixed = ClearTileIsolation(possitionPoint);
                if(conflictUncollapsed)
                    noConflincts = false;
                if(isolationFixed)
                    noIsolation = false;
            }
        }
        
        _generationStepStepState = GenerationStepState.Propagation;
        clean = noConflincts && noIsolation;
    }
    
    private void PropagateState()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                WavePossitionPoint possitionPoint = new(row, col);
                WavePossition possition = _waveMap.GetPossitionAtPoint(possitionPoint);
                if(!possition.collapsed)
                    continue;
                
                TileSocket socketMapTile = _waveMap.GetTileAtPossition(possitionPoint).TileSocket;

                WavePossitionArea possitionArea = new(possitionPoint);
                _ = _waveMap.CheckAreaOutOfBound(possitionArea, out bool top, out bool right, out bool bottom, out bool left);
                
                if(!top)
                {
                    WavePossition topPossition = _waveMap.GetPossitionAtPoint(possitionArea.Top);
                    byte[] newEntropyTop = topPossition.Entropy
                        .IntersectItems(socketMapTile.fitTop);
                    _waveMap.UpdateEntropyAt(possitionArea.Top, newEntropyTop);
                }
                if(!right)
                {
                    WavePossition rightPossition = _waveMap.GetPossitionAtPoint(possitionArea.Right);
                    byte[] newEntropyRight = rightPossition.Entropy
                        .IntersectItems(socketMapTile.fitRight);
                    _waveMap.UpdateEntropyAt(possitionArea.Right, newEntropyRight);
                }
                if(!bottom)
                {
                    WavePossition bottomPossition = _waveMap.GetPossitionAtPoint(possitionArea.Bottom);
                    byte[] newEntropyBottom = bottomPossition.Entropy
                        .IntersectItems(socketMapTile.fitBottom);
                    _waveMap.UpdateEntropyAt(possitionArea.Bottom, newEntropyBottom);
                }
                // ReSharper disable once InvertIf
                if(!left)
                {
                    WavePossition leftPossition = _waveMap.GetPossitionAtPoint(possitionArea.Left);
                    byte[] newEntropyLeft = leftPossition.Entropy
                        .IntersectItems(socketMapTile.fitLeft);
                    _waveMap.UpdateEntropyAt(possitionArea.Left, newEntropyLeft);
                }
            }
        }
    }
    
    /// <summary>
    /// Unpropagate the conflict tile and update the entropy of the tiles around it.
    /// </summary>
    /// <param name="possitionPoint">Tile matrix possition</param>
    /// <param name="possition">Tile information</param>
    /// <returns><c>true</c> if the tile are in conflict and it was updated, otherwise, <c>false</c></returns>
    private bool UncollapseConflictTiles(WavePossitionPoint possitionPoint, WavePossition possition)
    {
        if(!possition.conflict)
            return false;
                
        _waveMap.UpdateEntropyAt(possitionPoint, _waveMap.ValidInitialTiles());
                
        WavePossitionArea possitionArea = new(possitionPoint);
        _ = _waveMap.CheckAreaOutOfBound(possitionArea, out bool top, out bool right, out bool bottom, out bool left);
                
        if(!left)
            _waveMap.UpdateEntropyAt(possitionArea.Left, _waveMap.ValidInitialTiles());
        if(!right)
            _waveMap.UpdateEntropyAt(possitionArea.Right, _waveMap.ValidInitialTiles());
        if(!top)
            _waveMap.UpdateEntropyAt(possitionArea.Top, _waveMap.ValidInitialTiles());
        if(!bottom)
            _waveMap.UpdateEntropyAt(possitionArea.Bottom, _waveMap.ValidInitialTiles());
        
        return true;
    }
    
    /// <summary>
    /// Uncollapse the non isolated tile.
    /// </summary>
    /// <param name="possitionPoint">The tile possition</param>
    /// <returns><c>true</c> if the tile is not isolated and the entropy was updated,
    /// otherwise, <c>false</c></returns>
    private bool ClearTileIsolation(WavePossitionPoint possitionPoint)
    {
        bool isTileIsolation = _waveMap.IsTileIsolation(ref possitionPoint);
        if(isTileIsolation)
            return false;
        
        _waveMap.UpdateEntropyAt(possitionPoint, _waveMap.ValidInitialTiles());
        return true;
    }

    public void ChangeAtlasInstance(TileAtlas newAtlas)
    {
        _waveMap = new(width, height, newAtlas);
        _worldMapIntance = null;
    }
    
    public MapTile GetTileAtPossition(int rowIndex, int columnIndex) =>
        _waveMap.GetTileAtPossition(rowIndex, columnIndex);
     
    public GenerationInformation DumpGenerationInformation() =>
        new()
        {
            collapsedTiles = _waveMap.CollapsedTilesCount,
            conflictTiles = _waveMap.CollapsedTilesCount,
            leftTilesToCollapse = _waveMap.WaveLength - _waveMap.CollapsedTilesCount,
            totalTiles = _waveMap.WaveLength,
            allCollapsed = _waveMap.AllCollapsed()
        };
    
    public void ResetMap()
    {
        _waveMap.Reset();
        _worldMapIntance = null;
        _updateMapInstance = true;
        clean = false;
        _generationStepStepState = GenerationStepState.Idle;
    }
}