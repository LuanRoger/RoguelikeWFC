namespace RoguelikeWFC.WFC;

public class WavePossition
{
    public byte[] entropy { get; set; }
    public bool collapsed => entropy.Length == 1;

    public WavePossition(byte[] entropy)
    {
        this.entropy = entropy;
    }
}