namespace Flashcards.ConsoleApp.Models;

/// <summary>
/// Used to display a menu choice that requires both a hidden ID and visible Name value.
/// </summary>
internal class SelectionChoice
{
    #region Constructors

    public SelectionChoice(int id, string name)
    {
        Id = id;
        Name = name;
    }

    #endregion
    #region Properties

    internal int Id { get; init; }

    internal string? Name { get; init; }

    #endregion
}
