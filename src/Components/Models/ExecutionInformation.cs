using RoguelikeWFC.Enums;

namespace RoguelikeWFC.Components.Models;

public record ExecutionInformation
{
    public ExecutionMode executionMode { get; init; }
    public SelectedMap selectedMap { get; init; }
    public TimeSpan elapsedTime { get; init; }
}