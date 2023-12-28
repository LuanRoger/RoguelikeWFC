namespace LawGen.Core.Wave.Types;

internal interface IWaveMatrixCommons
{
    internal bool HasOnlyConflicts { get; }
    internal int CollapsedTilesCount { get; }
    internal int ConflictsTilesCount { get; }
    internal void UpdateEntropyAt(WavePossitionPoint possitionPoint, byte[] newEntropy);
    internal bool AllCollapsed();
    internal WavePossition GetPossitionAtPoint(WavePossitionPoint point);
    internal WavePossitionPoint GetSmallerEntropyPossition(bool includeConflicts = false);
}