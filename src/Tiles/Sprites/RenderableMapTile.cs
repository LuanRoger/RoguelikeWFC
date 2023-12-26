﻿using LawGen.Core.Tiling;

namespace RoguelikeWFC.Tiles.Sprites;

public abstract record RenderableMapTile : MapTile
{
    public abstract TileSpriteMetadata SpriteMetadata { get; }
    
    protected RenderableMapTile(byte id, ushort glyph, TileSocket tileSocket, byte[]? isolationGroup = null) : 
        base(id, glyph, tileSocket, isolationGroup) { }
    
    protected RenderableMapTile(byte id, ushort[] variationses, TileSocket tileSocket, byte[]? isolationGroup = null) : 
        base(id, variationses, tileSocket, isolationGroup) { }
}