namespace RoguelikeWFC.WFC;

public readonly struct Wave(int width, int height, WavePossition[,] wave)
{
    public int width { get; } = width;
    public int height { get; } = height;
    public WavePossition[,] wave { get; } = wave;

    public bool AllCollapsed()
    {
        foreach (WavePossition wavePossition in wave)
        {
            if (!wavePossition.collapsed)
                return false;
        }

        return true;
    }
    
    public void UpdateEntropyAt(WavePossitionPoint point, byte[] newEntropy)
    {
        wave[point.row, point.column] = new(newEntropy);
    }
}