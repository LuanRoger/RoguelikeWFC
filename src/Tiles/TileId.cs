namespace RoguelikeWFC.Tiles;

public readonly struct TileId(byte id)
{
    private readonly byte _id = id;

    public static implicit operator byte(TileId tileId) => 
        tileId._id;
}

public static class TileIDs
{ 
    public static readonly TileId Null = new(0);
    public static readonly TileId Text = new(1);
    public static readonly TileId Grass = new(2);
    public static readonly TileId Tree = new(3);
    public static readonly TileId River = new(4);
    public static readonly TileId DeepRiver = new(5);
    public static readonly TileId Sand = new(6);
    public static readonly TileId Mountain = new(8);
    public static readonly TileId MountainPeek = new(9);
    public static readonly TileId Dirt = new(10);
    public static readonly TileId DesertSand = new(11);
    public static readonly TileId Rock = new(12);
    public static readonly TileId Cactus = new(13);
    public static readonly TileId DeepOcean = new(14);
    public static readonly TileId Ocean = new(15);
    public static readonly TileId Water = new(16);
    public static readonly TileId OceanCoatsSand = new(17);
    public static readonly TileId OceanCoastGrass = new(18);
}

public static class AtlasIDs
{
    public static readonly TileId Plains = new(1);
    public static readonly TileId Desert = new(2);
    public static readonly TileId Ocean = new(3);
}