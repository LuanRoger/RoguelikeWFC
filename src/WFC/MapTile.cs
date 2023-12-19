using RoguelikeWFC.Tiles;
// ReSharper disable MemberCanBePrivate.Global

namespace RoguelikeWFC.WFC;

public abstract class MapTile
{
    public readonly byte Id;
    public readonly ushort[] Glyph;
    public readonly Color Color;
    public readonly Color? Background;
    public readonly TileSocket TileSocket;
    public readonly byte[]? IsolationGroup;
    
    public bool hasSpriteVariants => Glyph.Length > 1;

    protected MapTile(TileId id, ushort glyph, Color color, TileSocket tileSocket, 
        byte[]? isolationGroup = null, Color? background = null)
    {
        Id = id;
        Glyph = new[] { glyph };
        Color = color;
        Background = background;
        TileSocket = tileSocket;
        IsolationGroup = isolationGroup;
    }
    protected MapTile(TileId id, char glyph, Color color, TileSocket tileSocket, 
        byte[]? isolationGroup = null, Color? background = null)
    {
        Id = id;
        Glyph = new ushort[] { glyph };
        Color = color;
        Background = background;
        TileSocket = tileSocket;
        IsolationGroup = isolationGroup;
    }
    protected MapTile(TileId id, ushort[] glyphs, Color color, TileSocket tileSocket, 
        byte[]? isolationGroup = null, Color? background = null)
    {
        Id = id;
        Glyph = glyphs;
        Color = color;
        Background = background;
        TileSocket = tileSocket;
        IsolationGroup = isolationGroup;
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