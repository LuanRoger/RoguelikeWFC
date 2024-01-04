using LawGen.ChunkBuilders;
using LawGen.ChunkBuilders.WFC;
using LawGen.Core.Tiling;
using LawGen.Core.Wave.Chunk.Types;
using LawGen.Core.Wave.Types;

namespace LawGen.Core.Wave.Chunk;

public class ChunkBuilder
{
    public required uint Height { get; init; }
    public required uint Width { get; init; }
    public readonly ChunkCollapsor Collapsor;
    private TileAtlas _chunkAtlas;

    public ChunkBuilder(TileAtlas chunkAtlas, ChunkBuildMethod buildMethod)
    {
        _chunkAtlas = chunkAtlas;
        Collapsor = CreateCollapsorFromMethod(buildMethod);
    }
    
    public WaveChunk Build()
    {
        Collapsor.CollapseChunk();
    }

    private ChunkCollapsor CreateCollapsorFromMethod(ChunkBuildMethod method)
    {
        return method switch
        {
            ChunkBuildMethod.Wfc => new WfcChunkCollapson(Metadata),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
    }

    private WfcChunkBuilderMetadata CreateBuilderMetadaFromAtlas()
    {
        WaveMatrix waveMatrix = new(Width, Height, InitializeWavePossitions());
        var prototypes = CreateChunkTilesPrototype();

        return new(waveMatrix, prototypes);
    }

    private ChunkTilesPrototype[] CreateChunkTilesPrototype()
    {
        var prototypes = new ChunkTilesPrototype[_chunkAtlas.Tiles.Length];
        int prototypesIndex = 0;
        
        foreach (MapTile mapTile in _chunkAtlas.Tiles)
        {
            prototypes[prototypesIndex] = new(mapTile.Id, mapTile.TileSocket);
            prototypesIndex++;
        }

        return prototypes;
    }
    private WavePossition[,] InitializeWavePossitions()
    {
        var wavePossitions = new WavePossition[Height, Width];
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
                wavePossitions[row, col] = new(_chunkAtlas.ValidInitialTiles());
        }
        
        return wavePossitions;
    }
}