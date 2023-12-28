namespace LawGen.Core.Tiling.Internals;

public record SuperTile() : 
    MapTile(0, 0, TileSocket.Empty)
{
    public required byte[] Entropy { get; init; }
    public bool inConflict => Entropy.Length == 0;
}