
// ReSharper disable MemberCanBePrivate.Global

namespace LawGen.Core.Tiling;

public abstract record MapTile
{
    public readonly byte Id;
    public readonly ushort[] Variations;
    public readonly TileSocket TileSocket;
    public readonly byte[]? IsolationGroup;
    
    public bool hasSpriteVariants => Variations.Length > 1;

    protected MapTile(byte id, ushort glyph, TileSocket tileSocket, 
        byte[]? isolationGroup = null)
    {
        Id = id;
        Variations = [glyph];
        TileSocket = tileSocket;
        IsolationGroup = isolationGroup;
    }
    protected MapTile(byte id, ushort[] variations, TileSocket tileSocket, 
        byte[]? isolationGroup = null)
    {
        Id = id;
        Variations = variations;
        TileSocket = tileSocket;
        IsolationGroup = isolationGroup;
    }
    
    public ushort GetSprite(bool variant = false)
    {
        if(!hasSpriteVariants || !variant)
            return Variations[0];
            
        Random random = new();
        int randomSpritePicker = random.Next(Variations.Length);
        return Variations[randomSpritePicker];
    }
}