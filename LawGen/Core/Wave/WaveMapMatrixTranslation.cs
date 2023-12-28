using LawGen.Core.Wave.Types;

namespace LawGen.Core.Wave;

public partial class WaveMap : IWaveMatrixMapCommons
{
    public bool HasOnlyConflicts =>
        waveMatrix.HasOnlyConflicts;
    public int CollapsedTilesCount =>
        waveMatrix.CollapsedTilesCount;
    
    public void UpdateEntropyAt(WavePossitionPoint possitionPoint, byte[] newEntropy) =>
        waveMatrix.UpdateEntropyAt(possitionPoint, newEntropy);
    public bool AllCollapsed() =>
        waveMatrix.AllCollapsed();
    public WavePossition GetPossitionAtPoint(WavePossitionPoint point) =>
        waveMatrix.wave[point.row, point.column];
    public WavePossitionPoint GetSmallerEntropyPossition(bool includeConflicts = false) =>
        waveMatrix.GetSmallerEntropyPossition(includeConflicts);
}