// ReSharper disable InconsistentNaming

using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public class GrassTile : MapTile
{
    public GrassTile() : base(TileIDs.Grass, 249, Color.AnsiGreen,
        new()
        {
            fitTop = new byte[] { TileIDs.Grass, TileIDs.Mountain, TileIDs.Sand },
            fitRight = new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand },
            fitBottom = new byte[] { TileIDs.Grass, TileIDs.Mountain, TileIDs.Sand },
            fitLeft = new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand }
        }) { }
}

public class TreeTile : MapTile
{
    public TreeTile() : base(TileIDs.Tree, 5, Color.Brown, 
        new()
        {
            fitTop = new byte[] { TileIDs.Tree, TileIDs.Grass },
            fitRight = new byte[] { TileIDs.Tree, TileIDs.Grass },
            fitBottom = new byte[] { TileIDs.Tree, TileIDs.Grass },
            fitLeft = new byte[] { TileIDs.Tree, TileIDs.Grass }
        }) { }
}

public class MountainTile : MapTile
{
    public MountainTile() : base(TileIDs.Mountain, 30, Color.Gray,
        new()
        {
            fitTop = new byte[] { TileIDs.Mountain, TileIDs.MountainPeek,  TileIDs.Grass, TileIDs.Sand },
            fitRight = new byte[] { TileIDs.Mountain, TileIDs.MountainPeek, TileIDs.Grass, TileIDs.Sand },
            fitBottom = new byte[] { TileIDs.Mountain, TileIDs.MountainPeek, TileIDs.Grass, TileIDs.Sand },
            fitLeft = new byte[] { TileIDs.Mountain, TileIDs.MountainPeek, TileIDs.Grass, TileIDs.Sand }
        }) { }
}

public class MountainPeekTile : MapTile
{
    public MountainPeekTile() : base(TileIDs.MountainPeek, 30, Color.AnsiWhite,
        new()
        {
            fitTop = new byte[] { TileIDs.MountainPeek, TileIDs.Mountain, TileIDs.Sand },
            fitRight = new byte[] { TileIDs.MountainPeek, TileIDs.Mountain },
            fitBottom = new byte[] { TileIDs.MountainPeek, TileIDs.Mountain, TileIDs.Sand },
            fitLeft = new byte[] { TileIDs.MountainPeek, TileIDs.Mountain }
        }) { }

}

public class RiverTile : MapTile
{
    public RiverTile() : base(TileIDs.River, 247, Color.AnsiBlue,
        new()
        {
            fitTop = new byte[] { TileIDs.River, TileIDs.DeepRiver, TileIDs.Grass, TileIDs.Sand },
            fitRight = new byte[] { TileIDs.River, TileIDs.DeepRiver, TileIDs.Grass, TileIDs.Sand },
            fitBottom = new byte[] { TileIDs.River, TileIDs.DeepRiver, TileIDs.Grass, TileIDs.Sand },
            fitLeft = new byte[] { TileIDs.River, TileIDs.DeepRiver, TileIDs.Grass, TileIDs.Sand }
        }) { }
}

public class DeepRiverTile : MapTile
{
    public DeepRiverTile() : base(TileIDs.DeepRiver, 247, Color.AnsiCyan,
        new()
        {
            fitTop = new byte[] { TileIDs.DeepRiver, TileIDs.River },
            fitRight = new byte[] { TileIDs.DeepRiver, TileIDs.River },
            fitBottom = new byte[] { TileIDs.DeepRiver, TileIDs.River },
            fitLeft = new byte[] { TileIDs.DeepRiver, TileIDs.River }
        }) { }
}

public class SandTile : MapTile
{
    public SandTile() : base(TileIDs.Sand, 249, Color.Yellow, 
        new()
        {
            fitTop = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.River },
            fitRight = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.River },
            fitBottom = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.River },
            fitLeft = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.River }
        }) { }
}

public class PlainsTiles : TileAtlas
{
    private static PlainsTiles? _instance;

    public static PlainsTiles Instance
    {
        get
        {
            _instance ??= new();
            return _instance;
        }
    }

    private PlainsTiles() : 
        base(new MapTile[] {
        new GrassTile(),
        new TreeTile(),
        new MountainTile(),
        new MountainPeekTile(),
        new RiverTile(),
        new DeepRiverTile(),
        new SandTile(),
        }) { }
}