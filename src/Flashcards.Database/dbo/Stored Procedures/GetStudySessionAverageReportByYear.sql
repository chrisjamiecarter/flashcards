CREATE PROCEDURE [dbo].[GetStudySessionAverageReportByYear]

	 @Year	NVARCHAR(4)

AS
BEGIN

	SELECT
		*
	FROM
		[dbo].[vwStudySessionAverageReport]
	WHERE
		[StudyYear] = @Year
	ORDER BY
		[StackName]

END
GO
