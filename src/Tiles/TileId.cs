namespace RoguelikeWFC.Tiles;

public readonly struct TileId
{
    private readonly byte _id;

    public TileId(byte id)
    {
        _id = id;
    }
    
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
}

public static class AtlasIDs
{
    public static readonly TileId Plains = new(1);
}