using LawGen.Core.Tiling;
using RoguelikeWFC.Tiles.Sprites;

namespace RoguelikeWFC.Tiles;

public record GrassTile() : RenderableMapTile(TileIDs.GRASS, [249, 250],
    new([TileIDs.GRASS, TileIDs.TREE, TileIDs.MOUNTAIN, TileIDs.SAND]))
{
    public override TileSpriteMetadata SpriteMetadata { get; } = new(Color.Green);
}

public record TreeTile() : RenderableMapTile(TileIDs.TREE, [5, 231],
    new([TileIDs.TREE, TileIDs.GRASS, TileIDs.DIRT]))
{
    public override TileSpriteMetadata SpriteMetadata { get; } = new(Color.Brown);
}

public record MountainTile() : RenderableMapTile(TileIDs.MOUNTAIN, 30,
    new([ TileIDs.MOUNTAIN, TileIDs.MOUNTAIN_PEEK, TileIDs.GRASS, TileIDs.SAND ]),
    isolationGroup: [ TileIDs.MOUNTAIN, TileIDs.MOUNTAIN_PEEK ])
{
    public override TileSpriteMetadata SpriteMetadata { get; } = new(Color.Gray);
}

public record MountainPeekTile() : RenderableMapTile(TileIDs.MOUNTAIN_PEEK, 30,
    new([ TileIDs.MOUNTAIN_PEEK, TileIDs.MOUNTAIN ]))
{
    public override TileSpriteMetadata SpriteMetadata { get; } = new(Color.AnsiWhite);
}

public record RiverTile() : RenderableMapTile(TileIDs.RIVER, 247,
    new([ TileIDs.RIVER, TileIDs.DEEP_RIVER, TileIDs.SAND, TileIDs.DIRT ]),
    isolationGroup: [ TileIDs.RIVER, TileIDs.DEEP_RIVER ])
{
    public override TileSpriteMetadata SpriteMetadata { get; } = new(Color.AnsiBlue);
}

public record DeepRiverTile() : RenderableMapTile(TileIDs.DEEP_RIVER, 247,
    new([ TileIDs.DEEP_RIVER, TileIDs.RIVER ]))
{
    public override TileSpriteMetadata SpriteMetadata { get; } = new(Color.DarkBlue);
}

public record SandTile() : RenderableMapTile(TileIDs.SAND, 249,
    new([ TileIDs.SAND, TileIDs.GRASS, TileIDs.RIVER, TileIDs.DIRT ]))
{
    public override TileSpriteMetadata SpriteMetadata { get; } = new(Color.Yellow);
}

public record DirtTile() : RenderableMapTile(TileIDs.DIRT, 249,
    new([ TileIDs.DIRT, TileIDs.GRASS ]))
{
    public override TileSpriteMetadata SpriteMetadata { get; } = new(Color.SaddleBrown);
}

public class PlainsTiles() : TileAtlas(
[
    new GrassTile(),
        new TreeTile(),
        new MountainTile(),
        new MountainPeekTile(),
        new RiverTile(),
        new DeepRiverTile(),
        new SandTile(),
        new DirtTile()
], AtlasIDs.PLAINS)
{
    private static PlainsTiles? _instance;

    public static PlainsTiles Instance => _instance ??= new();
}