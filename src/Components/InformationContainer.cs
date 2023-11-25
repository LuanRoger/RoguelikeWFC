using RoguelikeWFC.Components.Models;

namespace RoguelikeWFC.Components;

public class InformationContainer : ScreenObject
{
    private readonly Console container;
    private readonly InformationControllers _informationControllers;
    
    public InformationContainer(int width, int height, string title, Point possition,
        GenerationInformation information)
    {
        container = new(width, height);
        container.Position = possition;
        container.Surface.DefaultBackground = Color.AnsiCyan;
        container.Clear();
        
        container.Print(container.Width / 2 - title.Length / 2, 0, title);
        
        _informationControllers = new(container.Width, container.Width - 1, new(0, 1),
            information);
        container.Children.Add(_informationControllers);
        
        Children.Add(container);
    }
    
    public void UpdateInformations(GenerationInformation newInformations)
    {
        _informationControllers.UpdateInformations(newInformations);
    }
}