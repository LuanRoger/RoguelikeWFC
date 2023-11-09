namespace RoguelikeWFC.WFC;

public class Wave
{
    public int width { get; }
    public int height { get; }
    public WavePossition[,] wave { get; }

    public Wave(int width, int height, WavePossition[,] wave)
    {
        this.width = width;
        this.height = height;
        this.wave = wave;
    }
    
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