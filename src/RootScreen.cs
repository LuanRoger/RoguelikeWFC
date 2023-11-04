using RoguelikeWFC.Tiles;
using RoguelikeWFC.WFC;

namespace RoguelikeWFC;

internal class RootScreen : ScreenObject
{
    private ScreenSurface _mainSurface;
    private WorldGenerator worldGenerator { get; }
    
    private readonly int _width = GameSettings.GAME_WIDTH;
    private readonly int _height = GameSettings.GAME_HEIGHT;
    
    public RootScreen()
    {
        _mainSurface = new(_width, _height);
        Children.Add(_mainSurface);
        
        
        worldGenerator = new(_width, _height, FlorestTiles.Instance);
    }

    public override void Update(TimeSpan delta)
    {
        worldGenerator.InterateWfcOnce();
        
        //Draw map
        for (int row = 0; row < _height - 1; row++)
        {
            for (int col = 0; col < _width - 1; col++)
            {
                MapTile tile = worldGenerator.GetTileAtPossition(row, col);
                _mainSurface.SetGlyph(col, row, tile.glyph, tile.color);
            }
        }
    }
}
