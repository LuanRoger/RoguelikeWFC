using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public class GrassTile() : MapTile(TileIDs.Grass, new ushort[] { 249, 250 }, Color.AnsiGreen,
    new(new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand }));

public class TreeTile() : MapTile(TileIDs.Tree, new ushort[] { 5, 231 }, Color.Brown,
    new(new byte[] { TileIDs.Tree, TileIDs.Grass, TileIDs.Dirt }));

public class MountainTile() : MapTile(TileIDs.Mountain, 30, Color.Gray,
    new(new byte[] { TileIDs.Mountain, TileIDs.MountainPeek, TileIDs.Grass, TileIDs.Sand }));

public class MountainPeekTile() : MapTile(TileIDs.MountainPeek, 30, Color.AnsiWhite,
    new(new byte[] { TileIDs.MountainPeek, TileIDs.Mountain }));

public class RiverTile() : MapTile(TileIDs.River, 247, Color.AnsiBlue,
    new(new byte[] { TileIDs.River, TileIDs.DeepRiver, TileIDs.Sand, TileIDs.Dirt }));

public class DeepRiverTile() : MapTile(TileIDs.DeepRiver, 247, Color.DarkBlue,
    new(new byte[] { TileIDs.DeepRiver, TileIDs.River }));

public class SandTile() : MapTile(TileIDs.Sand, 249, Color.Yellow,
    new(new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.River, TileIDs.Dirt }));

public class DirtTile() : MapTile(TileIDs.Dirt, 249, Color.SaddleBrown,
    new(new byte[] { TileIDs.Dirt, TileIDs.Grass }));

public class PlainsTiles() : TileAtlas(
    new MapTile[]
    {
        new GrassTile(),
        new TreeTile(),
        new MountainTile(),
        new MountainPeekTile(),
        new RiverTile(),
        new DeepRiverTile(),
        new SandTile(),
        new DirtTile()
    }, AtlasIDs.Plains)
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
}