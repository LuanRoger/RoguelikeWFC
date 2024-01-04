namespace LawGen.Core.Wave.Chunk.Types;

public abstract class ChunkCollapsor(uint height, uint width, WfcChunkBuilderMetadata metadata)
{
    protected readonly uint Height = height;
    protected readonly uint Width = width;
    protected WfcChunkBuilderMetadata Metadata = metadata;
    public ChunkGenerationStepState CurrentStepState { get; protected set; } = ChunkGenerationStepState.Idle;

    public abstract WaveMatrix CollapseChunk();
    public abstract void PostProcessing();
}