using LawGen.Core.Extensions;
using LawGen.Core.Tiling;
using LawGen.Core.Wave;
using LawGen.Core.Wave.Chunk.Types;
using LawGen.Core.Wave.Types;
using LawGen.Information;
using LawGen.WFC.Enum;

namespace LawGen.World;

public class WorldGenerator(int width, int height, TileAtlas tileAtlas)
{
    private Wave _wave = new(width, height, tileAtlas);
    private int width => _wave.Width;
    private int height => _wave.Height;
    private WorldMap? _worldMapIntance;
    private bool _updateMapInstance = true;
    private ChunkGenerationStepState _chunkGenerationStepStepState = ChunkGenerationStepState.Idle;

    private bool allCollapsed => _wave.AllCollapsed();
    private bool clean { get; set; }

    public ChunkGenerationStepState chunkGenerationStepState
    {
        get => _chunkGenerationStepStepState;
        private set
        {
            if(value <= _chunkGenerationStepStepState)
                return;
            _chunkGenerationStepStepState = value;
            OnGenerationStepStateChange?.Invoke(value);
        }
    }
    
    public delegate void OnGenerationStepStateChangeEventHandler(ChunkGenerationStepState state);
    public event OnGenerationStepStateChangeEventHandler? OnGenerationStepStateChange;

    public WorldMap? worldMap
    {
        get
        {
            if(_worldMapIntance is not null)
                return _worldMapIntance;
            
            if(!_wave.AllCollapsed() || !_updateMapInstance)
                return null;
        
            var mapTiles = new MapTile[height, width];
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    WavePossition wavePossition = _wave.GetPossitionAtPoint(new(row, col));
                    byte tileId = wavePossition.Entropy[0];
                    MapTile tile = _wave.GetTileById(tileId);
            
                    mapTiles[row, col] = tile;
                }
            }
        
            _worldMapIntance = new(width, height, mapTiles, _wave.AtalsId);
            _updateMapInstance = false;
            return _worldMapIntance;
        }
    }

    public void Wfc(WfcCallKind callKind = WfcCallKind.Interation)
    {
        chunkGenerationStepState = ChunkGenerationStepState.WaveCollapse;
        
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
            chunkGenerationStepState = ChunkGenerationStepState.Finished;
    }
    private void WfcComplete()
    {
        while (!allCollapsed && !clean)
            InterateWfcOnce();
    }
    private void InterateWfcOnce()
    {
        WavePossitionPoint possitionPoint = _wave.GetSmallerEntropyPossition();
        byte tileId = _wave.GetRandomTileFromPossition(possitionPoint);
        _wave.UpdateEntropyAt(possitionPoint, [tileId]);

        chunkGenerationStepState = ChunkGenerationStepState.Propagation;
        PropagateState();
        
        if(_wave.HasOnlyConflicts || _chunkGenerationStepStepState == ChunkGenerationStepState.PosGenerationProcessing)
            PosMapGenerationClean();
    }
    private void PosMapGenerationClean()
    {
        chunkGenerationStepState = ChunkGenerationStepState.PosGenerationProcessing;
        
        bool noConflincts = true;
        bool noIsolation = true;
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                WavePossitionPoint possitionPoint = new(row, col);
                WavePossition possition = _wave.GetPossitionAtPoint(possitionPoint);
                
                bool conflictUncollapsed = UncollapseConflictTiles(possitionPoint, possition);
                bool isolationFixed = ClearTileIsolation(possitionPoint);
                if(conflictUncollapsed)
                    noConflincts = false;
                if(isolationFixed)
                    noIsolation = false;
            }
        }
        
        _chunkGenerationStepStepState = ChunkGenerationStepState.Propagation;
        clean = noConflincts && noIsolation;
    }
    
    private void PropagateState()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                WavePossitionPoint possitionPoint = new(row, col);
                WavePossition possition = _wave.GetPossitionAtPoint(possitionPoint);
                if(!possition.collapsed)
                    continue;
                
                TileSocket socketMapTile = _wave.GetTileAtPossition(possitionPoint).TileSocket;

                WavePossitionArea possitionArea = new(possitionPoint);
                _ = _wave.CheckAreaOutOfBound(possitionArea, out bool top, out bool right, out bool bottom, out bool left);
                
                if(!top)
                {
                    WavePossition topPossition = _wave.GetPossitionAtPoint(possitionArea.Top);
                    byte[] newEntropyTop = topPossition.Entropy
                        .IntersectItems(socketMapTile.fitTop);
                    _wave.UpdateEntropyAt(possitionArea.Top, newEntropyTop);
                }
                if(!right)
                {
                    WavePossition rightPossition = _wave.GetPossitionAtPoint(possitionArea.Right);
                    byte[] newEntropyRight = rightPossition.Entropy
                        .IntersectItems(socketMapTile.fitRight);
                    _wave.UpdateEntropyAt(possitionArea.Right, newEntropyRight);
                }
                if(!bottom)
                {
                    WavePossition bottomPossition = _wave.GetPossitionAtPoint(possitionArea.Bottom);
                    byte[] newEntropyBottom = bottomPossition.Entropy
                        .IntersectItems(socketMapTile.fitBottom);
                    _wave.UpdateEntropyAt(possitionArea.Bottom, newEntropyBottom);
                }
                // ReSharper disable once InvertIf
                if(!left)
                {
                    WavePossition leftPossition = _wave.GetPossitionAtPoint(possitionArea.Left);
                    byte[] newEntropyLeft = leftPossition.Entropy
                        .IntersectItems(socketMapTile.fitLeft);
                    _wave.UpdateEntropyAt(possitionArea.Left, newEntropyLeft);
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
                
        _wave.UpdateEntropyAt(possitionPoint, _wave.ValidInitialTiles());
                
        WavePossitionArea possitionArea = new(possitionPoint);
        _ = _wave.CheckAreaOutOfBound(possitionArea, out bool top, out bool right, out bool bottom, out bool left);
                
        if(!left)
            _wave.UpdateEntropyAt(possitionArea.Left, _wave.ValidInitialTiles());
        if(!right)
            _wave.UpdateEntropyAt(possitionArea.Right, _wave.ValidInitialTiles());
        if(!top)
            _wave.UpdateEntropyAt(possitionArea.Top, _wave.ValidInitialTiles());
        if(!bottom)
            _wave.UpdateEntropyAt(possitionArea.Bottom, _wave.ValidInitialTiles());
        
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
        bool isTileIsolation = _wave.IsTileIsolation(ref possitionPoint);
        if(isTileIsolation)
            return false;
        
        _wave.UpdateEntropyAt(possitionPoint, _wave.ValidInitialTiles());
        return true;
    }

    public void ChangeAtlasInstance(TileAtlas newAtlas)
    {
        _wave = new(width, height, newAtlas);
        _worldMapIntance = null;
    }
    
    public MapTile GetTileAtPossition(int rowIndex, int columnIndex) =>
        _wave.GetTileAtPossition(rowIndex, columnIndex);
     
    public GenerationInformation DumpGenerationInformation() =>
        new()
        {
            collapsedTiles = _wave.CollapsedTilesCount,
            conflictTiles = _wave.ConflictsTilesCount,
            leftTilesToCollapse = _wave.WaveLength - _wave.CollapsedTilesCount,
            totalTiles = _wave.WaveLength,
            allCollapsed = _wave.AllCollapsed()
        };
    
    public void ResetMap()
    {
        _wave.Reset();
        _worldMapIntance = null;
        _updateMapInstance = true;
        clean = false;
        _chunkGenerationStepStepState = ChunkGenerationStepState.Idle;
    }
}