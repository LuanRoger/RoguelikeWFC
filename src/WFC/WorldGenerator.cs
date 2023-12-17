﻿using RoguelikeWFC.Components.Models;
using RoguelikeWFC.Extensions;
using RoguelikeWFC.Tiles;
using RoguelikeWFC.WFC.Enum;

namespace RoguelikeWFC.WFC;

public class WorldGenerator(int width, int height, TileAtlas tileAtlas)
{
    private WaveMap _waveMap = new(width, height, tileAtlas);
    private int width => _waveMap.width;
    private int height => _waveMap.height;
    private WorldMap? _worldMapIntance;
    private bool _updateMapInstance = true;
    private GenerationStepState _generationStepState = GenerationStepState.Idle;

    private bool allCollapsed => _waveMap.AllCollapsed();
    private bool clean => _waveMap.HasOnlyConflicts() && !_waveMap.HasTileIsolation();

    public GenerationStepState generationState
    {
        get => _generationStepState;
        private set
        {
            if(value <= _generationStepState)
                return;
            _generationStepState = value;
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
                    int tileId = wavePossition.Entropy[0];
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
        generationState = GenerationStepState.WaveCollapse;
        
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
            generationState = GenerationStepState.Finished;
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
        _waveMap.UpdateEntropyAt(possitionPoint, new[] { tileId });

        generationState = GenerationStepState.Propagation;
        PropagateState();
        
        if(_waveMap.HasOnlyConflicts() || _generationStepState == GenerationStepState.PosGenerationProcessing)
            PosMapGenerationClean();
    }
    private void PosMapGenerationClean()
    {
        generationState = GenerationStepState.PosGenerationProcessing;
        
        if (_waveMap.HasOnlyConflicts())
            UnpropagateNonCollapsed();
        
        if(_waveMap.HasTileIsolation())
            ClearTileIsolation();
        
        _generationStepState = GenerationStepState.Propagation;
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
                
                int topRaw = row - 1;
                int rightRaw = col + 1;
                int bottomRaw = row + 1;
                int leftRaw = col - 1;

                WavePossitionArea possitionArea = new(possitionPoint);
                
                if(leftRaw >= 0 && leftRaw < width)
                {
                    WavePossition leftPossition = _waveMap.GetPossitionAtPoint(possitionArea.Left);
                    byte[] newEntropyLeft = leftPossition.Entropy
                        .Intersect(socketMapTile.fitLeft);
                    _waveMap.UpdateEntropyAt(possitionArea.Left, newEntropyLeft);
                }

                if(rightRaw >= 0 && rightRaw < width)
                {
                    WavePossition rightPossition = _waveMap.GetPossitionAtPoint(possitionArea.Right);
                    byte[] newEntropyRight = rightPossition.Entropy
                        .Intersect(socketMapTile.fitRight);
                    _waveMap.UpdateEntropyAt(possitionArea.Right, newEntropyRight);
                }

                if(topRaw >= 0 && topRaw < height)
                {
                    WavePossition topPossition = _waveMap.GetPossitionAtPoint(possitionArea.Top);
                    byte[] newEntropyTop = topPossition.Entropy
                        .Intersect(socketMapTile.fitTop);
                    _waveMap.UpdateEntropyAt(possitionArea.Top, newEntropyTop);
                }

                // ReSharper disable once InvertIf
                if(bottomRaw >= 0 && bottomRaw < height)
                {
                    WavePossition bottomPossition = _waveMap.GetPossitionAtPoint(possitionArea.Bottom);
                    byte[] newEntropyBottom = bottomPossition.Entropy
                        .Intersect(socketMapTile.fitBottom);
                    _waveMap.UpdateEntropyAt(possitionArea.Bottom, newEntropyBottom);
                }
            }
        }
    }
    
    private void UnpropagateNonCollapsed()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                WavePossitionPoint possitionPoint = new(row, col);
                WavePossition possition = _waveMap.GetPossitionAtPoint(possitionPoint);
                if(!possition.conflict)
                    continue;
                
                _waveMap.UpdateEntropyAt(possitionPoint, _waveMap.ValidInitialTiles());
                
                //TODO: Move to WaveMap and create method to check if the bounds are valid
                int topRaw = row - 1;
                int rightRaw = col + 1;
                int bottomRaw = row + 1;
                int leftRaw = col - 1;

                WavePossitionArea possitionArea = new(possitionPoint);
                
                if(leftRaw >= 0 && leftRaw < width)
                {
                    _waveMap.UpdateEntropyAt(possitionArea.Left, _waveMap.ValidInitialTiles());
                }
                if(rightRaw >= 0 && rightRaw < width)
                {
                    _waveMap.UpdateEntropyAt(possitionArea.Right, _waveMap.ValidInitialTiles());
                }
                if(topRaw >= 0 && topRaw < height)
                {
                    _waveMap.UpdateEntropyAt(possitionArea.Top, _waveMap.ValidInitialTiles());
                }
                // ReSharper disable once InvertIf
                if(bottomRaw >= 0 && bottomRaw < height)
                {
                    _waveMap.UpdateEntropyAt(possitionArea.Bottom, _waveMap.ValidInitialTiles());
                }
            }
        }
    }

    private void ClearTileIsolation()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                WavePossitionPoint possitionPoint = new(row, col);
                
                bool isTileIsolation = _waveMap.IsTileIsolation(ref possitionPoint);
                
                if(isTileIsolation)
                    continue;
                
                _waveMap.UpdateEntropyAt(possitionPoint, _waveMap.ValidInitialTiles());
            }
        }
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
            collapsedTiles = _waveMap.GetCountOfCollapsedTiles(),
            conflictTiles = _waveMap.GetCountOfConflicts(),
            leftTilesToCollapse = _waveMap.waveLength - _waveMap.GetCountOfCollapsedTiles(),
            totalTiles = _waveMap.waveLength,
            allCollapsed = _waveMap.AllCollapsed()
        };
    
    public void ResetMap()
    {
        _waveMap.Reset();
        _worldMapIntance = null;
        _updateMapInstance = true;
    }
}