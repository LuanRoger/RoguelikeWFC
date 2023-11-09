namespace RoguelikeWFC.WFC;

public struct WavePossitionPoint
{
    public int row { get; }
    public int column { get; }

    public WavePossitionPoint(int row, int column)
    {
        this.row = row;
        this.column = column;
    }
}