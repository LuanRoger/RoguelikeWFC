using RoguelikeWFC.Tiles;

namespace RoguelikeWFC.WFC;

public abstract class MapTile
{
    public readonly byte id;
    public readonly int glyph;
    public readonly Color color;
    public readonly TileSocket tileSocket;

    protected MapTile(TileId id, int glyph, Color color, TileSocket tileSocket)
    {
        this.id = id;
        this.glyph = glyph;
        this.color = color;
        this.tileSocket = tileSocket;
    }
    protected MapTile(TileId id, char glyph, Color color, TileSocket tileSocket)
    {
        this.id = id;
        this.glyph = glyph;
        this.color = color;
        this.tileSocket = tileSocket;
    }
}