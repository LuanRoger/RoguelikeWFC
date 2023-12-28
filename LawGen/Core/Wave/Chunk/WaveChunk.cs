namespace LawGen.Core.Wave.Chunk;

public class WaveChunk
{
    public readonly uint Height;
    public readonly uint Width;
    public Wave Map { get; private set; }
    
    public WaveChunk(uint width, uint height)
    {
        Width = width;
        Height = height;
    }
}