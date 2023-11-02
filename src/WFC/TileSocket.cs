namespace RoguelikeWFC.WFC;

public struct TileSocket
{
    /// <summary>
    /// The tile id that this tile can fit with
    /// </summary>
    public int[] canFit { get; }
    
    public TileSocket(int[] canFit)
    {
        this.canFit = canFit;
    }
}