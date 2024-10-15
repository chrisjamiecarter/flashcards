using Flashcards.ConsoleApp.Enums;
using Flashcards.ConsoleApp.Models;
using Flashcards.ConsoleApp.Services;
using Flashcards.Controllers;
using Flashcards.Models;
using Spectre.Console;

namespace Flashcards.ConsoleApp.Views;

/// <summary>
/// Page which allows users to manage a stack.
/// </summary>
internal class ManageStacksPage : BasePage
{
    #region Constants

    private const string PageTitle = "Manage Stacks";

    #endregion
    #region Fields

    private readonly StackController _stackController;
    
    #endregion
    #region Constructors

    public ManageStacksPage(StackController stackController)
    {
        _stackController = stackController;
    }

    #endregion
    #region Properties
    
    internal static IEnumerable<SelectionChoice> PageChoices
    {
        get
        {
            return
            [
                new(1, "Add stack"),
                new(2, "Update stack"),
                new(3, "Delete stack"),
                new(0, "Close page")
            ];
        }
    }

    #endregion
    #region Methods - Internal

    internal void Show()
    {
        var status = PageStatus.Opened;

        while (status != PageStatus.Closed)
        {
            WriteHeader(PageTitle);

            var choice = UserInputService.GetSelectionChoice(PromptTitle, PageChoices);
            status = PerformOption(choice);
        }
    }

    #endregion
    #region Methods - Private

    private SelectionChoice PerformOption(SelectionChoice option)
    {
        switch (option.Id)
        {
            case 1:

                // Add stack.
                AddStack();
                break;

            case 2:

                // Update stack.
                UpdateStack();
                break;

            case 3:

                // Delete stack.
                DeleteStack();
                break;

            default:

                // Close page.
                return PageStatus.Closed;
        }

        return PageStatus.Opened;
    }

    private void AddStack()
    {
        // Get raw data.
        var stacks = _stackController.GetStacks();
                
        // Get new stack.
        StackDto? stack = CreateStackPage.Show();
        if (stack == null)
        {
            return;
        }

        // Stack name must be unique.
        if (stacks.Select(x => x.Name).Contains(stack.Name))
        {
            MessagePage.Show("Error", $"A stack with the name {stack.Name} already exists.");
            return;
        }

        // Commit to database.
        _stackController.AddStack(stack.Name);

        // Show message.
        MessagePage.Show("Create Stack", "Stack created successfully.");
    }

    private void UpdateStack()
    {
        // Get raw data.
        var stacks = _stackController.GetStacks();

        // Get stack to manage, or stop.
        StackDto? stack = SelectStackPage.Show(stacks);
        if (stack == null)
        {
            return;
        }

        // Get updated stack.
        StackDto? updatedStack = UpdateStackPage.Show(stack);
        if (updatedStack == null)
        {
            return;
        }

        // Stack name must be unique.
        if (stacks.Select(x => x.Name).Contains(updatedStack.Name))
        {
            MessagePage.Show("Error", $"A stack with the name {updatedStack.Name} already exists.");
            return;
        }

        // Commit to database.
        _stackController.SetStack(stack.Id, updatedStack.Name);

        // Show message.
        MessagePage.Show("Update Stack", "Stack updated successfully.");
    }

    private void DeleteStack()
    {
        // Get raw data.
        var stacks = _stackController.GetStacks();

        // Get stack to manage, or stop.
        StackDto? stack = SelectStackPage.Show(stacks);
        if (stack == null)
        {
            return;
        }

        // Commit to database.
        _stackController.DeleteStack(stack.Id);

        // Show message.
        MessagePage.Show("Delete Stack", "Stack deleted successfully.");
    }

    #endregion
}
