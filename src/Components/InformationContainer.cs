using RoguelikeWFC.Components.Models;

namespace RoguelikeWFC.Components;

public class InformationContainer : ScreenObject
{
    private readonly Console container;
    private readonly InformationControlers _informationControlers;
    
    public InformationContainer(int width, int height, string title, Point possition,
        ControlsInformationCompound information)
    {
        container = new(width, height);
        container.Position = possition;
        container.Surface.DefaultBackground = Color.AnsiCyan;
        container.Clear();
        
        container.Print(container.Width / 2 - title.Length / 2, 0, title);
        
        _informationControlers = new(container.Width, container.Height - 1, new(0, 1),
            information);
        container.Children.Add(_informationControlers);
        
        Children.Add(container);
    }
    
    public void UpdateInformations(ControlsInformationCompound newInformations)
    {
        _informationControlers.UpdateInformations(newInformations);
    }
}