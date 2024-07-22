using Flashcards.Data.Managers;
using Flashcards.Models;

namespace Flashcards.Controllers;

public class StackController
{
    #region Fields

    private readonly SqlDataManager _dataManager;

    #endregion
    #region Constructors

    public StackController(SqlDataManager dataManager)
    {
        _dataManager = dataManager;
    }

    #endregion
    #region Methods

    public void AddStack(StackDto stack)
    {
        _dataManager.AddStack(stack.Name);
    }

    public void DeleteStack(StackDto stack)
    {
        _dataManager.DeleteStack(stack.Id);
    }

    public IReadOnlyList<StackDto> GetStacks()
    {
        return _dataManager.GetStacks().Select(x => new StackDto(x)).ToList();
    }

    public void SetStack(StackDto stack)
    {
        _dataManager.SetStack(stack.Id, stack.Name);
    }

    #endregion
}
