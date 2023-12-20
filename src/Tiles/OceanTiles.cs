using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public class OceanCoastGrass() : MapTile(TileIDs.OceanCoastGrass, 249, Color.AnsiGreen, 
    new(new byte[] { TileIDs.OceanCoastGrass, TileIDs.OceanCoatsSand }),  
    background: Color.GreenYellow);

public class OceanCoastSand() : MapTile(TileIDs.OceanCoatsSand, 178, Color.Yellow, 
    new(new byte[] { TileIDs.OceanCoatsSand, TileIDs.OceanCoastGrass, TileIDs.Water }),
    background: Color.GreenYellow);

public class WatterTile() : MapTile(TileIDs.Water, 247, Color.AnsiCyan, 
    new(new byte[] { TileIDs.Water, TileIDs.Ocean, TileIDs.OceanCoatsSand }), 
    background: Color.Aqua);

public class OceanTile() : MapTile(TileIDs.Ocean, 247, Color.AnsiBlue, 
    new(new byte[] { TileIDs.Ocean, TileIDs.DeepOcean, TileIDs.Water }), 
    background: Color.Aqua);

public class DeepOceanTile() : MapTile(TileIDs.DeepOcean, 247, Color.DarkBlue, 
    new(new byte[] { TileIDs.DeepOcean, TileIDs.Ocean }), 
    background: Color.Aqua);

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