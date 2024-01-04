using LawGen.Core.Tiling;

namespace LawGen.Core.Wave.Chunk.Types;

public readonly struct ChunkTilesPrototype(byte id, TileSocket socket)
{
    public readonly byte Id = id;
    public readonly TileSocket Socket = socket;
}