using RoguelikeWFC.Tiles;
// ReSharper disable MemberCanBePrivate.Global

namespace RoguelikeWFC.WFC;

public abstract class MapTile
{
    public readonly byte Id;
    public readonly ushort[] Glyph;
    public readonly Color Color;
    public readonly TileSocket TileSocket;
    
    public bool hasSpriteVariants => Glyph.Length > 1;

    protected MapTile(TileId id, ushort glyph, Color color, TileSocket tileSocket)
    {
        Id = id;
        Glyph = new[] { glyph };
        Color = color;
        TileSocket = tileSocket;
    }
    protected MapTile(TileId id, char glyph, Color color, TileSocket tileSocket)
    {
        Id = id;
        Glyph = new ushort[] { glyph };
        Color = color;
        TileSocket = tileSocket;
    }
    protected MapTile(TileId id, ushort[] glyphs, Color color, TileSocket tileSocket)
    {
        Id = id;
        Glyph = glyphs;
        Color = color;
        TileSocket = tileSocket;
    }
    
    public ushort GetSprite(bool variant = false)
    {
        if(!hasSpriteVariants || !variant)
            return Glyph[0];
            
        Random random = new();
        int randomSpritePicker = random.Next(Glyph.Length);
        return Glyph[randomSpritePicker];
    }
}