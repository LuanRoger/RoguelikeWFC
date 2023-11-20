using RoguelikeWFC.Components;
using RoguelikeWFC.Tiles;
using RoguelikeWFC.WFC;

namespace RoguelikeWFC;

internal class RootScreen : ScreenObject
{
    private ScreenSurface _world;
    private WorldGenerator worldGenerator { get; }
    
    private readonly int _width = GameSettings.GAME_WIDTH;
    private readonly int _height = GameSettings.GAME_HEIGHT;
    
    private int worldWidth => _width - 35;
    private WorldGenerationMenu menu;
    private bool _generationReady;
    
    public RootScreen()
    {
        worldGenerator = new(worldWidth, _height, PlainsTiles.Instance);
        _world = new(worldWidth, _height);
        menu = new(30, _height, 
            "Controls", new(_world.AbsolutePosition.X + _world.Width + 2, 1),
            onResetButtonClick: () =>
            {
                worldGenerator.ResetMap();
                _generationReady = false;
            });
        
        Children.Add(_world);
        Children.Add(menu);
    }
    
    public override void Update(TimeSpan delta)
    {
        base.Update(delta);
        if(_generationReady)
            return;
        
        if(!worldGenerator.allCollapsed)
        {
            worldGenerator.InterateWfcOnce();
            DrawnMap();
        }
        else
        {
            DrawnWorldMap();
            _generationReady = true;
        }
    }
    
    private void DrawnMap()
    {
        for (int row = 0; row < _height; row++)
        {
            for (int col = 0; col < worldWidth; col++)
            {
                MapTile tile = worldGenerator.GetTileAtPossition(row, col);
                _world.SetGlyph(col, row, tile.sprite, tile.Color);
            }
        }
    }
    
    private void DrawnWorldMap()
    {
        WorldMap worldMap = worldGenerator.worldMap!;
        for(int row = 0; row < _height; row++)
        {
            for(int col = 0; col < worldWidth; col++)
            {
                MapTile tile = worldMap.tiles[row, col];
                _world.SetGlyph(col, row, tile.sprite, tile.Color);
            }
        }
    }
}
