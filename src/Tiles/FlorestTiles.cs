// ReSharper disable InconsistentNaming

using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public class GrassTile : MapTile
{
    public override int id => 1;
    public override char sprite => '.';
    public override Color color => Color.AnsiGreen;
    public override TileSocket tileSocket => 
        new()
        {
            fitTop = new[] { 1, 2, 4, 6, 7 },
            fitRight = new[] { 1, 2, 4, 6, 7 },
            fitBottom = new[] { 1, 2, 4, 6, 7 },
            fitLeft = new[] { 1, 2, 4, 6, 7 }
        };
}

public class TreeTile : MapTile
{
    public override int id => 2;
    public override char sprite => 'T';
    public override Color color => Color.Brown;
    public override TileSocket tileSocket => 
        new()
        {
            fitTop = new[] { 1, 2 },
            fitRight = new[] { 2, 6, 1, 7 },
            fitBottom = new[] { 1, 2 },
            fitLeft = new[] { 2, 6, 1, 7 }
        };
}

public class WaterTile : MapTile
{
    public override int id => 3;
    public override char sprite => '~';
    public override Color color => Color.AnsiBlue;
    public override TileSocket tileSocket => new()
    {
        fitTop = new[] { 3, 6, 7 },
        fitRight = new[] { 3, 6, 7 },
        fitBottom = new[] { 3, 6, 7 },
        fitLeft = new[] { 3, 6, 7 }
    };
}

public class MountainTile : MapTile
{
    public override int id => 4;
    public override char sprite => '^';
    public override Color color => Color.AnsiWhite;
    public override TileSocket tileSocket => new()
    {
        fitTop = new[] { 1, 4 },
        fitRight = new[] { 1, 4 },
        fitBottom = new[] { 1, 4 },
        fitLeft = new[] { 1, 4 }
    };
}

public class SandTile : MapTile
{
    public override int id => 6;
    public override char sprite => '.';
    public override Color color => Color.AnsiYellow;
    public override TileSocket tileSocket => new()
    {
        fitTop = new[] { 6, 1, 4 },
        fitRight = new[] { 6, 1, 4 },
        fitBottom = new[] { 6, 1, 4 },
        fitLeft = new[] { 6, 1, 4 }
    };
}

public class DirtTile : MapTile
{
    public override int id => 7;
    public override char sprite => '.';
    public override Color color => Color.SaddleBrown;
    public override TileSocket tileSocket => new()
    {
        fitTop = new[] { 7, 1, 2 },
        fitRight = new[] { 7, 1, 2 },
        fitBottom = new[] { 7, 1, 2 },
        fitLeft = new[] { 7, 1, 2 },
    };
}

public class FlorestTiles : TileSet
{
    private static FlorestTiles? _instance;

    public static FlorestTiles Instance
    {
        get
        {
            _instance ??= new();
            return _instance;
        }
    }

    private FlorestTiles() : 
        base(new MapTile[] {
        new GrassTile(),
        new TreeTile(),
        new WaterTile(),
        new MountainTile(),
        new DirtTile(),
        new SandTile(),
        }) { }

    public override int[] ValidInitialTiles() =>
        new[] { 1, 2, 3, 4 };
}