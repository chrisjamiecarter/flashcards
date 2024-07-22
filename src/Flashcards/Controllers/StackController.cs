using Flashcards.Data.Managers;
using Flashcards.Models;

namespace Flashcards.Controllers;

public class StackController
{
    #region Fields

    private readonly SqlDataManager _dataManager;

    #endregion
    #region Constructors

    public StackController(string connectionString)
    {
        _dataManager = new SqlDataManager(connectionString);
    }

    #endregion
    #region Methods

    public void AddStack(string name)
    {
        _dataManager.AddStack(name);
    }

    public void AddStack(StackDto stack)
    {
        _dataManager.AddStack(stack.Name);
    }

    public void DeleteStack(int id)
    {
        _dataManager.DeleteStack(id);
    }

    public void DeleteStack(StackDto stack)
    {
        _dataManager.DeleteStack(stack.Id);
    }

    public StackDto GetStack(string name)
    {
        return new StackDto(_dataManager.GetStack(name));
    }

    public IReadOnlyList<StackDto> GetStacks()
    {
        return _dataManager.GetStacks().Select(x => new StackDto(x)).ToList();
    }

    public void SetStack(int id, string name)
    {
        _dataManager.SetStack(id, name);
    }

    public void SetStack(StackDto stack)
    {
        _dataManager.SetStack(stack.Id, stack.Name);
    }

    #endregion
}
