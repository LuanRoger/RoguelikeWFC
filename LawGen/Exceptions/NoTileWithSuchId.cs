namespace LawGen.Exceptions;

public class NoTileWithSuchId(byte tileId) : Exception(string.Format(MESSAGE, tileId))
{
    private const string MESSAGE = "There is no tile with the given ID ({0}) on the atlas.";
}