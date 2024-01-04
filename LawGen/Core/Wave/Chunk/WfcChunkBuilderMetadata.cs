
using LawGen.Core.Tiling;
using LawGen.Core.Wave.Chunk.Types;
using LawGen.Core.Wave.Types;

namespace LawGen.Core.Wave.Chunk;

public struct WfcChunkBuilderMetadata(WaveMatrix tileMatrix, ChunkTilesPrototype[] tiles) : IWaveMatrixCommons
{
    public bool Ready = false;
    private readonly ChunkTilesPrototype[] Tiles = tiles;
    public byte[] tilesIds => Tiles.Select(t => t.Id)
        .ToArray();
    public readonly WaveMatrix TileMatrix = tileMatrix;
    
    public bool HasOnlyConflicts =>
        TileMatrix.HasOnlyConflicts;
    public int CollapsedTilesCount =>
        TileMatrix.CollapsedTilesCount;
    public int ConflictsTilesCount =>
        TileMatrix.ConflictsTilesCount;

    public void UpdateEntropyAt(WavePossitionPoint possitionPoint, byte[] newEntropy) =>
        TileMatrix.UpdateEntropyAt(possitionPoint, newEntropy);
    public bool AllCollapsed() =>
        TileMatrix.AllCollapsed();
    public WavePossition GetPossitionAtPoint(WavePossitionPoint point) =>
        TileMatrix.wave[point.row, point.column];
    public WavePossitionPoint GetSmallerEntropyPossition(bool includeConflicts = false) =>
        TileMatrix.GetSmallerEntropyPossition(includeConflicts);

    public TileSocket GetTileSocket(byte id) =>
        Tiles.First(t => t.Id == id)
            .Socket;
}