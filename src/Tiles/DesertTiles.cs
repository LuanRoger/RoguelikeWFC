using LawGen.Core.Tiling;
using RoguelikeWFC.Tiles.Sprites;

namespace RoguelikeWFC.Tiles;

public record DesertSandTile() : RenderableMapTile(TileIDs.DESERT_SAND, [ 176, 177, 178 ],
    new([ TileIDs.DESERT_SAND, TileIDs.ROCK, TileIDs.CACTUS ]))
{
    public override TileSpriteMetadata SpriteMetadata { get; } = 
        new(Color.AnsiYellowBright, Color.LightYellow);
}

public record RockTile() : RenderableMapTile(TileIDs.ROCK, 239,
    new([ TileIDs.DESERT_SAND ]))
{
    public override TileSpriteMetadata SpriteMetadata { get; } = 
        new(Color.DimGray, Color.LightYellow);
}

public record CactusTile() : RenderableMapTile(TileIDs.CACTUS, 33,
    new([ TileIDs.DESERT_SAND ]))
{
    public override TileSpriteMetadata SpriteMetadata { get; } = 
        new(Color.LimeGreen, Color.LightYellow);
}

public class DesertTiles() : TileAtlas(
[
    new DesertSandTile(),
        new RockTile(),
        new CactusTile()
], AtlasIDs.DESERT, [
    new(TileIDs.DESERT_SAND, 0.8f),
    new(TileIDs.ROCK, 0.05f),
    new(TileIDs.CACTUS, 0.1f)
])
{
    private static DesertTiles? _instance;

    public static DesertTiles Instance
    {
        get
        {
            _instance ??= new();
            return _instance;
        }
    }
}