namespace RoguelikeWFC.WFC;

public struct WavePossition
{
    public readonly byte[] Entropy;
    public bool collapsed => Entropy.Length == 1;
    public bool hasConflict => !Entropy.Any();

    public WavePossition(byte[] entropy)
    {
        Entropy = entropy;
    }
    
    public static explicit operator WavePossition(byte[] entropy) => 
        new(entropy);
}