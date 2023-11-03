namespace RoguelikeWFC.WFC;

public class Wave
{
    public int width { get; }
    public int height { get; }
    public WavePossition[,] wave { get; }

    public Wave(int width, int height)
    {
        this.width = width;
        this.height = height;
        wave = new WavePossition[width, height];
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                wave[col, row] = new(Array.Empty<byte>());
            }
        }
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
}