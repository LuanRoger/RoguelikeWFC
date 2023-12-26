namespace LawGen.WFC;

public readonly struct WavePossitionPoint(int row, int column)
{
    public int row { get; } = row;
    public int column { get; } = column;
}