using RoguelikeWFC.Tiles;
using RoguelikeWFC.WFC;

namespace RoguelikeWFC;

internal class RootScreen : ScreenObject
{
    private ScreenSurface _mainSurface;
    private BitMap map { get; }
    
    private readonly int width = GameSettings.GAME_WIDTH;
    private readonly int height = GameSettings.GAME_HEIGHT;
    
    private int interations;
    
    public RootScreen()
    {
        _mainSurface = new(width, height);
        Children.Add(_mainSurface);
        
        map = new(width, height, FlorestTiles.Instance);
        map.Init();
    }

    public override void Update(TimeSpan delta)
    {
        map.InterateWfcOnce();
        
        //Draw map
        for (int row = 0; row < height - 1; row++)
        {
            for (int col = 0; col < width - 1; col++)
            {
                MapTile tile = map.GetTileAtPossition(row, col);
                _mainSurface.SetGlyph(col, row, tile.glyph, tile.color);
            }
        }
        
        interations++;
    }
}
