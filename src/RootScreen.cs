using System.Diagnostics;
using LawGen.Core.Tiling;
using LawGen.Core.Tiling.Internals;
using LawGen.Information;
using LawGen.WFC;
using LawGen.WFC.Enum;
using RoguelikeWFC.Components;
using RoguelikeWFC.Components.Enums;
using RoguelikeWFC.Components.Models;
using RoguelikeWFC.Tiles;
using RoguelikeWFC.Tiles.Sprites;

namespace RoguelikeWFC;

internal class RootScreen : ScreenObject
{
    private ScreenSurface _world;
    private WorldGenerator worldGenerator { get; }
    private WorldMap? worldMap { get; set; }
    
    private readonly int _width = GameSettings.GAME_WIDTH;
    private readonly int _height = GameSettings.GAME_HEIGHT;
    
    private int worldWidth => _width - 35;
    private WorldGenerationMenu _menu;
    private SaveLoadMapControlersMenu _saveLoadMapControlersMenu;
    private bool _generationReady;
    private ExecutionMode _executionMode;
    private SelectedMap _selectedMap;
    
    private readonly Stopwatch _generationTime = new();
    
    public RootScreen()
    {
        worldGenerator = new(worldWidth, _height, PlainsTiles.Instance);
        _world = new(worldWidth, _height);
        _menu = new(30, _height, 
            "Controls", new(_world.AbsolutePosition.X + _world.Width + 2, 1),
            new(), onResetButtonClick: OnResetButtonClick);
        
        _saveLoadMapControlersMenu = new(_width, _height);
        _saveLoadMapControlersMenu.OnLoadMap += LoadWorldMap;
        
        Children.Add(_world);
        Children.Add(_menu);
        Children.Add(_saveLoadMapControlersMenu);
    }

    public override void Update(TimeSpan delta)
    {
        base.Update(delta);
        if(_generationReady)
        {
            if(_generationTime.IsRunning)
                _generationTime.Stop();
            return;
        }
        
        if(!_generationTime.IsRunning)
            _generationTime.Start();
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
        if(worldGenerator.generationStepState != GenerationStepState.Finished)
        {
            worldGenerator.Wfc();
            DrawnGenerationMap();
        }
        else
        {
            _generationReady = true;
            worldMap = worldGenerator.worldMap!;
            DrawnWorldMap();
            _saveLoadMapControlersMenu.MadeMapReadyToSave(worldMap);
        }
        UpdateInformations();
    }
    
    private void InterateInstant()
    {
        worldGenerator.Wfc(WfcCallKind.Complete);
        _generationReady = true;
        worldMap = worldGenerator.worldMap!;
        
        _saveLoadMapControlersMenu.MadeMapReadyToSave(worldMap);
        DrawnWorldMap();
        UpdateInformations();
    }
    
    private void LoadWorldMap(WorldMap map)
    {
        _generationReady = true;
        worldMap = map;
        _saveLoadMapControlersMenu.MadeMapReadyToSave(worldMap);
        DrawnWorldMap();
        UpdateInformations();
    }

    public void UpdateMapAtlas()
    {
        TileAtlas newAtlas = _selectedMap switch
        {
            SelectedMap.Plains => PlainsTiles.Instance,
            SelectedMap.Desert => DesertTiles.Instance,
            SelectedMap.Ocean => OceanAtlas.Instance,
            _ => throw new NotImplementedException()
        };
        
        worldGenerator.ChangeAtlasInstance(newAtlas);
    }
    
    private void DrawnWorldMap()
    {
        if(worldMap is null) return;
        for(int row = 0; row < _height; row++)
        {
            for(int col = 0; col < worldWidth; col++)
            {
                RenderTile(row: row, col: col, true);
            }
        }
    }
    private void DrawnGenerationMap()
    {
        for (int row = 0; row < _height; row++)
        {
            for (int col = 0; col < worldWidth; col++)
            {
                RenderTile(row: row, col: col);
            }
        }
    }
    
    private void UpdateInformations()
    {
        GenerationInformation mapInformation = worldGenerator.DumpGenerationInformation();
        ExecutionInformation executionInformation = new()
        {
            executionMode = _executionMode,
            selectedMap = _selectedMap,
            elapsedTime = _generationTime.Elapsed
        };
        _menu.currentControlInformation = new()
        {
            generationInformation = mapInformation,
            executionInformation = executionInformation
        };
    }
    
    private void OnResetButtonClick()
    {
        _executionMode = _menu.executionMode;
        _selectedMap = _menu.selectedMap;
        
        _generationTime.Reset();
        _saveLoadMapControlersMenu.Reset();
        UpdateMapAtlas();
        worldGenerator.ResetMap();
        _world.Clear();
        
        _generationReady = false;
    }

    private void RenderTile(int row, int col, bool fromWorldMap = false)
    {
        MapTile tileAtPossition = fromWorldMap ? worldMap!.tiles[row, col] : worldGenerator.GetTileAtPossition(row, col);
        if(tileAtPossition is SuperTile superTile)
        {
            if(superTile.inConflict)
                _world.SetGlyph(col, row, 63, Color.Red);
            else
            {
                char text = superTile.Entropy.Length.ToString()[0];
                _world.SetGlyph(col, row, text);
            }
            return;
        }
        RenderableMapTile tile = (RenderableMapTile)tileAtPossition;
        TileSpriteMetadata spriteMetadata = tile.SpriteMetadata;
        if(spriteMetadata.Background.HasValue)
            _world.SetGlyph(col, row, tile.GetSprite(fromWorldMap), spriteMetadata.Foreground, spriteMetadata.Background.Value);
        else
            _world.SetGlyph(col, row, tile.GetSprite(fromWorldMap), spriteMetadata.Foreground);
    }
}
