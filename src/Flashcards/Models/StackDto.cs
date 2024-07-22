using Flashcards.Data.Entities;

namespace Flashcards.Models;

public class StackDto
{
    #region Constructors

    /// <summary>
    /// Maps a Stack Entity to a Stack DTO.
    /// </summary>
    /// <param name="entity">The Stack Entity to map.</param>
    public StackDto(StackEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
    }

    #endregion
    #region Properties

    public int Id { get; init; }

    public string Name { get; init; }

    #endregion
}
