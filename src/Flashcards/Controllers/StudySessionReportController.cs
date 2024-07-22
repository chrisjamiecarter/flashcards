using Flashcards.Data.Managers;
using Flashcards.Models;

namespace Flashcards.Controllers;

public class StudySessionReportController
{
    #region Fields

    private readonly SqlDataManager _dataManager;

    #endregion
    #region Constructors

    public StudySessionReportController(string connectionString)
    {
        _dataManager = new SqlDataManager(connectionString);
    }

    #endregion
    #region Methods

    public IReadOnlyList<StudySessionReportDto> GetStudySessionAverageReportByYear(DateTime dateTime)
    {
        return _dataManager.GetStudySessionAverageReportByYear(dateTime).Select(x => new StudySessionReportDto(x)).ToList();
    }

    public IReadOnlyList<StudySessionReportDto> GetStudySessionTotalReportByYear(DateTime dateTime)
    {
        return _dataManager.GetStudySessionTotalReportByYear(dateTime).Select(x => new StudySessionReportDto(x)).ToList();
    }

    #endregion
}
