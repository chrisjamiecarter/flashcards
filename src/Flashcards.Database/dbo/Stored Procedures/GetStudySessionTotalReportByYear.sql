CREATE PROCEDURE [dbo].[GetStudySessionTotalReportByYear]

	 @Year	NVARCHAR(4)

AS
BEGIN

	SELECT
		*
	FROM
		[dbo].[vwStudySessionTotalReport]
	WHERE
		[StudyYear] = @Year
	ORDER BY
		[StackName]

END
GO
