namespace LawGen.Core.Wave.Types;

public readonly struct WavePossition(byte[] entropy)
{
    public readonly byte[] Entropy = entropy;
    public bool collapsed => Entropy.Length == 1;
    public bool conflict => Entropy.Length == 0;

    public static explicit operator WavePossition(byte[] entropy) => 
        new(entropy);
}