using LawGen.Core.Wave.Types;

namespace LawGen.Core.Wave;

public readonly struct WaveMatrix(uint width, uint height, WavePossition[,] wave) : IWaveMatrixCommons
{
    public readonly uint Width = width;
    public readonly uint Height = height;
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
    public int ConflictsTilesCount
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
    
    /// <summary>
    /// Check if the offset area around the tile is out of bound.
    /// </summary>
    /// <param name="possitionArea">The tile area</param>
    /// <param name="top">The top side is out of bounds</param>
    /// <param name="right">The right side is out of bounds</param>
    /// <param name="bottom">The bottom side is out of bounds</param>
    /// <param name="left">The left side is out of bounds</param>
    /// <returns>Return <c>true</c> if one of the sides is out of bound</returns>
    public bool CheckAreaOutOfBound(in WavePossitionArea possitionArea, out bool top, out bool right, out bool bottom, out bool left)
    {
        top = possitionArea.TopRaw < 0 || possitionArea.TopRaw >= Height;
        right = possitionArea.RightRaw < 0 || possitionArea.RightRaw >= Width;
        bottom = possitionArea.BottomRaw < 0 || possitionArea.BottomRaw >= Height;
        left = possitionArea.LeftRaw < 0 || possitionArea.LeftRaw >= Width;

        return top || right || bottom || left;
    }
}