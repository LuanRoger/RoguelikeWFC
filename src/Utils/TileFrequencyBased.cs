namespace RoguelikeWFC.Utils;

public static class TileFrequencyBased
{
    public static byte PickTileIdByFrequency(ref Random rng, Dictionary<byte, float> tileFrequency)
    {
        float random = rng.NextSingle();
        foreach ((byte tileId, float frequency) in tileFrequency)
        {
            if (random < frequency)
                return tileId;
            random -= frequency;
        }
        return tileFrequency.Keys.First();
    }
}