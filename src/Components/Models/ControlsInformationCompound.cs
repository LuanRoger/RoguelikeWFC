namespace RoguelikeWFC.Components.Models;

public class ControlsInformationCompound
{
    public GenerationInformation generationInformation { get; init; } = new();
    public ExecutionInformation executionInformation { get; init; } = new();
}