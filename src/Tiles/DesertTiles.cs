using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public class DesertSandTile() : MapTile(TileIDs.DesertSand, new ushort[] { 176, 177, 178 }, Color.AnsiYellowBright, new()
{
    fitTop = new byte[] { TileIDs.DesertSand, TileIDs.Rock, TileIDs.Cactus },
    fitRight = new byte[] { TileIDs.DesertSand, TileIDs.Rock, TileIDs.Cactus },
    fitBottom = new byte[] { TileIDs.DesertSand, TileIDs.Rock, TileIDs.Cactus },
    fitLeft = new byte[] { TileIDs.DesertSand, TileIDs.Rock, TileIDs.Cactus },
}, Color.LightYellow);

public class RockTile() : MapTile(TileIDs.Rock, new ushort[] { 239 }, Color.DimGray, new()
{
    fitTop = new byte[] { TileIDs.DesertSand},
    fitRight = new byte[] { TileIDs.DesertSand },
    fitBottom = new byte[] { TileIDs.DesertSand },
    fitLeft = new byte[] {  TileIDs.DesertSand },
}, Color.LightYellow);

public class CactusTile() : MapTile(TileIDs.Cactus, new ushort[] { 33 }, Color.LimeGreen, new()
{
    fitTop = new byte[] { TileIDs.DesertSand },
    fitRight = new byte[] { TileIDs.DesertSand },
    fitBottom = new byte[] { TileIDs.DesertSand },
    fitLeft = new byte[] { TileIDs.DesertSand },
}, Color.LightYellow);

public class DesertTiles() : TileAtlas(
    new MapTile[]
    {
        new DesertSandTile(),
        new RockTile(),
        new CactusTile(),
    }, AtlasIDs.Desert, new()
    {
        { TileIDs.DesertSand, 0.8f },
        { TileIDs.Rock, 0.05f },
        { TileIDs.Cactus, 0.1f }
    })
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