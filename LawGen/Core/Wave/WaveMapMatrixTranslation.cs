﻿using LawGen.Core.Wave.Types;

namespace LawGen.Core.Wave;

public partial class Wave : IWaveMatrixCommons
{
    public bool HasOnlyConflicts =>
        waveMatrix.HasOnlyConflicts;
    public int CollapsedTilesCount =>
        waveMatrix.CollapsedTilesCount;
    public int ConflictsTilesCount =>
        waveMatrix.ConflictsTilesCount;
    
    public void UpdateEntropyAt(WavePossitionPoint possitionPoint, byte[] newEntropy) =>
        waveMatrix.UpdateEntropyAt(possitionPoint, newEntropy);
    public bool AllCollapsed() =>
        waveMatrix.AllCollapsed();
    public WavePossition GetPossitionAtPoint(WavePossitionPoint point) =>
        waveMatrix.wave[point.row, point.column];
    public WavePossitionPoint GetSmallerEntropyPossition(bool includeConflicts = false) =>
        waveMatrix.GetSmallerEntropyPossition(includeConflicts);
}