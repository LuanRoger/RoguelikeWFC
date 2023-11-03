using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public class NullTile : MapTile
{
    public NullTile() : base(TileIDs.Null, 63, Color.Red, TileSocket.Empty)
    { }
}

public class TextTile : MapTile
{
    public TextTile(char text) : base(TileIDs.Text, text, Color.LightGray, TileSocket.Empty)
    { }
}