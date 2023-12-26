using LawGen.Core.Tiling;

namespace RoguelikeWFC.Tiles;

public record OceanCoastGrass() : MapTile(TileIDs.OCEAN_COAST_GRASS, 249,
    new([ TileIDs.OCEAN_COAST_GRASS, TileIDs.OCEAN_COATS_SAND ]))
{
    public readonly TileSpriteMetadata SpriteMetadata = 
        new(Color.AnsiGreen, Color.Yellow);
}

public record OceanCoastSand() : MapTile(TileIDs.OCEAN_COATS_SAND, 178,
    new([ TileIDs.OCEAN_COATS_SAND, TileIDs.OCEAN_COAST_GRASS, TileIDs.WATER ]),
    isolationGroup: [ TileIDs.OCEAN_COATS_SAND ])
{
    public readonly TileSpriteMetadata SpriteMetadata = 
        new(Color.Yellow);
}

public record WatterTile() : MapTile(TileIDs.WATER, 247,
    new([ TileIDs.WATER, TileIDs.OCEAN, TileIDs.OCEAN_COATS_SAND ]),
    isolationGroup: [ TileIDs.WATER, TileIDs.OCEAN, TileIDs.DEEP_OCEAN ])
{
    public readonly TileSpriteMetadata SpriteMetadata = 
        new(Color.AnsiCyan, Color.Aqua);
}

public record OceanTile() : MapTile(TileIDs.OCEAN, 247,
    new([ TileIDs.OCEAN, TileIDs.DEEP_OCEAN, TileIDs.WATER ]))
{
    public readonly TileSpriteMetadata SpriteMetadata = 
        new(Color.AnsiBlue, Color.Aqua);
}

public record DeepOceanTile() : MapTile(TileIDs.DEEP_OCEAN, 247,
    new([ TileIDs.DEEP_OCEAN, TileIDs.OCEAN ]))
{
    public readonly TileSpriteMetadata SpriteMetadata = 
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