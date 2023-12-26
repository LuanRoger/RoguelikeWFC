namespace LawGen.WFC;

public readonly struct WavePossitionArea
{
    public readonly int TopRaw;
    public readonly int RightRaw;
    public readonly int BottomRaw;
    public readonly int LeftRaw;
    
    public readonly WavePossitionPoint Top;
    public readonly WavePossitionPoint Right;
    public readonly WavePossitionPoint Bottom;
    public readonly WavePossitionPoint Left;

    public WavePossitionArea(WavePossitionPoint possitionPoint)
    {
        TopRaw = possitionPoint.row - 1;
        RightRaw = possitionPoint.column + 1;
        BottomRaw = possitionPoint.row + 1;
        LeftRaw = possitionPoint.column - 1;

        Top = new(TopRaw, possitionPoint.column);
        Right = new(possitionPoint.row, RightRaw);
        Bottom = new(BottomRaw, possitionPoint.column);
        Left = new(possitionPoint.row, LeftRaw);
    }
}