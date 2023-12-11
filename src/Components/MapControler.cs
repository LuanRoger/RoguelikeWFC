using RoguelikeWFC.Components.Enums;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace RoguelikeWFC.Components;

public class MapControler : ControlsConsole
{
    private readonly ButtonBase _resetButton;
    private readonly ListBox _environmentsListBox;
    private readonly RadioButton _executionModeInteractive;
    private readonly RadioButton _executionModeInstant;
    
    public delegate void ExecutionModeChangedEventHandler(ExecutionMode newExecutionMode);
    public delegate void SelectedMapChangedEventHandler(SelectedMap newSelectedMap);
    public event ExecutionModeChangedEventHandler? OnExecutionModeChanged;
    public event SelectedMapChangedEventHandler? OnSelectedMapChanged;
    
    public MapControler(int width, int height, Point possition,
        Action? onResetButtonClick = null) : base(width, height)
    {
        Controls.ThemeColors = Colors.Default;
        Surface.DefaultBackground = Color.AnsiWhite;
        Position = possition;
        Surface.Clear();
        
        const int resetButtonWidth = 7;
        _resetButton = new Button(resetButtonWidth, 3)
        {
            Text = "Reset",
            Position = new(Width - resetButtonWidth, 2),
            IsVisible = true
        };
        if(onResetButtonClick is not null)
            _resetButton.Click += (_, _) => onResetButtonClick.Invoke();
        
        _executionModeInteractive = new(15, 1)
        {
            Text = "Interactive",
            Position = new(0, 0),
            IsSelected = true
        };
        const int executionModeInstantWidth = 11;
        _executionModeInstant = new(executionModeInstantWidth, 1)
        {
            Text = "Instant",
            Position = new(Width - executionModeInstantWidth, 0),
            IsSelected = false
        };
        _executionModeInteractive.IsSelectedChanged += (_, _) => 
            OnExecutionModeChanged?.Invoke(ExecutionMode.Interactive);
        _executionModeInstant.IsSelectedChanged += (_, _) =>
            OnExecutionModeChanged?.Invoke(ExecutionMode.Instant);
        Controls.Add(_executionModeInteractive);
        Controls.Add(_executionModeInstant);
        
        _environmentsListBox = new(6, 3)
        {
            Position = new(0, 2),
        };
        _environmentsListBox.Items.Add("Plains");
        _environmentsListBox.Items.Add("Ocean");
        _environmentsListBox.Items.Add("Desert");
        _environmentsListBox.SelectedIndex = 0;
        _environmentsListBox.SelectedItemChanged += EnvironmentsListBoxOnSelectedItemChanged;
        
        Controls.Add(_resetButton);
        Controls.Add(_environmentsListBox);
    }

    private void EnvironmentsListBoxOnSelectedItemChanged(object? sender, ListBox.SelectedItemEventArgs e)
    {
        SelectedMap newSelectedMap = _environmentsListBox.SelectedIndex switch
        {
            0 => SelectedMap.Plains,
            1 => SelectedMap.Ocean,
            2 => SelectedMap.Desert,
            _ => throw new ArgumentOutOfRangeException()
        };
        OnSelectedMapChanged?.Invoke(newSelectedMap);
    }
}