using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public class DesertSandTile() : MapTile(TileIDs.DesertSand, new ushort[] { 176, 177, 178 }, Color.AnsiYellowBright, 
    new(new byte[] { TileIDs.DesertSand, TileIDs.Rock, TileIDs.Cactus }), 
    background: Color.LightYellow);

public class RockTile() : MapTile(TileIDs.Rock, new ushort[] { 239 }, Color.DimGray, 
    new(new byte[] { TileIDs.DesertSand }), 
    background: Color.LightYellow);

public class CactusTile() : MapTile(TileIDs.Cactus, new ushort[] { 33 }, Color.LimeGreen, 
    new(new byte[] { TileIDs.DesertSand }), 
    background: Color.LightYellow);

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