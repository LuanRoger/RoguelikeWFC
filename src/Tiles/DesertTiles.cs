using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public class DesertSandTile() : MapTile(TileIDs.DesertSand, new ushort[] { 176 }, Color.LightYellow, new()
{
    fitTop = new byte[] { TileIDs.DesertSand, TileIDs.Rock, TileIDs.Cactus },
    fitRight = new byte[] { TileIDs.DesertSand, TileIDs.Rock, TileIDs.Cactus },
    fitBottom = new byte[] { TileIDs.DesertSand, TileIDs.Rock, TileIDs.Cactus },
    fitLeft = new byte[] { TileIDs.DesertSand, TileIDs.Rock, TileIDs.Cactus },
});

public class RockTile() : MapTile(TileIDs.Rock, new ushort[] { 239 }, Color.DimGray, new()
{
    fitTop = new byte[] { TileIDs.DesertSand},
    fitRight = new byte[] { TileIDs.DesertSand },
    fitBottom = new byte[] { TileIDs.DesertSand },
    fitLeft = new byte[] {  TileIDs.DesertSand },
});

public class CactusTile() : MapTile(TileIDs.Cactus, new ushort[] { 33 }, Color.LimeGreen, new()
{
    fitTop = new byte[] { TileIDs.DesertSand },
    fitRight = new byte[] { TileIDs.DesertSand },
    fitBottom = new byte[] { TileIDs.DesertSand },
    fitLeft = new byte[] { TileIDs.DesertSand },
});

public class DesertTiles() : TileAtlas(
    new MapTile[]
    {
        new DesertSandTile(),
        new RockTile(),
        new CactusTile(),
    }, AtlasIDs.Desert)
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