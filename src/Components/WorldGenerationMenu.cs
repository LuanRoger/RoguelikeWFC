using SadConsole.UI.Controls;
using SadConsole.UI;

namespace RoguelikeWFC.Components;

public class WorldGenerationMenu : ScreenObject
{
    private readonly string _title;
    private readonly Console _container;
    private readonly MapController _controllerMenu;

    public WorldGenerationMenu(int width, int height, string title, Point possition = new(), 
        Action? onResetButtonClick = null)
    {
        _title = title;
        _container = new(width, height - 3);
        _container.Position = possition;
        
        _container.Clear();
        Border.CreateForSurface(_container, title);
        
        _controllerMenu = new(width - 2, 10,
            onResetButtonClick: onResetButtonClick);
        _controllerMenu.Position = new(1, 1);
        _container.Children.Add(_controllerMenu);
        
        Children.Add(_container);
    }
}

public class MapController : ControlsConsole
{
    private readonly Button _resetButton;
    
    public MapController(int width, int height, 
        Action? onResetButtonClick = null) : base(width, height)
    {
        Controls.ThemeColors = Colors.Default;
        Surface.DefaultBackground = Color.AnsiWhite;
        Surface.DefaultForeground = Color.Black;
        Surface.Clear();
        
        _resetButton = new(7)
        {
            Text = "Reset",
            Position = new(0, 0),
            IsVisible = true
        };
        if(onResetButtonClick is not null)
            _resetButton.Click += (_, _) => onResetButtonClick.Invoke();
        
        Controls.Add(_resetButton);
    }
} 