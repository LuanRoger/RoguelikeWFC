using LawGen.Core.Extensions;
using LawGen.Core.Tiling;
using LawGen.Core.Wave;
using LawGen.Core.Wave.Chunk;
using LawGen.Core.Wave.Chunk.Types;
using LawGen.Core.Wave.Types;

namespace LawGen.ChunkBuilders.WFC;

public class WfcChunkCollapson(WfcChunkBuilderMetadata builderMetadata) : ChunkCollapsor(builderMetadata)
{
    private bool allCollapsed { get; set; } = false;
    private bool clean { get; set; }
    
    public WaveMatrix CollapseChunk()
    {
        CurrentStepState = ChunkGenerationStepState.WaveCollapse;
        
        while (!allCollapsed && !clean)
            InterateWfcOnce();
        
        CurrentStepState = ChunkGenerationStepState.Finished;

        return Metadata.DumpWave();
    }
    
    private void InterateWfcOnce()
    {
        WavePossitionPoint possitionPoint = Metadata.GetSmallerEntropyPossition();
        byte tileId = GetRandomTileFromPossition(possitionPoint);
        Metadata.UpdateEntropyAt(possitionPoint, [tileId]);

        CurrentStepState = ChunkGenerationStepState.Propagation;
        PropagateState();
        
        if(Metadata.HasOnlyConflicts || CurrentStepState == ChunkGenerationStepState.PosGenerationProcessing)
            PosMapGenerationClean();
    }
    
    public override void PostProcessing()
    {
        CurrentStepState = ChunkGenerationStepState.PosGenerationProcessing;
        
        bool noConflincts = true;
        bool noIsolation = true;
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                WavePossitionPoint possitionPoint = new(row, col);
                WavePossition possition = Metadata.GetPossitionAtPoint(possitionPoint);
                
                bool conflictUncollapsed = UncollapseConflictTiles(possitionPoint, possition);
                bool isolationFixed = ClearTileIsolation(possitionPoint);
                if(conflictUncollapsed)
                    noConflincts = false;
                if(isolationFixed)
                    noIsolation = false;
            }
        }
        
        CurrentStepState = ChunkGenerationStepState.Propagation;
        clean = noConflincts && noIsolation;
    }
    
    private void PropagateState()
    {
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                WavePossitionPoint possitionPoint = new(row, col);
                WavePossition possition = Metadata.GetPossitionAtPoint(possitionPoint);
                if(!possition.collapsed)
                    continue;
                
                TileSocket socketMapTile = Metadata.GetTileSocket(possition.Entropy[0]);

                WavePossitionArea possitionArea = new(possitionPoint);
                _ = Metadata.TileMatrix.CheckAreaOutOfBound(possitionArea, out bool top, out bool right, out bool bottom, out bool left);
                
                if(!top)
                {
                    WavePossition topPossition = Metadata.GetPossitionAtPoint(possitionArea.Top);
                    byte[] newEntropyTop = topPossition.Entropy
                        .IntersectItems(socketMapTile.fitTop);
                    Metadata.UpdateEntropyAt(possitionArea.Top, newEntropyTop);
                }
                if(!right)
                {
                    WavePossition rightPossition = Metadata.GetPossitionAtPoint(possitionArea.Right);
                    byte[] newEntropyRight = rightPossition.Entropy
                        .IntersectItems(socketMapTile.fitRight);
                    Metadata.UpdateEntropyAt(possitionArea.Right, newEntropyRight);
                }
                if(!bottom)
                {
                    WavePossition bottomPossition = Metadata.GetPossitionAtPoint(possitionArea.Bottom);
                    byte[] newEntropyBottom = bottomPossition.Entropy
                        .IntersectItems(socketMapTile.fitBottom);
                    Metadata.UpdateEntropyAt(possitionArea.Bottom, newEntropyBottom);
                }
                // ReSharper disable once InvertIf
                if(!left)
                {
                    WavePossition leftPossition = Metadata.GetPossitionAtPoint(possitionArea.Left);
                    byte[] newEntropyLeft = leftPossition.Entropy
                        .IntersectItems(socketMapTile.fitLeft);
                    Metadata.UpdateEntropyAt(possitionArea.Left, newEntropyLeft);
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
                
        _waveMap.UpdateEntropyAt(possitionPoint, Metadata.tilesIds);
                
        WavePossitionArea possitionArea = new(possitionPoint);
        _ = Metadata.TileMatrix.CheckAreaOutOfBound(possitionArea, out bool top, out bool right, out bool bottom, out bool left);
                
        if(!left)
            Metadata.UpdateEntropyAt(possitionArea.Left, Metadata.tilesIds);
        if(!right)
            Metadata.UpdateEntropyAt(possitionArea.Right, Metadata.tilesIds);
        if(!top)
            Metadata.UpdateEntropyAt(possitionArea.Top, Metadata.tilesIds);
        if(!bottom)
            Metadata.UpdateEntropyAt(possitionArea.Bottom, Metadata.tilesIds);
        
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
        bool isTileIsolation = IsTileIsolation(ref possitionPoint);
        if(isTileIsolation)
            return false;
        
        Metadata.UpdateEntropyAt(possitionPoint, Metadata.tilesIds);
        return true;
    }
    
    public byte GetRandomTileFromPossition(WavePossitionPoint possition)
    {
        Random rng = new();
        
        WavePossition wavePossition = Metadata.GetPossitionAtPoint(possition);
        
        //TODO: Tile frequency (weight)
        
        int tileIndex = rng.Next(0, wavePossition.Entropy.Length);
        return wavePossition.Entropy[tileIndex];
    }
    
    public bool IsTileIsolation(ref WavePossitionPoint tilePossition)
    {
        WavePossition possition = Metadata.GetPossitionAtPoint(tilePossition);
        MapTile tileAtPossition = Metadata.GetTileAtPossition(tilePossition.row, tilePossition.column);
        
        if(possition.conflict || !possition.collapsed || tileAtPossition.IsolationGroup is null)
            return true;

        WavePossitionArea possitionArea = new(tilePossition);
        byte[] allowedSideTilesByIsolationRule = tileAtPossition.IsolationGroup;
        
        try
        {
            WavePossition onTopPossition = Metadata.GetPossitionAtPoint(possitionArea.Top);
            byte topTileId = onTopPossition.Entropy[0];
            bool isAllowedTopTile = allowedSideTilesByIsolationRule.Contains(topTileId);
            
            if (onTopPossition.collapsed && isAllowedTopTile)
                return true;
        }
        catch (IndexOutOfRangeException) { /* ignored */ }

        try
        {
            WavePossition onRightPossition = Metadata.GetPossitionAtPoint(possitionArea.Right);
            byte rightTileId = onRightPossition.Entropy[0];
            bool isAllowedRightTile = allowedSideTilesByIsolationRule.Contains(rightTileId);
            
            if (onRightPossition.collapsed && isAllowedRightTile)
                return true;
        }
        catch (IndexOutOfRangeException) { /* ignored */ }

        try
        {
            WavePossition onBottomPossition = Metadata.GetPossitionAtPoint(possitionArea.Bottom);
            byte bottomTileId = onBottomPossition.Entropy[0];
            bool isAllowedBottomTile = allowedSideTilesByIsolationRule.Contains(bottomTileId);
            
            if (onBottomPossition.collapsed && isAllowedBottomTile)
                return true;
        }
        catch (IndexOutOfRangeException) { /* ignored */ }

        try
        {
            WavePossition onLeftPossition = Metadata.GetPossitionAtPoint(possitionArea.Left);
            byte leftTileId = onLeftPossition.Entropy[0];
            bool isAllowedLeftTile = allowedSideTilesByIsolationRule.Contains(leftTileId);
            
            if(onLeftPossition.collapsed && isAllowedLeftTile)
                return true;
        }
        catch (IndexOutOfRangeException) { /* ignored */ }

        return false;
    }
}