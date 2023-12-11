using RoguelikeWFC.Components.Window;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace RoguelikeWFC.Components;

public class SaveLoadMapControler : ControlsConsole
{
    private readonly Button _saveButton;
    private readonly Button _loadButton;
    
    public delegate void SaveMapEventHandler(string filePath);
    public delegate void LoadMapEventHandler(string filePath);
    public event SaveMapEventHandler? OnSaveMap;
    public event LoadMapEventHandler? OnLoadMap;
    
    public SaveLoadMapControler(int width, int height) : base(width, height)
    {
        _saveButton = new("Save")
        {
            Position = new(0, 0)
        };
        _loadButton = new("Load")
        {
            Position = new(0, 1)
        };
        _saveButton.Click += OnSaveButtonClick;
        _loadButton.Click += LoadButtonOnClick;
        
        Controls.Add(_saveButton);
        Controls.Add(_loadButton);
    }
    
    private void LoadButtonOnClick(object? sender, EventArgs e)
    {
        SelectDirectoryPopup popup = new("Load map", ".wmap");
        popup.skipFileExistCheck = true;
        popup.selectButtonText = "Load";
        popup.Closed += (_, _) =>
        {
            if (!popup.DialogResult) return;
            
            OnLoadMap?.Invoke(popup.selectedFile);
        };
        popup.Show(true);
    }

    private void OnSaveButtonClick(object? sender, EventArgs e)
    {
        SelectDirectoryPopup popup = new("Save map", ".wmap");
        popup.skipFileExistCheck = true;
        popup.selectButtonText = "Save";
        popup.Closed += (_, _) =>
        {
            if (!popup.DialogResult) return;
            
            OnSaveMap?.Invoke(popup.selectedFile);
        };
        popup.Show(true);
    }
}