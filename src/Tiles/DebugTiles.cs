using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public class NullTile() : MapTile(TileIDs.Null, 63, Color.Red, TileSocket.Empty);

public class TextTile(char text) : MapTile(TileIDs.Text, text, Color.LightGray, TileSocket.Empty);