namespace LawGen.Core.Tiling;

public readonly struct CollapseFrequency(byte tileId, float frequency)
{
    public readonly byte TileId = tileId;
    public readonly float Frequency = frequency;
}