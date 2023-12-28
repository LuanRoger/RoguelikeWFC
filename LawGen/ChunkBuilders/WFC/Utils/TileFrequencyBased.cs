using LawGen.Core.Tiling;

namespace LawGen.WFC.Utils;

public static class TileFrequencyBased
{
    public static byte PickTileIdByFrequency(ref Random rng, CollapseFrequency[] collapseFrequencies)
    {
        float random = rng.NextSingle();
        foreach (CollapseFrequency frequency in collapseFrequencies)
        {
            if (random < frequency.Frequency)
                return frequency.TileId;
            random -= frequency.Frequency;
        }
        return collapseFrequencies[0].TileId;
    }
}