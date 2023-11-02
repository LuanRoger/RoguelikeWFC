namespace RoguelikeWFC.WFC;

public class GrassTile : MapTile
{
    public override int id => 1;
    public override char sprite => '.';
    public override Color color => Color.AnsiGreen;
    public override TileSocket tileSocket => 
        new(new [] { 1, 2, 3, 4 });
}

public class TreeTile : MapTile
{
    public override int id => 2;
    public override char sprite => 'T';
    public override Color color => Color.Brown;
    public override TileSocket tileSocket => 
        new(new[] { 1, 2 });
}

public class WaterTile : MapTile
{
    public override int id => 3;
    public override char sprite => '~';
    public override Color color => Color.AnsiBlue;
    public override TileSocket tileSocket => 
        new(new[] { 1, 3 });
}

public class MountainTile : MapTile
{
    public override int id => 4;
    public override char sprite => '^';
    public override Color color => Color.AnsiWhite;
    public override TileSocket tileSocket => new(new[] { 4, 1 });
}

public class NullTile : MapTile
{
    public override int id => -1;
    public override char sprite => '?';
    public override Color color => Color.Red;
    public override TileSocket tileSocket => new(Array.Empty<int>());
}

public class TextTile : MapTile
{
    public override int id => -2;
    public override char sprite { get; }
    public override Color color => Color.LightGray;
    public override TileSocket tileSocket => new(Array.Empty<int>());

    public TextTile(char text)
    {
        sprite = text;
    }
}

public static class FlorestTiles
{
    public static readonly MapTile NullTile = new NullTile();
    public static readonly MapTile[] Tiles = {
        new GrassTile(),
        new TreeTile(),
        new WaterTile(),
        new MountainTile()
    };
}