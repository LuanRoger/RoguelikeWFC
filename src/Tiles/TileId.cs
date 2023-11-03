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
    public static readonly TileId Water = new(4);
    public static readonly TileId Sand = new(5);
    public static readonly TileId Dirt = new(6);
    public static readonly TileId Mountain = new(7);
    public static readonly TileId MountainPeek = new(8);
}