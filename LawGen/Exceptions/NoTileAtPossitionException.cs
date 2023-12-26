namespace LawGen.Exceptions;

public class NoTileAtPossitionException(int row, int col) : Exception(string.Format(MESSAGE, row, col))
{
    private const string MESSAGE = "There is no tile at the given possition (Row: {0}, Column: {1}).";
}