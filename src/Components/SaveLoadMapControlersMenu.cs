using LawGen.WFC;
using RoguelikeWFC.MapIO;
using RoguelikeWFC.MapIO.Models;
using RoguelikeWFC.MetaMap;
using SadConsole.UI;
using static SadConsole.UI.Window;

namespace RoguelikeWFC.Components;

public class SaveLoadMapControlersMenu : ScreenObject
{
    private readonly Console _container;
    private readonly SaveLoadMapControler _saveLoadMapControler;
    private WorldMap? possibleMapToSave { get; set; }
    
    public delegate void OnLoadMapEventHandler(WorldMap map);
    public event OnLoadMapEventHandler? OnLoadMap;
    
    public SaveLoadMapControlersMenu(int screenWidth, int screenHeight)
    {
        _container = new(8, 2)
        {
            Position = new(screenWidth - 14, screenHeight - 6)
        };
        Border.CreateForSurface(_container, "File");
        
        _saveLoadMapControler = new(8, 2);
        _saveLoadMapControler.OnSaveMap += SaveLoadMapControlerOnOnSaveMap;
        _saveLoadMapControler.OnLoadMap += SaveLoadMapControlerOnOnLoadMap;
        _container.Children.Add(_saveLoadMapControler);
        
        Children.Add(_container);
    }

    private void SaveLoadMapControlerOnOnSaveMap(string filepath)
    {
        if(possibleMapToSave is null) return;
        
        MapSaver mapSaver = new(possibleMapToSave, filepath);
        mapSaver.Save();
    }
    
    private void SaveLoadMapControlerOnOnLoadMap(string filepath)
    {
        MapLoader mapLoader = new(filepath);
        SerializebleWorldMap? result = mapLoader.Load();
        if(result is null)
        {
            Message("Can't load map from file", "Ok");
            return;
        }
        
        MetaMapRecognize metaMapRecognize = new(result);
        WorldMap map = metaMapRecognize.Recognize();
        OnLoadMap?.Invoke(map);
    }
    
    public void MadeMapReadyToSave(WorldMap map)
    {
        possibleMapToSave = map;
    }
    
    public void Reset()
    {
        possibleMapToSave = null;
    }
}