namespace RoguelikeWFC.WFC;

public struct WavePossitionPoint(int row, int column)
{
    public int row { get; } = row;
    public int column { get; } = column;
}