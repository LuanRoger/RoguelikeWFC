using RoguelikeWFC.Components.Models;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace RoguelikeWFC.Components;

public class InformationControlers : ControlsConsole
{
    private readonly Label _selectedMapLabel;
    private const string SELECTED_MAP_LABEL = "Selected map: {0}";
    private readonly Label _executionModeLabel;
    private const string EXECUTION_MODE_LABEL = "Execution mode: {0}";
    private readonly Label _colapsedTilesLabel;
    private const string COLAPSED_TILES_LABEL = "Colapsed tiles: {0}";
    private readonly Label _conflictTilesLabel;
    private const string CONFLICT_TILES_LABEL = "Conflict tiles: {0}";
    private readonly Label _leftTilesToCollapseLabel;
    private const string LEFT_TILES_TO_COLLAPSE_LABEL = "Left tiles to collapse: {0}";
    private readonly Label _totalTilesLabel;
    private const string TOTAL_TILES_LABEL = "Total tiles: {0}";
    private readonly ProgressBar _generationProgress;
    private readonly Label _generationTimeLabel;
    
    private ControlsInformationCompound informations;
    private GenerationInformation generationInformation => informations.generationInformation;
    private ExecutionInformation executionInformation => informations.executionInformation;
    
    public InformationControlers(int width, int height, Point possition, ControlsInformationCompound informations) : base(width, height)
    {
        this.informations = informations;
        Position = possition;
        Surface.Clear();
        
        _selectedMapLabel = new(Width)
        {
            DisplayText = string.Format(SELECTED_MAP_LABEL, executionInformation.selectedMap),
            Position = new(0, 0)
        };
        _executionModeLabel = new(Width)
        {
            DisplayText = string.Format(EXECUTION_MODE_LABEL, executionInformation.executionMode),
            Position = new(0, 1)
        };
        _colapsedTilesLabel = new(Width)
        {
            DisplayText = string.Format(COLAPSED_TILES_LABEL, generationInformation.collapsedTiles),
            Position = new(0, 2)
        };
        _conflictTilesLabel = new(Width)
        {
            DisplayText = string.Format(CONFLICT_TILES_LABEL, generationInformation.conflictTiles),
            Position = new(0, 3)
        };
        _leftTilesToCollapseLabel = new(Width)
        {
            DisplayText = string.Format(LEFT_TILES_TO_COLLAPSE_LABEL, generationInformation.leftTilesToCollapse),
            Position = new(0, 4)
        };
        _totalTilesLabel = new(Width)
        {
            DisplayText = string.Format(TOTAL_TILES_LABEL, generationInformation.totalTiles),
            Position = new(0, 5)
        };
        _generationProgress = new(Width, 3, HorizontalAlignment.Left)
        {
            DisplayText = "Generating world...",
            Progress = generationInformation.progress,
            Position = new(0, 6)
        };
        string elapsedTimeText = executionInformation.elapsedTime.ToString();
        _generationTimeLabel = new(Width)
        {
            DisplayText = elapsedTimeText,
            Position = new(Width - elapsedTimeText.Length, 9)
        };
        
        Controls.Add(_selectedMapLabel);
        Controls.Add(_executionModeLabel);
        Controls.Add(_colapsedTilesLabel);
        Controls.Add(_conflictTilesLabel);
        Controls.Add(_leftTilesToCollapseLabel);
        Controls.Add(_totalTilesLabel);
        Controls.Add(_generationProgress);
        Controls.Add(_generationTimeLabel);
    }
    
    public void UpdateInformations(ControlsInformationCompound newInformations)
    {
        informations = newInformations;
        
        _selectedMapLabel.DisplayText = string.Format(SELECTED_MAP_LABEL, executionInformation.selectedMap);
        _executionModeLabel.DisplayText = string.Format(EXECUTION_MODE_LABEL, executionInformation.executionMode);
        _colapsedTilesLabel.DisplayText = string.Format(COLAPSED_TILES_LABEL, generationInformation.collapsedTiles);
        _conflictTilesLabel.DisplayText = string.Format(CONFLICT_TILES_LABEL, generationInformation.conflictTiles);
        _leftTilesToCollapseLabel.DisplayText = string.Format(LEFT_TILES_TO_COLLAPSE_LABEL, generationInformation.leftTilesToCollapse);
        _totalTilesLabel.DisplayText = string.Format(TOTAL_TILES_LABEL, generationInformation.totalTiles);
        _generationProgress.Progress = generationInformation.progress;
        _generationTimeLabel.DisplayText = executionInformation.elapsedTime.ToString();
    }
}