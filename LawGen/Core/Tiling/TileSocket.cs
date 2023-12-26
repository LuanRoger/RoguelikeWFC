namespace LawGen.Core.Tiling;

public struct TileSocket
{
    public static TileSocket Empty => new();

    public TileSocket()
    {
        fitTop = Array.Empty<byte>();
        fitRight = Array.Empty<byte>();
        fitBottom = Array.Empty<byte>();
        fitLeft = Array.Empty<byte>();
    }

    public TileSocket(byte[] fit)
    {
        int fitRuleLength = fit.Length;
        fitTop = new byte[fitRuleLength];
        fitRight = new byte[fitRuleLength];
        fitBottom = new byte[fitRuleLength];
        fitLeft = new byte[fitRuleLength];
        
        Array.Copy(fit, fitTop, fitRuleLength);
        Array.Copy(fit, fitRight, fitRuleLength);
        Array.Copy(fit, fitBottom, fitRuleLength);
        Array.Copy(fit, fitLeft, fitRuleLength);
    }
    
    public byte[] fitTop { get; init; }
    public byte[] fitRight { get; init; }
    public byte[] fitBottom { get; init; }
    public byte[] fitLeft { get; init; }
}