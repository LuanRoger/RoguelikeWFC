namespace RoguelikeWFC.WFC;

public struct WavePossition(byte[] entropy)
{
    public readonly byte[] Entropy = entropy;
    public bool collapsed => Entropy.Length == 1;
    public bool hasConflict => !Entropy.Any();

    public static explicit operator WavePossition(byte[] entropy) => 
        new(entropy);
}