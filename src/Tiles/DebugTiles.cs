using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public class NullTile : MapTile
{
    public override int id => -1;
    public override char sprite => '?';
    public override Color color => Color.Red;
    public override TileSocket tileSocket => TileSocket.Empty;
}

public class TextTile : MapTile
{
    public override int id => -2;
    public override char sprite { get; }
    public override Color color => Color.LightGray;
    public override TileSocket tileSocket => TileSocket.Empty;

    public TextTile(char text)
    {
        sprite = text;
    }
}