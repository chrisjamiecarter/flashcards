using Flashcards.ConsoleApp.Models;
using Flashcards.ConsoleApp.Services;
using Flashcards.Models;
using Spectre.Console;

namespace Flashcards.ConsoleApp.Views;

/// <summary>
/// Page which allows users to select a flashcard to perform an action on.
/// </summary>
internal class SelectFlashcardPage : BasePage
{
    #region Constants

    private const string PageTitle = "Select Flashcard";

    #endregion
    #region Properties

    internal static IEnumerable<SelectionChoice> PageChoices
    {
        get
        {
            return
            [
                new(0, "Close page"),
            ];
        }
    }

    #endregion
    #region Methods - Internal

    internal static FlashcardDto? Show(IReadOnlyList<FlashcardDto> flashcards)
    {
        WriteHeader(PageTitle);

        var option = GetOption(flashcards);
        
        return option.Id == 0 ? null : flashcards.First(x => x.Id == option.Id);
    }

    #endregion
    #region Methods - Private

    private static SelectionChoice GetOption(IReadOnlyList<FlashcardDto> flashcards)
    {
        // Add the list to the existing PageChoices.
        IEnumerable<SelectionChoice> pageChoices = [.. flashcards.Select(x => new SelectionChoice(x.Id, $"{x.Question} = {x.Answer}")), .. PageChoices];

        return UserInputService.GetSelectionChoice(PromptTitle, pageChoices);
    }

    #endregion
}
