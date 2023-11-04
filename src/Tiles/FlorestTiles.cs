// ReSharper disable InconsistentNaming

using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public class GrassTile : MapTile
{
    public GrassTile() : base(TileIDs.Grass, 249, Color.AnsiGreen,
        new()
        {
            fitTop = new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand, TileIDs.Dirt },
            fitRight = new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand, TileIDs.Dirt },
            fitBottom = new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand, TileIDs.Dirt },
            fitLeft = new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand, TileIDs.Dirt }
        }) { }
}

public class TreeTile : MapTile
{
    public TreeTile() : base(TileIDs.Tree, 231, Color.Brown, 
        new()
        {
            fitTop = new byte[] { TileIDs.Tree, TileIDs.Grass },
            fitRight = new byte[] { TileIDs.Tree, TileIDs.Grass, TileIDs.Dirt },
            fitBottom = new byte[] { TileIDs.Tree, TileIDs.Grass },
            fitLeft = new byte[] { TileIDs.Tree, TileIDs.Grass, TileIDs.Dirt }
        }) { }
}

public class WaterTile : MapTile
{
    public WaterTile() : base(TileIDs.Water, 247, Color.AnsiBlue, 
        new()
        {
            fitTop = new byte[] { TileIDs.Water, TileIDs.Sand, TileIDs.Dirt },
            fitRight = new byte[] { TileIDs.Water, TileIDs.Sand, TileIDs.Dirt },
            fitBottom = new byte[] { TileIDs.Water, TileIDs.Sand, TileIDs.Dirt },
            fitLeft = new byte[] { TileIDs.Water, TileIDs.Sand, TileIDs.Dirt }
        }) { }
}

public class MountainTile : MapTile
{
    public MountainTile() : base(TileIDs.Mountain, 30, Color.AnsiWhite, 
        new()
        {
            fitTop = new byte[] { TileIDs.Mountain, TileIDs.Grass },
            fitRight = new byte[] { TileIDs.Mountain, TileIDs.Grass },
            fitBottom = new byte[] { TileIDs.Mountain, TileIDs.Grass },
            fitLeft = new byte[] { TileIDs.Mountain, TileIDs.Grass }
        }) { }
}

public class SandTile : MapTile
{
    public SandTile() : base(TileIDs.Sand, 249, Color.AnsiYellow, 
        new()
        {
            fitTop = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.Water },
            fitRight = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.Water },
            fitBottom = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.Water },
            fitLeft = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.Water }
        }) { }
}

public class DirtTile : MapTile
{
    public DirtTile() : base(TileIDs.Dirt, 249,  Color.SandyBrown, 
        new()
        {
            fitTop = new byte[] { TileIDs.Dirt, TileIDs.Grass },
            fitRight = new byte[] { TileIDs.Dirt, TileIDs.Grass },
            fitBottom = new byte[] { TileIDs.Dirt, TileIDs.Grass },
            fitLeft = new byte[] { TileIDs.Dirt, TileIDs.Grass },
        }) { }
}

public class FlorestTiles : TileSet
{
    private static FlorestTiles? _instance;

    public static FlorestTiles Instance
    {
        get
        {
            _instance ??= new();
            return _instance;
        }
    }

    private FlorestTiles() : 
        base(new MapTile[] {
        new GrassTile(),
        new TreeTile(),
        new WaterTile(),
        new MountainTile(),
        new DirtTile(),
        new SandTile(),
        }) { }
}