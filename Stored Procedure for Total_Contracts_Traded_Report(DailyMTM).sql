SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Caleb Sewcharran 
-- Create date: 30 January 2022
-- Description:	Stored procedure created to gather the total reports based upon the date range specified
-- =============================================

/*
	EXEC [dbo].[sp_Total_Contracts_Traded_Report] @DateFrom = N'2021-01-04',@DateTo = N'2021-01-05'
*/

CREATE PROCEDURE [dbo].[sp_Total_Contracts_Traded_Report]
--DECLARE
	@DateFrom DATETIME = N'2021-01-04',
	@DateTo DATETIME = N'2021-01-05'
AS
BEGIN
	SELECT DISTINCT 
	DM.[FileDate], 
	DM.Contract,
	(SELECT SUM(ContractsTraded) FROM [dbo].[DailyMTM] WHERE Contract = DM.Contract AND FileDate = DM.FileDate) AS ContractsTraded,
	(SELECT SUM(ContractsTraded) FROM [dbo].[DailyMTM] WHERE Contract = DM.Contract AND FileDate = DM.FileDate) * 100  / (SELECT SUM([ContractsTraded]) FROM [dbo].[DailyMTM]) AS PercentageOfTotalContractsTraded
	FROM [dbo].[DailyMTM] DM
	WHERE [FileDate] BETWEEN @DateFrom AND @DateTo AND DM.ContractsTraded > 0
	ORDER BY DM.[FileDate]
END
GO
