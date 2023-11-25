using RoguelikeWFC.Components.Models;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace RoguelikeWFC.Components;

public class InformationControllers : ControlsConsole
{
    private readonly Label _colapsedTilesLabel;
    private const string COLAPSED_TILES_LABEL = "Colapsed tiles: {0}";
    private readonly Label _conflictTilesLabel;
    private const string CONFLICT_TILES_LABEL = "Conflict tiles: {0}";
    private readonly Label _leftTilesToCollapseLabel;
    private const string LEFT_TILES_TO_COLLAPSE_LABEL = "Left tiles to collapse: {0}";
    private readonly Label _totalTilesLabel;
    private const string TOTAL_TILES_LABEL = "Total tiles: {0}";
    private readonly ProgressBar _generationProgress;
    
    public InformationControllers(int width, int height, Point possition, GenerationInformation information) : base(width, height)
    {
        Position = possition;
        Surface.Clear();
        
        _colapsedTilesLabel = new(Width)
        {
            DisplayText = string.Format(COLAPSED_TILES_LABEL, information.collapsedTiles),
            Position = new(0, 0)
        };
        _conflictTilesLabel = new(Width)
        {
            DisplayText = string.Format(CONFLICT_TILES_LABEL, information.conflictTiles),
            Position = new(0, 1)
        };
        _leftTilesToCollapseLabel = new(Width)
        {
            DisplayText = string.Format(LEFT_TILES_TO_COLLAPSE_LABEL, information.leftTilesToCollapse),
            Position = new(0, 2)
        };
        _totalTilesLabel = new(Width)
        {
            DisplayText = string.Format(TOTAL_TILES_LABEL, information.totalTiles),
            Position = new(0, 3)
        };
        _generationProgress = new(Width, 1, HorizontalAlignment.Left)
        {
            DisplayText = "Generating world...",
            Progress = information.progress,
            Position = new(0, 5)
        };
        
        Controls.Add(_colapsedTilesLabel);
        Controls.Add(_conflictTilesLabel);
        Controls.Add(_leftTilesToCollapseLabel);
        Controls.Add(_totalTilesLabel);
        Controls.Add(_generationProgress);
    }
    
    public void UpdateInformations(GenerationInformation newInformations)
    {
        _colapsedTilesLabel.DisplayText = string.Format(COLAPSED_TILES_LABEL, newInformations.collapsedTiles);
        _conflictTilesLabel.DisplayText = string.Format(CONFLICT_TILES_LABEL, newInformations.conflictTiles);
        _leftTilesToCollapseLabel.DisplayText = string.Format(LEFT_TILES_TO_COLLAPSE_LABEL, newInformations.leftTilesToCollapse);
        _totalTilesLabel.DisplayText = string.Format(TOTAL_TILES_LABEL, newInformations.totalTiles);
        _generationProgress.Progress = newInformations.progress;
    }
}