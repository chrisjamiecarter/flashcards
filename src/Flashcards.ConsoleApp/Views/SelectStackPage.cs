using Flashcards.ConsoleApp.Models;
using Flashcards.ConsoleApp.Services;
using Flashcards.Models;
using Spectre.Console;

namespace Flashcards.ConsoleApp.Views;

/// <summary>
/// Page which allows users to select a stack to perform an action on.
/// </summary>
internal class SelectStackPage : BasePage
{
    #region Constants

    private const string PageTitle = "Select Stack";

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

    internal static StackDto? Show(IReadOnlyList<StackDto> stacks)
    {
        WriteHeader(PageTitle);

        var option = GetOption(stacks);
        
        return option.Id == 0 ? null : stacks.First(x => x.Id == option.Id);
    }

    #endregion
    #region Methods - Private

    private static SelectionChoice GetOption(IReadOnlyList<StackDto> stacks)
    {
        // Add the list to the existing PageChoices.
        IEnumerable<SelectionChoice> pageChoices = [.. stacks.Select(x => new SelectionChoice(x.Id, x.Name)), .. PageChoices];

        return UserInputService.GetSelectionChoice(PromptTitle, pageChoices);
    }

    #endregion
}
