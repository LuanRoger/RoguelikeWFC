namespace RoguelikeWFC.MapIO.Models;

public class SerializebleWorldMap
{
    public int version { get; init; }
    public int width { get; init; }
    public int height { get; init; }
    public required int atlasId { get; init; }
    public required byte[] tiles { get; init; }
}