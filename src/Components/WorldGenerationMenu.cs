using RoguelikeWFC.Components.Models;
using RoguelikeWFC.Enums;
using SadConsole.UI;

namespace RoguelikeWFC.Components;

public class WorldGenerationMenu : ScreenObject
{
    private readonly string _title;
    private readonly Console _container;
    private readonly MapController _controllerMenu;
    private readonly InformationContainer _informationContainer;
    private GenerationInformation _generationInformation;
    public GenerationInformation currentInformation
    {
        get => _generationInformation;
        set
        {
            _generationInformation = value;
            _informationContainer.UpdateInformations(value);
        }
    }

    public ExecutionMode executionMode { get; private set; }
    public SelectedMap selectedMap { get; private set; }

    public WorldGenerationMenu(int width, int height, string title, Point possition,  
        GenerationInformation generationInformation, Action? onResetButtonClick = null)
    {
        _title = title;
        _container = new(width, height - 3);
        _container.Position = possition;
        _generationInformation = generationInformation;
        
        _container.Clear();
        Border.CreateForSurface(_container, title);
        
        _controllerMenu = new(_container.Width, 6, new(0, 1),
            onResetButtonClick: onResetButtonClick);
        _controllerMenu.OnExecutionModeChanged += mode => executionMode = mode;
        _controllerMenu.OnSelectedMapChanged += map => selectedMap = map;
        _container.Children.Add(_controllerMenu);
        
        _informationContainer = new(_container.Width, 8, "Information",
            new(0, _controllerMenu.Height + 1), _generationInformation);
        _container.Children.Add(_informationContainer);
        
        Children.Add(_container);
    }
}