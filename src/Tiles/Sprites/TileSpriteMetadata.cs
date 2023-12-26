namespace RoguelikeWFC.Tiles.Sprites;

public readonly struct TileSpriteMetadata(Color foreground, Color? background = null)
{
    public readonly Color Foreground = foreground;
    public readonly Color? Background = background;
}