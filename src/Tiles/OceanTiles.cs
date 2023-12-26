using LawGen.Core.Tiling;
using RoguelikeWFC.Tiles.Sprites;

namespace RoguelikeWFC.Tiles;

public record OceanCoastGrass() : RenderableMapTile(TileIDs.OCEAN_COAST_GRASS, 249,
    new([ TileIDs.OCEAN_COAST_GRASS, TileIDs.OCEAN_COATS_SAND ]))
{
    public override TileSpriteMetadata SpriteMetadata { get; } = 
        new(Color.AnsiGreen, Color.Yellow);
}

public record OceanCoastSand() : RenderableMapTile(TileIDs.OCEAN_COATS_SAND, 178,
    new([ TileIDs.OCEAN_COATS_SAND, TileIDs.OCEAN_COAST_GRASS, TileIDs.WATER ]),
    isolationGroup: [ TileIDs.OCEAN_COATS_SAND ])
{
    public override TileSpriteMetadata SpriteMetadata { get; } =
        new(Color.Yellow);
}

public record WatterTile() : RenderableMapTile(TileIDs.WATER, 247,
    new([ TileIDs.WATER, TileIDs.OCEAN, TileIDs.OCEAN_COATS_SAND ]),
    isolationGroup: [ TileIDs.WATER, TileIDs.OCEAN, TileIDs.DEEP_OCEAN ])
{
    public override TileSpriteMetadata SpriteMetadata { get; } = 
        new(Color.AnsiCyan, Color.Aqua);
}

public record OceanTile() : RenderableMapTile(TileIDs.OCEAN, 247,
    new([ TileIDs.OCEAN, TileIDs.DEEP_OCEAN, TileIDs.WATER ]))
{
    public override TileSpriteMetadata SpriteMetadata { get; } = 
        new(Color.AnsiBlue, Color.Aqua);
}

public record DeepOceanTile() : RenderableMapTile(TileIDs.DEEP_OCEAN, 247,
    new([ TileIDs.DEEP_OCEAN, TileIDs.OCEAN ]))
{
    public override TileSpriteMetadata SpriteMetadata { get; } = 
        new(Color.DarkBlue, Color.Aqua);
}

public class OceanAtlas() : TileAtlas([
    new OceanCoastGrass(),
    new OceanCoastSand(),
    new WatterTile(),
    new OceanTile(),
    new DeepOceanTile()
], AtlasIDs.OCEAN)
{
    private static OceanAtlas? _instance;

    public static OceanAtlas Instance => _instance ??= new();
}