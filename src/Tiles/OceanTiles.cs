using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public class OceanCoastGrass() : MapTile(TileIDs.OceanCoastGrass, 249, Color.AnsiGreen, new()
{
    fitTop = new byte[] { TileIDs.OceanCoastGrass, TileIDs.OceanCoatsSand },
    fitRight = new byte[] { TileIDs.OceanCoastGrass, TileIDs.OceanCoatsSand },
    fitBottom = new byte[] { TileIDs.OceanCoastGrass, TileIDs.OceanCoatsSand },
    fitLeft = new byte[] { TileIDs.OceanCoastGrass, TileIDs.OceanCoatsSand }
}, canBeIsolated: false, background: Color.GreenYellow);

public class OceanCoastSand() : MapTile(TileIDs.OceanCoatsSand, 178, Color.Yellow, new()
{
    fitTop = new byte[] { TileIDs.OceanCoatsSand, TileIDs.OceanCoastGrass, TileIDs.Water },
    fitRight = new byte[] { TileIDs.OceanCoatsSand, TileIDs.OceanCoastGrass, TileIDs.Water },
    fitBottom = new byte[] { TileIDs.OceanCoatsSand, TileIDs.OceanCoastGrass, TileIDs.Water },
    fitLeft = new byte[] { TileIDs.OceanCoatsSand, TileIDs.OceanCoastGrass, TileIDs.Water }
}, canBeIsolated: false, background: Color.GreenYellow);

public class WatterTile() : MapTile(TileIDs.Water, 247, Color.AnsiCyan, new()
{
    fitTop = new byte[] { TileIDs.Water, TileIDs.Ocean, TileIDs.OceanCoatsSand },
    fitRight = new byte[] { TileIDs.Water, TileIDs.Ocean, TileIDs.OceanCoatsSand },
    fitBottom = new byte[] { TileIDs.Water, TileIDs.Ocean, TileIDs.OceanCoatsSand },
    fitLeft = new byte[] { TileIDs.Water, TileIDs.Ocean, TileIDs.OceanCoatsSand }
}, canBeIsolated: false, background: Color.Aqua);

public class OceanTile() : MapTile(TileIDs.Ocean, 247, Color.AnsiBlue, new()
{
    fitTop = new byte[] { TileIDs.Ocean, TileIDs.DeepOcean, TileIDs.Water },
    fitRight = new byte[] { TileIDs.Ocean, TileIDs.DeepOcean, TileIDs.Water },
    fitBottom = new byte[] { TileIDs.Ocean, TileIDs.DeepOcean, TileIDs.Water },
    fitLeft = new byte[] { TileIDs.Ocean, TileIDs.DeepOcean, TileIDs.Water }
}, canBeIsolated: false, background: Color.Aqua);

public class DeepOceanTile() : MapTile(TileIDs.DeepOcean, 247, Color.DarkBlue, new()
{
    fitTop = new byte[] { TileIDs.DeepOcean, TileIDs.Ocean },
    fitRight = new byte[] { TileIDs.DeepOcean, TileIDs.Ocean },
    fitBottom = new byte[] { TileIDs.DeepOcean, TileIDs.Ocean },
    fitLeft = new byte[] { TileIDs.DeepOcean, TileIDs.Ocean }
}, canBeIsolated: false, background: Color.Aqua);

public class OceanAtlas() : TileAtlas(new MapTile[]
{
    new OceanCoastGrass(),
    new OceanCoastSand(),
    new WatterTile(),
    new OceanTile(),
    new DeepOceanTile()
}, AtlasIDs.Ocean)
{
    private static OceanAtlas? _instance;

    public static OceanAtlas Instance
    {
        get
        {
            _instance ??= new();
            return _instance;
        }
    }
}