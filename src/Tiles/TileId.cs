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
    public static readonly TileId RiverTopBranch = new(5);
    public static readonly TileId RiverRightBranch = new(6);
    public static readonly TileId RiverBottomBranch = new(7);
    public static readonly TileId RiverLeftBranch = new(8);
    public static readonly TileId RiverFromBottomToLeftCurve = new(9);
    public static readonly TileId RiverFromBottomToRightCurve = new(10);
    public static readonly TileId RiverFromLeftToUpCurver = new(11);
    public static readonly TileId RiverFromLeftToBottomCurver = new(12);
    public static readonly TileId RiverFromUpToLeftCurve = new(13);
    public static readonly TileId RiverFromUpToRightCurve = new(14);
    public static readonly TileId RiverFromRightToUpCurve = new(15);
    public static readonly TileId RiverFromRightToBottomCurve = new(16);
    public static readonly TileId Sand = new(17);
    public static readonly TileId Dirt = new(18);
    public static readonly TileId Mountain = new(19);
    public static readonly TileId MountainPeek = new(20);
}