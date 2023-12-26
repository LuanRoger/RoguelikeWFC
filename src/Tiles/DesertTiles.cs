using LawGen.Core.Tiling;

namespace RoguelikeWFC.Tiles;

public record DesertSandTile() : MapTile(TileIDs.DESERT_SAND, [ 176, 177, 178 ],
    new([ TileIDs.DESERT_SAND, TileIDs.ROCK, TileIDs.CACTUS ]))
{
    public readonly TileSpriteMetadata SpriteMetadata = 
        new(Color.AnsiYellowBright, Color.LightYellow);
}

public record RockTile() : MapTile(TileIDs.ROCK, 239,
    new([ TileIDs.DESERT_SAND ]))
{
    public readonly TileSpriteMetadata SpriteMetadata = 
        new(Color.DimGray, Color.LightYellow);
}

public record CactusTile() : MapTile(TileIDs.CACTUS, 33,
    new([ TileIDs.DESERT_SAND ]))
{
    public readonly TileSpriteMetadata SpriteMetadata = 
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