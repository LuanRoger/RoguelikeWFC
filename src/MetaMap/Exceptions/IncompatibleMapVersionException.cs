namespace RoguelikeWFC.MetaMap.Exceptions;

public class IncompatibleMapVersionException() : Exception(MESSAGE)
{
    private const string MESSAGE = "The map version is incompatible with the current version of the recoginizer.";
}