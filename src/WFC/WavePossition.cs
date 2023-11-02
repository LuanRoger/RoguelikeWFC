namespace RoguelikeWFC.WFC;

public class WavePossition
{
    public int[] entropy { get; set; }
    public bool collapsed => entropy.Length == 1;

    public WavePossition(int[] entropy)
    {
        this.entropy = entropy;
    }
}