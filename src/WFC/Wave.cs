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
        for (int i = 0; i < width * height; i++)
        {
            int row = i / width;
            int col = i % width;
            wave[col, row] = new(Array.Empty<int>());
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