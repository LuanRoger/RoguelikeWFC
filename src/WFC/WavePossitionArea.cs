namespace RoguelikeWFC.WFC;

public readonly struct WavePossitionArea(WavePossitionPoint possitionPoint)
{
    public readonly WavePossitionPoint Top = new(possitionPoint.row - 1, possitionPoint.column);
    public readonly WavePossitionPoint Right = new(possitionPoint.row, possitionPoint.column + 1);
    public readonly WavePossitionPoint Bottom = new(possitionPoint.row + 1, possitionPoint.column);
    public readonly WavePossitionPoint Left = new(possitionPoint.row, possitionPoint.column - 1);
}