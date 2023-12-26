namespace RoguelikeWFC.Tiles;

public readonly struct TileSpriteMetadata(Color foreground, Color? bakground = null)
{
    public readonly Color Foreground = foreground;
    public readonly Color? Bakground = bakground;
}