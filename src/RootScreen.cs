using RoguelikeWFC.Components;
using RoguelikeWFC.Components.Models;
using RoguelikeWFC.Enums;
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
    private WorldGenerationMenu _menu;
    private bool _generationReady;
    private ExecutionMode _executionMode;
    
    public RootScreen()
    {
        worldGenerator = new(worldWidth, _height, PlainsTiles.Instance);
        _world = new(worldWidth, _height);
        _menu = new(30, _height, 
            "Controls", new(_world.AbsolutePosition.X + _world.Width + 2, 1),
            new(), onResetButtonClick: OnResetButtonClick);
        
        Children.Add(_world);
        Children.Add(_menu);
    }

    public override void Update(TimeSpan delta)
    {
        base.Update(delta);
        if(_generationReady)
            return;

        switch (_executionMode)
        {
            case ExecutionMode.Interactive:
                InterateInteractive();
                break;
            case ExecutionMode.Instant:
                InterateInstant();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void InterateInteractive()
    {
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
        UpdateInformations();
    }
    
    private void InterateInstant()
    {
        worldGenerator.Wfc();
        DrawnWorldMap();
        _generationReady = true;
    }
    
    private void DrawnMap()
    {
        for (int row = 0; row < _height; row++)
        {
            for (int col = 0; col < worldWidth; col++)
            {
                MapTile tile = worldGenerator.GetTileAtPossition(row, col);
                _world.SetGlyph(col, row, tile.GetSprite(), tile.Color);
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
                _world.SetGlyph(col, row, tile.GetSprite(true), tile.Color);
            }
        }
    }
    private void UpdateInformations()
    {
        GenerationInformation mapInformation = worldGenerator.DumpGenerationInformation();
        _menu.currentInformation = mapInformation;
    }
    
    private void OnResetButtonClick()
    {
        worldGenerator.ResetMap();
        _executionMode = _menu.executionMode;
        _generationReady = false;
    }
}
