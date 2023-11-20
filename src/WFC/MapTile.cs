using RoguelikeWFC.Tiles;
// ReSharper disable MemberCanBePrivate.Global

namespace RoguelikeWFC.WFC;

public abstract class MapTile
{
    public readonly byte Id;
    public readonly ushort[] Glyph;
    public readonly Color Color;
    public readonly TileSocket TileSocket;
    
    private ushort? _instanceSprite;
    public bool hasSpriteVariants => Glyph.Length > 1;
    public ushort sprite
    {
        get
        {
            if(_instanceSprite is not null)
                return _instanceSprite.Value;
            if(!hasSpriteVariants)
            {
                _instanceSprite = Glyph[0];
                return _instanceSprite.Value;
            }
            
            Random random = new();
            int randomSpritePicker = random.Next(Glyph.Length);
            _instanceSprite = Glyph[randomSpritePicker];
            return _instanceSprite.Value;
        }
    }

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
    
    public void ResetSprite()
    {
        _instanceSprite = null;
    }
}