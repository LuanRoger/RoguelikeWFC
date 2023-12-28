using LawGen.Core.Tiling;

namespace RoguelikeWFC.Tiles.Sprites;

public abstract record RenderableMapTile : MapTile
{
    public abstract TileSpriteMetadata SpriteMetadata { get; }
    
    protected RenderableMapTile(byte id, ushort glyph, TileSocket tileSocket, byte[]? isolationGroup = null) : 
        base(id, glyph, tileSocket, isolationGroup) { }
    
    protected RenderableMapTile(byte id, ushort[] variations, TileSocket tileSocket, byte[]? isolationGroup = null) : 
        base(id, variations, tileSocket, isolationGroup) { }
}