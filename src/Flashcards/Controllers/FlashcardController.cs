using Flashcards.Data.Managers;
using Flashcards.Models;

namespace Flashcards.Controllers;

public class FlashcardController
{
    #region Fields

    private readonly SqlDataManager _dataManager;

    #endregion
    #region Constructors

    public FlashcardController(SqlDataManager dataManager)
    {
        _dataManager = dataManager;
    }

    #endregion
    #region Methods

    public void AddFlashcard(FlashcardDto flashcard)
    {
        _dataManager.AddFlashcard(flashcard.StackId, flashcard.Question, flashcard.Answer);
    }

    public void DeleteFlashcard(FlashcardDto flashcard)
    {
        _dataManager.DeleteFlashcard(flashcard.Id);
    }

    public void SetFlashcard(FlashcardDto flashcard)
    {
        _dataManager.SetFlashcard(flashcard.Id, flashcard.Question, flashcard.Answer);
    }

    #endregion
}
