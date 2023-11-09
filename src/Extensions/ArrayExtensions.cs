namespace RoguelikeWFC.Extensions;

public static class ArrayExtensions
{
    public static byte[] Intersect(this byte[] a, byte[] b)
    {
        var result = new List<byte>();
        foreach (byte item in a)
        {
            if (b.Contains(item))
                result.Add(item);
        }
        return result.ToArray();
    }
}