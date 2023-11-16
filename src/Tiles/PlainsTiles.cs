// ReSharper disable InconsistentNaming

using RoguelikeWFC.WFC;

namespace RoguelikeWFC.Tiles;

public class GrassTile : MapTile
{
    public GrassTile() : base(TileIDs.Grass, 249, Color.AnsiGreen,
        new()
        {
            fitTop = new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand, TileIDs.Dirt },
            fitRight = new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand, TileIDs.Dirt },
            fitBottom = new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand, TileIDs.Dirt },
            fitLeft = new byte[] { TileIDs.Grass, TileIDs.Tree, TileIDs.Mountain, TileIDs.Sand, TileIDs.Dirt }
        }) { }
}

public class TreeTile : MapTile
{
    public TreeTile() : base(TileIDs.Tree, 5, Color.Brown, 
        new()
        {
            fitTop = new byte[] { TileIDs.Tree, TileIDs.Grass },
            fitRight = new byte[] { TileIDs.Tree, TileIDs.Grass, TileIDs.Dirt },
            fitBottom = new byte[] { TileIDs.Tree, TileIDs.Grass },
            fitLeft = new byte[] { TileIDs.Tree, TileIDs.Grass, TileIDs.Dirt }
        }) { }
}

public class WaterTiles : TileSet
{
    public override byte tileIdGenerator => TileIDs.River;
    
    public WaterTiles() : base(new MapTile[]
    {
        new RiverTile(),
        new RiverTopBranchTile(),
        new RiverFromBottomToLeftCurveTile(),
        new RiverFromBottomToRightCurveTile(),
        new RiverRightBranch(),
        new RiverFromLeftToUpCurve(),
        new RiverFromLeftToBottomCurve(),
        new RiverBottomBranch(),
        new RiverFromUpToRightCurve(),
        new RiverFromUpToLeftCurve(),
        new RiverLeftBranch(),
        new RiverFromRightToUpCurve(),
        new RiverFromRightToBottomCurve()
    }) {}
    public class RiverTile : MapTile
    {
        public RiverTile() : base(TileIDs.River, 247, Color.AnsiBlue, 
            new()
            {
                fitTop = new byte[] { TileIDs.River, TileIDs.RiverTopBranch, TileIDs.RiverFromBottomToRightCurve, TileIDs.RiverFromBottomToLeftCurve, 
                    TileIDs.Sand, TileIDs.Dirt },
                fitRight = new byte[] { TileIDs.River, TileIDs.RiverRightBranch, TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromLeftToUpCurver,
                    TileIDs.Sand, TileIDs.Dirt },
                fitBottom = new byte[] { TileIDs.River, TileIDs.RiverBottomBranch, TileIDs.RiverFromUpToLeftCurve, TileIDs.RiverFromUpToRightCurve,
                    TileIDs.Sand, TileIDs.Dirt },
                fitLeft = new byte[] { TileIDs.River, TileIDs.RiverLeftBranch, TileIDs.RiverFromRightToUpCurve, TileIDs.RiverFromRightToBottomCurve,
                    TileIDs.Sand, TileIDs.Dirt }
            }) {}
    }
    public class RiverTopBranchTile : MapTile
    {
        public RiverTopBranchTile() : base(TileIDs.RiverTopBranch, 167, Color.AnsiBlue, 
            new()
            {
                fitTop = new byte[] { TileIDs.RiverTopBranch, TileIDs.River, TileIDs.RiverFromBottomToRightCurve, TileIDs.RiverFromBottomToLeftCurve },
                fitRight = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitBottom = new byte[] { TileIDs.RiverTopBranch, TileIDs.River },
                fitLeft = new byte[] { TileIDs.Dirt, TileIDs.Sand }
            }) {}
        
    }
    public class RiverFromBottomToLeftCurveTile : MapTile
    {
        public RiverFromBottomToLeftCurveTile() : base(TileIDs.RiverFromBottomToLeftCurve, 180, Color.AnsiBlue, 
            new()
            {
                fitTop = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitRight = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitBottom = new byte[] { TileIDs.River, TileIDs.RiverTopBranch },
                fitLeft = new byte[] { TileIDs.River, TileIDs.RiverRightBranch, TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromLeftToUpCurver }
            }) {}
    }
    public class RiverFromBottomToRightCurveTile : MapTile
    {
        public RiverFromBottomToRightCurveTile() : base(TileIDs.RiverFromBottomToRightCurve, 195, Color.AnsiBlue, 
            new()
            {
                fitTop = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitRight = new byte[] { TileIDs.River, TileIDs.RiverLeftBranch, TileIDs.RiverFromRightToBottomCurve, TileIDs.RiverFromRightToUpCurve },
                fitBottom = new byte[] { TileIDs.River, TileIDs.RiverTopBranch },
                fitLeft = new byte[] { TileIDs.Dirt, TileIDs.Sand }
            }) {}
    }
    public class RiverRightBranch : MapTile
    {
        public RiverRightBranch() : base(TileIDs.RiverRightBranch, 185, Color.AnsiBlue, 
            new()
            {
                fitTop = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitRight = new byte[] { TileIDs.RiverRightBranch, TileIDs.River, TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromLeftToUpCurver },
                fitLeft = new byte[] { TileIDs.RiverRightBranch, TileIDs.River },
                fitBottom = new byte[] { TileIDs.Dirt, TileIDs.Sand }
            }) {}
    }
    public class RiverFromLeftToUpCurve : MapTile
    {
            public RiverFromLeftToUpCurve() : base(TileIDs.RiverFromLeftToUpCurver, 192, Color.AnsiBlue, 
            new()
            {
                fitTop = new byte[] { TileIDs.River, TileIDs.RiverFromBottomToLeftCurve, TileIDs.RiverFromBottomToRightCurve, TileIDs.RiverTopBranch },
                fitRight = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitBottom = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitLeft = new byte[] { TileIDs.River, TileIDs.RiverRightBranch },
            }) {}
    }
    public class RiverFromLeftToBottomCurve : MapTile
    {
        public RiverFromLeftToBottomCurve() : base(TileIDs.RiverFromLeftToBottomCurver, 218, Color.AnsiBlue, 
            new()
            {
                fitTop = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitRight = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitBottom = new byte[] { TileIDs.River, TileIDs.RiverFromUpToLeftCurve, TileIDs.RiverFromUpToRightCurve, TileIDs.RiverBottomBranch },
                fitLeft = new byte[] { TileIDs.River, TileIDs.RiverRightBranch },
            }) {}
    }
    
    public class RiverBottomBranch : MapTile
    {
        public RiverBottomBranch() : base(TileIDs.RiverBottomBranch, 167, Color.AnsiBlue, 
            new()
            {
                fitTop = new byte[] { TileIDs.River, TileIDs.RiverBottomBranch, 
                    TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromRightToBottomCurve },
                fitRight = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitBottom = new byte[] { TileIDs.RiverBottomBranch, TileIDs.River, TileIDs.RiverFromUpToLeftCurve, TileIDs.RiverFromUpToRightCurve },
                fitLeft = new byte[] { TileIDs.Dirt, TileIDs.Sand }
            }) {}
    }
    public class RiverFromUpToRightCurve : MapTile
    {
        public RiverFromUpToRightCurve() : base(TileIDs.RiverFromUpToRightCurve, 217, Color.AnsiBlue, 
            new()
            {
                fitTop = new byte[] { TileIDs.River, TileIDs.RiverBottomBranch, TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromRightToBottomCurve },
                fitRight = new byte[] { TileIDs.River, TileIDs.RiverRightBranch, TileIDs.RiverFromRightToBottomCurve, TileIDs.RiverFromRightToUpCurve },
                fitBottom = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitLeft = new byte[] { TileIDs.Dirt, TileIDs.Sand }
            }) {}
    }
    public class RiverFromUpToLeftCurve : MapTile
    {
        public RiverFromUpToLeftCurve() : base(TileIDs.RiverFromUpToLeftCurve, 191, Color.AnsiBlue, 
            new()
            {
                fitTop = new byte[] { TileIDs.River, TileIDs.RiverBottomBranch, TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromRightToBottomCurve },
                fitRight = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitBottom = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitLeft = new byte[] { TileIDs.River, TileIDs.RiverLeftBranch, TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromLeftToUpCurver }
            }) {}
    }
    
    public class RiverLeftBranch : MapTile
    {
        public RiverLeftBranch() : base(TileIDs.RiverLeftBranch, 185, Color.AnsiBlue, 
            new()
            {
                fitTop = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitRight = new byte[] { TileIDs.RiverLeftBranch, TileIDs.River },
                fitBottom = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitLeft = new byte[] { TileIDs.RiverLeftBranch, TileIDs.River, TileIDs.RiverFromRightToUpCurve, TileIDs.RiverFromRightToBottomCurve }
            }) {}
    }
    public class RiverFromRightToUpCurve : MapTile
    {
        public RiverFromRightToUpCurve() : base(TileIDs.RiverFromRightToUpCurve, 192, Color.AnsiBlue, 
            new()
            {
                fitTop = new byte[] { TileIDs.River, TileIDs.RiverFromBottomToLeftCurve, TileIDs.RiverFromBottomToRightCurve, TileIDs.RiverTopBranch },
                fitRight = new byte[] { TileIDs.River, TileIDs.RiverRightBranch, TileIDs.RiverFromBottomToRightCurve, TileIDs.RiverFromUpToRightCurve },
                fitBottom = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitLeft = new byte[] { TileIDs.Dirt, TileIDs.Sand }
            }) {}
    }
    public class RiverFromRightToBottomCurve : MapTile
    {
        public RiverFromRightToBottomCurve() : base(TileIDs.RiverFromRightToBottomCurve, 218, Color.AnsiBlue, 
            new()
            {
                fitTop = new byte[] { TileIDs.Dirt, TileIDs.Sand },
                fitRight = new byte[] { TileIDs.River, TileIDs.RiverRightBranch, TileIDs.RiverFromBottomToRightCurve, TileIDs.RiverFromUpToRightCurve },
                fitBottom = new byte[] { TileIDs.River, TileIDs.RiverFromUpToLeftCurve, TileIDs.RiverFromUpToRightCurve, TileIDs.RiverBottomBranch },
                fitLeft = new byte[] { TileIDs.Dirt, TileIDs.Sand }
            }) {}
    }
}

public class MountainTile : MapTile
{
    public MountainTile() : base(TileIDs.Mountain, 30, Color.Gray,
        new()
        {
            fitTop = new byte[] { TileIDs.Mountain, TileIDs.MountainPeek,  TileIDs.Grass, TileIDs.Dirt, TileIDs.Sand },
            fitRight = new byte[] { TileIDs.Mountain, TileIDs.MountainPeek, TileIDs.Grass, TileIDs.Dirt, TileIDs.Sand },
            fitBottom = new byte[] { TileIDs.Mountain, TileIDs.MountainPeek, TileIDs.Grass, TileIDs.Dirt, TileIDs.Sand },
            fitLeft = new byte[] { TileIDs.Mountain, TileIDs.MountainPeek, TileIDs.Grass, TileIDs.Dirt, TileIDs.Sand }
        }) { }
}

public class MountainPeekTile : MapTile
{
    public MountainPeekTile() : base(TileIDs.MountainPeek, 30, Color.AnsiWhite,
        new()
        {
            fitTop = new byte[] { TileIDs.MountainPeek, TileIDs.Mountain },
            fitRight = new byte[] { TileIDs.MountainPeek, TileIDs.Mountain },
            fitBottom = new byte[] { TileIDs.MountainPeek, TileIDs.Mountain },
            fitLeft = new byte[] { TileIDs.MountainPeek, TileIDs.Mountain }
        }) { }

}

public class SandTile : MapTile
{
    public SandTile() : base(TileIDs.Sand, 249, Color.AnsiYellow, 
        new()
        {
            fitTop = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.River, TileIDs.RiverBottomBranch, TileIDs.RiverLeftBranch, TileIDs.RiverRightBranch, TileIDs.RiverTopBranch, 
                TileIDs.RiverFromBottomToLeftCurve, TileIDs.RiverFromBottomToRightCurve, TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromLeftToUpCurver, 
                TileIDs.RiverFromUpToLeftCurve, TileIDs.RiverFromUpToRightCurve, TileIDs.RiverFromRightToUpCurve, TileIDs.RiverFromRightToBottomCurve },
            fitRight = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.River, TileIDs.RiverBottomBranch, TileIDs.RiverLeftBranch, TileIDs.RiverRightBranch, TileIDs.RiverTopBranch, 
                TileIDs.RiverFromBottomToLeftCurve, TileIDs.RiverFromBottomToRightCurve, TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromLeftToUpCurver, 
                TileIDs.RiverFromUpToLeftCurve, TileIDs.RiverFromUpToRightCurve, TileIDs.RiverFromRightToUpCurve, TileIDs.RiverFromRightToBottomCurve },
            fitBottom = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.River, TileIDs.RiverBottomBranch, TileIDs.RiverLeftBranch, TileIDs.RiverRightBranch, TileIDs.RiverTopBranch, 
                TileIDs.RiverFromBottomToLeftCurve, TileIDs.RiverFromBottomToRightCurve, TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromLeftToUpCurver, 
                TileIDs.RiverFromUpToLeftCurve, TileIDs.RiverFromUpToRightCurve, TileIDs.RiverFromRightToUpCurve, TileIDs.RiverFromRightToBottomCurve },
            fitLeft = new byte[] { TileIDs.Sand, TileIDs.Grass, TileIDs.River, TileIDs.RiverBottomBranch, TileIDs.RiverLeftBranch, TileIDs.RiverRightBranch, TileIDs.RiverTopBranch, 
                TileIDs.RiverFromBottomToLeftCurve, TileIDs.RiverFromBottomToRightCurve, TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromLeftToUpCurver, 
                TileIDs.RiverFromUpToLeftCurve, TileIDs.RiverFromUpToRightCurve, TileIDs.RiverFromRightToUpCurve, TileIDs.RiverFromRightToBottomCurve }
        }) { }
}

public class DirtTile : MapTile
{
    public DirtTile() : base(TileIDs.Dirt, 249,  Color.SandyBrown, 
        new()
        {
            fitTop = new byte[] { TileIDs.Dirt, TileIDs.Grass, TileIDs.River, TileIDs.RiverBottomBranch, TileIDs.RiverLeftBranch, TileIDs.RiverRightBranch, TileIDs.RiverTopBranch, 
                TileIDs.RiverFromBottomToLeftCurve, TileIDs.RiverFromBottomToRightCurve, TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromLeftToUpCurver, 
                TileIDs.RiverFromUpToLeftCurve, TileIDs.RiverFromUpToRightCurve, TileIDs.RiverFromRightToUpCurve, TileIDs.RiverFromRightToBottomCurve },
            fitRight = new byte[] { TileIDs.Dirt, TileIDs.Grass, TileIDs.River, TileIDs.RiverBottomBranch, TileIDs.RiverLeftBranch, TileIDs.RiverRightBranch, TileIDs.RiverTopBranch, 
                TileIDs.RiverFromBottomToLeftCurve, TileIDs.RiverFromBottomToRightCurve, TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromLeftToUpCurver, 
                TileIDs.RiverFromUpToLeftCurve, TileIDs.RiverFromUpToRightCurve, TileIDs.RiverFromRightToUpCurve, TileIDs.RiverFromRightToBottomCurve },
            fitBottom = new byte[] { TileIDs.Dirt, TileIDs.Grass, TileIDs.River, TileIDs.RiverBottomBranch, TileIDs.RiverLeftBranch, TileIDs.RiverRightBranch, TileIDs.RiverTopBranch, 
                TileIDs.RiverFromBottomToLeftCurve, TileIDs.RiverFromBottomToRightCurve, TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromLeftToUpCurver, 
                TileIDs.RiverFromUpToLeftCurve, TileIDs.RiverFromUpToRightCurve, TileIDs.RiverFromRightToUpCurve, TileIDs.RiverFromRightToBottomCurve },
            fitLeft = new byte[] { TileIDs.Dirt, TileIDs.Grass, TileIDs.River, TileIDs.RiverBottomBranch, TileIDs.RiverLeftBranch, TileIDs.RiverRightBranch, TileIDs.RiverTopBranch, 
                TileIDs.RiverFromBottomToLeftCurve, TileIDs.RiverFromBottomToRightCurve, TileIDs.RiverFromLeftToBottomCurver, TileIDs.RiverFromLeftToUpCurver, 
                TileIDs.RiverFromUpToLeftCurve, TileIDs.RiverFromUpToRightCurve, TileIDs.RiverFromRightToUpCurve, TileIDs.RiverFromRightToBottomCurve },
        }) { }
}

public class PlainsTiles : TileAtlas
{
    private static PlainsTiles? _instance;

    public static PlainsTiles Instance
    {
        get
        {
            _instance ??= new();
            return _instance;
        }
    }

    private PlainsTiles() : 
        base(new MapTile[] {
        new GrassTile(),
        new TreeTile(),
        new MountainTile(),
        new MountainPeekTile(),
        new DirtTile(),
        new SandTile(),
        }, new TileSet[]
        {
            new WaterTiles()
        }) { }
}