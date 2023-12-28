using LawGen.Core.Wave.Types;

namespace LawGen.Core.Wave;

public readonly struct WaveMatrix(int width, int height, WavePossition[,] wave) : IWaveMatrixMapCommons
{
    public readonly int Width = width;
    public readonly int Height = height;
    public WavePossition[,] wave { get; } = wave;
    public int CollapsedTilesCount
    {
        get
        {
            int count = 0;
            foreach (WavePossition wavePossition in wave)
            {
                if(wavePossition is { collapsed: true, conflict: false })
                    count++;
            }
        
            return count;
        }
    }
    public bool HasOnlyConflicts
    {
        get
        {
            foreach (WavePossition wavePossition in wave)
            {
                if (wavePossition is { collapsed: false, conflict: false })
                    return false;
            }

            return true;
        }
    }
    public int GetCountOfConflicts
    {
        get
        {
            int count = 0;
            foreach (WavePossition wavePossition in wave)
            {
                if(wavePossition.conflict)
                    count++;
            }
        
            return count;
        }
    }
    
    public void UpdateEntropyAt(WavePossitionPoint point, byte[] newEntropy)
    {
        wave[point.row, point.column] = new(newEntropy);
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
    public WavePossition GetPossitionAtPoint(WavePossitionPoint point) =>
        wave[point.row, point.column];
    
    public WavePossitionPoint GetSmallerEntropyPossition(bool includeConflicts = false)
    {
        int smallerEntropyRow = 0;
        int smallerEntropyCol = 0;
        int smallerEntropyLength = int.MaxValue;
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                WavePossition wavePossition = wave[row, col];
                
                if (wavePossition.collapsed ||
                    !includeConflicts && wavePossition.conflict ||
                    wavePossition.Entropy.Length >= smallerEntropyLength) 
                    continue;
                
                smallerEntropyLength = wavePossition.Entropy.Length;
                smallerEntropyRow = row;
                smallerEntropyCol = col;
            }
        }

        return new(smallerEntropyRow, smallerEntropyCol);
    }
}