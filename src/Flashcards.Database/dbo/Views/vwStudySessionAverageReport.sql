CREATE VIEW [dbo].[vwStudySessionAverageReport] 
AS
	SELECT
		 [StackName]
		,[StudyYear]
		,[January]
		,[February]
		,[March]
		,[April]
		,[May]
		,[June]
		,[July]
		,[August]
		,[September]
		,[October]
		,[November]
		,[December]
	FROM
	(
		SELECT
			 st.[Name] AS [StackName]
			,DATENAME(year, ss.[DateTime]) AS [StudyYear]
			,DATENAME(month, ss.[DateTime]) AS [StudyMonth]
			,ss.[Score]
		FROM
			[Stack]			AS st JOIN
			[StudySession]	AS ss ON st.[Id] = ss.[StackId]
	) AS s
	PIVOT
	(
		AVG([Score])
		FOR [StudyMonth] IN ([January], [February], [March], [April], [May], [June], [July], [August], [September], [October], [November], [December])
	) AS p
GO
