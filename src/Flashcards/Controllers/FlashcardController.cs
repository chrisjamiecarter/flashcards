using Flashcards.Data.Managers;
using Flashcards.Models;

namespace Flashcards.Controllers;

public class FlashcardController
{
    #region Fields

    private readonly SqlDataManager _dataManager;

    #endregion
    #region Constructors

    public FlashcardController(string connectionString)
    {
        _dataManager = new SqlDataManager(connectionString);
    }

    #endregion
    #region Methods

    public void AddFlashcard(FlashcardDto flashcard)
    {
        _dataManager.AddFlashcard(flashcard.StackId, flashcard.Question, flashcard.Answer);
    }

    public void DeleteFlashcard(int id)
    {
        _dataManager.DeleteFlashcard(id);
    }

    public void DeleteFlashcard(FlashcardDto flashcard)
    {
        _dataManager.DeleteFlashcard(flashcard.Id);
    }

    public IReadOnlyList<FlashcardDto> GetFlashcards()
    {
        return _dataManager.GetFlashcards().Select(x => new FlashcardDto(x)).ToList();
    }

    public IReadOnlyList<FlashcardDto> GetFlashcards(int stackId)
    {
        return _dataManager.GetFlashcards(stackId).Select(x => new FlashcardDto(x)).ToList();
    }

    public void SetFlashcard(int id, string question, string answer)
    {
        _dataManager.SetFlashcard(id, question, answer);
    }

    public void SetFlashcard(FlashcardDto flashcard)
    {
        _dataManager.SetFlashcard(flashcard.Id, flashcard.Question, flashcard.Answer);
    }

    #endregion
}
