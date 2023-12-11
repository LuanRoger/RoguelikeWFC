namespace RoguelikeWFC.MetaMap.Exceptions;

public class IncompatibleMapVersionException : Exception 
{
    private const string MESSAGE = "The map version is incompatible with the current version of the recoginizer.";
    
    public IncompatibleMapVersionException() : base(MESSAGE) { }
}