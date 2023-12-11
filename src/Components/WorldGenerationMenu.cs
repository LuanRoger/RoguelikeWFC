using RoguelikeWFC.Components.Enums;
using RoguelikeWFC.Components.Models;
using SadConsole.UI;

namespace RoguelikeWFC.Components;

public class WorldGenerationMenu : ScreenObject
{
    private readonly string _title;
    private readonly Console _container;
    private readonly MapControler _controlerMenu;
    private readonly InformationContainer _informationContainer;
    private ControlsInformationCompound _controlsControlInformationCompound;
    
    public ControlsInformationCompound currentControlInformation
    {
        get => _controlsControlInformationCompound;
        set
        {
            _controlsControlInformationCompound = value;
            _informationContainer.UpdateInformations(value);
        }
    }

    public ExecutionMode executionMode { get; private set; }
    public SelectedMap selectedMap { get; private set; }

    public WorldGenerationMenu(int width, int height, string title, Point possition,  
        ControlsInformationCompound controlsControlInformationCompound, Action? onResetButtonClick = null)
    {
        _title = title;
        _container = new(width, height - 3)
        {
            Position = possition
        };
        _controlsControlInformationCompound = controlsControlInformationCompound;
        
        Border.CreateForSurface(_container, title);
        
        _controlerMenu = new(_container.Width, 6, new(0, 1),
            onResetButtonClick: onResetButtonClick);
        _controlerMenu.OnExecutionModeChanged += mode => executionMode = mode;
        _controlerMenu.OnSelectedMapChanged += map => selectedMap = map;
        _container.Children.Add(_controlerMenu);
        
        _informationContainer = new(_container.Width, 15, "Informations",
            new(0, _controlerMenu.Height + 1), _controlsControlInformationCompound);
        _container.Children.Add(_informationContainer);
        
        Children.Add(_container);
    }
}