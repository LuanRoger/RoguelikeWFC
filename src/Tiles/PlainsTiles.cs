using LawGen.Core.Tiling;

namespace RoguelikeWFC.Tiles;

public record GrassTile() : MapTile(TileIDs.GRASS, [249, 250],
    new([TileIDs.GRASS, TileIDs.TREE, TileIDs.MOUNTAIN, TileIDs.SAND]))
{
    public readonly TileSpriteMetadata SpriteMetadata = new(Color.Green);
}

public record TreeTile() : MapTile(TileIDs.TREE, [5, 231],
    new([TileIDs.TREE, TileIDs.GRASS, TileIDs.DIRT]))
{
    public readonly TileSpriteMetadata SpriteMetadata = new(Color.Brown);
}

public record MountainTile() : MapTile(TileIDs.MOUNTAIN, 30,
    new([ TileIDs.MOUNTAIN, TileIDs.MOUNTAIN_PEEK, TileIDs.GRASS, TileIDs.SAND ]),
    isolationGroup: [ TileIDs.MOUNTAIN, TileIDs.MOUNTAIN_PEEK ])
{
    public readonly TileSpriteMetadata SpriteMetadata = new(Color.Gray);
}

public record MountainPeekTile() : MapTile(TileIDs.MOUNTAIN_PEEK, 30,
    new([ TileIDs.MOUNTAIN_PEEK, TileIDs.MOUNTAIN ]))
{
    public readonly TileSpriteMetadata SpriteMetadata = new(Color.AnsiWhite);
}

public record RiverTile() : MapTile(TileIDs.RIVER, 247,
    new([ TileIDs.RIVER, TileIDs.DEEP_RIVER, TileIDs.SAND, TileIDs.DIRT ]),
    isolationGroup: [ TileIDs.RIVER, TileIDs.DEEP_RIVER ])
{
    public readonly TileSpriteMetadata SpriteMetadata = new(Color.AnsiBlue);
}

public record DeepRiverTile() : MapTile(TileIDs.DEEP_RIVER, 247,
    new([ TileIDs.DEEP_RIVER, TileIDs.RIVER ]))
{
    public readonly TileSpriteMetadata SpriteMetadata = new(Color.DarkBlue);
}

public record SandTile() : MapTile(TileIDs.SAND, 249,
    new([ TileIDs.SAND, TileIDs.GRASS, TileIDs.RIVER, TileIDs.DIRT ]))
{
    public readonly TileSpriteMetadata SpriteMetadata = new(Color.Yellow);
}

public record DirtTile() : MapTile(TileIDs.DIRT, 249,
    new([
        TileIDs.DIRT, TileIDs.GRASS
    ]))
{
    public readonly TileSpriteMetadata SpriteMetadata = new(Color.SaddleBrown);
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