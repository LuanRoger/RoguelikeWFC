namespace RoguelikeWFC.WFC;

public abstract class MapTile
{
    public abstract int id { get; }
    public abstract char sprite { get; }
    public abstract Color color { get; }
    public abstract TileSocket tileSocket { get; }
}