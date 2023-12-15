// ReSharper disable InconsistentNaming

using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public class GrassTile() : MapTile(TileIDs.Grass, new ushort[] { 249, 250 }, Color.AnsiGreen,
    new()
    {
        fitTop = new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand },
        fitRight = new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand },
        fitBottom = new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand },
        fitLeft = new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand }
    });

public class TreeTile() : MapTile(TileIDs.Tree, new ushort[] { 5, 231 }, Color.Brown,
    new()
    {
        fitTop = new byte[] { TileIDs.Tree, TileIDs.Grass, TileIDs.Dirt },
        fitRight = new byte[] { TileIDs.Tree, TileIDs.Grass, TileIDs.Dirt },
        fitBottom = new byte[] { TileIDs.Tree, TileIDs.Grass, TileIDs.Dirt },
        fitLeft = new byte[] { TileIDs.Tree, TileIDs.Grass, TileIDs.Dirt }
    });

public class MountainTile() : MapTile(TileIDs.Mountain, 30, Color.Gray,
    new()
    {
        fitTop = new byte[] { TileIDs.Mountain, TileIDs.MountainPeek, TileIDs.Grass, TileIDs.Sand },
        fitRight = new byte[] { TileIDs.Mountain, TileIDs.MountainPeek, TileIDs.Grass, TileIDs.Sand },
        fitBottom = new byte[] { TileIDs.Mountain, TileIDs.MountainPeek, TileIDs.Grass, TileIDs.Sand },
        fitLeft = new byte[] { TileIDs.Mountain, TileIDs.MountainPeek, TileIDs.Grass, TileIDs.Sand }
    });

public class MountainPeekTile() : MapTile(TileIDs.MountainPeek, 30, Color.AnsiWhite,
    new()
    {
        fitTop = new byte[] { TileIDs.MountainPeek, TileIDs.Mountain },
        fitRight = new byte[] { TileIDs.MountainPeek, TileIDs.Mountain },
        fitBottom = new byte[] { TileIDs.MountainPeek, TileIDs.Mountain },
        fitLeft = new byte[] { TileIDs.MountainPeek, TileIDs.Mountain }
    });

public class RiverTile() : MapTile(TileIDs.River, 247, Color.AnsiBlue,
    new()
    {
        fitTop = new byte[] { TileIDs.River, TileIDs.DeepRiver, TileIDs.Sand, TileIDs.Dirt },
        fitRight = new byte[] { TileIDs.River, TileIDs.DeepRiver, TileIDs.Sand, TileIDs.Dirt },
        fitBottom = new byte[] { TileIDs.River, TileIDs.DeepRiver, TileIDs.Sand, TileIDs.Dirt },
        fitLeft = new byte[] { TileIDs.River, TileIDs.DeepRiver, TileIDs.Sand, TileIDs.Dirt }
    });

public class DeepRiverTile() : MapTile(TileIDs.DeepRiver, 247, Color.DarkBlue,
    new()
    {
        fitTop = new byte[] { TileIDs.DeepRiver, TileIDs.River },
        fitRight = new byte[] { TileIDs.DeepRiver, TileIDs.River },
        fitBottom = new byte[] { TileIDs.DeepRiver, TileIDs.River },
        fitLeft = new byte[] { TileIDs.DeepRiver, TileIDs.River }
    });

public class SandTile() : MapTile(TileIDs.Sand, 249, Color.Yellow,
    new()
    {
        fitTop = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.River, TileIDs.Dirt },
        fitRight = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.River, TileIDs.Dirt },
        fitBottom = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.River, TileIDs.Dirt },
        fitLeft = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.River, TileIDs.Dirt }
    });

public class DirtTile() : MapTile(TileIDs.Dirt, 249, Color.SaddleBrown,
    new()
    {
        fitTop = new byte[] { TileIDs.Dirt, TileIDs.Grass },
        fitRight = new byte[] { TileIDs.Dirt, TileIDs.Grass },
        fitBottom = new byte[] { TileIDs.Dirt, TileIDs.Grass },
        fitLeft = new byte[] { TileIDs.Dirt, TileIDs.Grass }
    });

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
        new DirtTile()
        }, AtlasIDs.Plains) { }
}