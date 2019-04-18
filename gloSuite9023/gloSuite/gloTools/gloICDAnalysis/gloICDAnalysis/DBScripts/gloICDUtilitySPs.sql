
GO
-- -- 1 -- 
-- -- gsp_GetDBVersion -- 

--GO
--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[gsp_GetDBVersion]') AND type in (N'P', N'PC'))
--DROP PROCEDURE [dbo].[gsp_GetDBVersion]

--GO
--CREATE PROCEDURE gsp_GetDBVersion        
-- AS 
-- Begin
--    SET NOCOUNT ON ;          
--    SELECT * FROM dbo.Settings WHERE sSettingsName = 'DB Version'   
 
--END 

--GO
 -- 2 -- 
 -- gsp_GetProviderByType -- 

 GO
 -- 1 -- 
 -- Check_IsDBScriptExist -- 

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Check_IsDBScriptExist]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Check_IsDBScriptExist]

GO
CREATE PROCEDURE [dbo].[Check_IsDBScriptExist]
 --@ISNotExist as Bit  OUTPUT 
AS                        
BEGIN 
    
    declare @ISNotExist bit 
    
    set @ISNotExist = 1
	IF Not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[gsp_GetProviderByType]') AND type in (N'P', N'PC'))
		Begin
			set @ISNotExist = 0
		End
		
    IF Not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICD9ToICD10Mapping]'))
        Begin
			set @ISNotExist = 0
		End
     
    IF Not  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[gsp_GetICDUsageByProviderRevised_7022]') AND type in (N'P', N'PC')) 
		Begin
			set @ISNotExist = 0
		End
		
	select @ISNotExist		
    
END 

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[gsp_GetProviderByType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[gsp_GetProviderByType]

GO

CREATE PROCEDURE [dbo].[gsp_GetProviderByType]
    @ProviderType AS VARCHAR(10)
AS 
    BEGIN
        SET NOCOUNT ON ;
	
	IF @ProviderType='Exam'
		BEGIN
		With TblExamProvider as 
	      (
			SELECT DISTINCT(pr.nProviderID),
			ISNULL(pr.sFirstName,'')+ SPACE(1) +
			CASE ISNULL(pr.sMiddleName,'') WHEN  '' THEN '' When sMiddleName then  
			sMiddleName + SPACE(1) END +ISNULL(pr.sLastName,'') AS Provider FROM dbo.ExamICD9CPT xIC
			INNER JOIN dbo.PatientExams px  ON xIC.nExamID = px.nExamID
			INNER JOIN dbo.Provider_MST pr ON px.nProviderID = pr.nProviderID			 
			UNION
			SELECT 0,'All' 
		  )
		SELECT * FROM TblExamProvider ORDER BY Provider,nProviderID
		  
		END
	
	ELSE
		BEGIN
		With TblClaimProvider as 
	      (
			SELECT 
			DISTINCT(pr.nProviderID),
			ISNULL(pr.sFirstName,'')+ SPACE(1) +
			CASE ISNULL(pr.sMiddleName,'') WHEN  '' THEN '' When sMiddleName then  
			sMiddleName + SPACE(1) END +ISNULL(pr.sLastName,'') AS Provider
			FROM dbo.BL_Transaction_MST BL_MST 
			INNER JOIN dbo.Provider_MST pr ON BL_MST.nTransactionProviderID = pr.nProviderID			
			UNION
			SELECT 0,'All' 
	      )	
	    SELECT * FROM TblClaimProvider ORDER BY Provider,nProviderID  	
			
		END
END


-- 3 -- 
 -- gsp_GetICDUsageByProviderRevised_7022 -- 

GO
/****** Object:  StoredProcedure [dbo].[gsp_GetICDUsageByProviderRevised_7022]    Script Date: 03/18/2014 11:59:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[gsp_GetICDUsageByProviderRevised_7022]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[gsp_GetICDUsageByProviderRevised_7022]
GO

/****** Object:  StoredProcedure [dbo].[gsp_GetICDUsageByProviderRevised_7022]    Script Date: 03/18/2014 11:59:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[gsp_GetICDUsageByProviderRevised_7022] 
	@ProviderID numeric (18,0) = 0,
	@ProviderType as VARCHAR(10),
	@dtStartDate AS date,    
    @dtEndDate AS DATE    
AS
BEGIN
			SET NOCOUNT ON;

			DECLARE @tempICD AS TABLE  
			(  
				TransactionDate date,  
				nProviderID numeric(18,0),  
				sICD9Code varchar(50),  
				sICD9Description varchar(max),  
				ProviderType varchar(10),
				ProviderName varchar(255),
				nUsage int  
			);  

			DECLARE @tempMain AS TABLE  
			(  
				sICD9Code varchar(50),  
				sICD9Description varchar(max),  
				ProviderName varchar(255),
				Usage int,
				Percentage decimal(18,2),
				RunningPercentage decimal(18,2)
			);  
			 
			With temp_icds as 
			(
				select CAST(exam.dtDOS AS DATE) as [TransactionDate], exam.nProviderID,exam_icd.sICD9Code, exam_icd.sICD9Description, 'EXAM' as ProviderType ,
				COUNT(exam.dtDOS) AS Usage
				from  ExamICD9CPT as exam_icd
				inner join PatientExams as exam on exam_icd.nExamID = exam.nExamID				
				group by exam_icd.sICD9Code, exam_icd.sICD9Description,exam.nProviderID,exam.dtDOS

				UNION 

				select 
				CAST(CAST(claim.nTransactionDate AS varchar) AS date) as nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx1Code as sICD9Code, claim_icd.sDx1Description as sICD9Description, 'CLAIM' AS ProviderType,
				COUNT(claim.nTransactionDate) AS Usage
				from BL_Transaction_Diagnosis as claim_icd
				inner join BL_Transaction_MST as claim on claim_icd.nTransactionID = claim.nTransactionID				
				group by claim.nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx1Code, claim_icd.sDx1Description
				
				UNION
				
				select 	CAST(CAST(claim.nTransactionDate AS varchar) AS date) as nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx1Code as sICD9Code, 
				claim_icd.sDx1Description as sICD9Description, 'CLAIM' AS ProviderType,COUNT(claim.nTransactionDate) AS Usage
				from BL_Transaction_Lines as claim_icd
				inner join BL_Transaction_MST as claim on claim_icd.nTransactionID = claim.nTransactionID
				where  ISNULL(claim_icd.sDx1Code,'')<>''
				group by claim.nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx1Code, claim_icd.sDx1Description
				
				UNION		
				select 	CAST(CAST(claim.nTransactionDate AS varchar) AS date) as nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx2Code as sICD9Code, 
						claim_icd.sDx2Description as sICD9Description, 'CLAIM' AS ProviderType,COUNT(claim.nTransactionDate) AS Usage
						from BL_Transaction_Lines as claim_icd
						inner join BL_Transaction_MST as claim on claim_icd.nTransactionID = claim.nTransactionID
						where  ISNULL(claim_icd.sDx2Code,'')<>''
						group by claim.nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx2Code, claim_icd.sDx2Description	
				
				UNION		
				select 	CAST(CAST(claim.nTransactionDate AS varchar) AS date) as nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx3Code as sICD9Code, 
						claim_icd.sDx3Description as sICD9Description, 'CLAIM' AS ProviderType,COUNT(claim.nTransactionDate) AS Usage
						from BL_Transaction_Lines as claim_icd
						inner join BL_Transaction_MST as claim on claim_icd.nTransactionID = claim.nTransactionID
						where  ISNULL(claim_icd.sDx3Code,'')<>''
						group by claim.nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx3Code, claim_icd.sDx3Description	
				
				UNION		
				select 	CAST(CAST(claim.nTransactionDate AS varchar) AS date) as nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx4Code as sICD9Code, 
						claim_icd.sDx4Description as sICD9Description, 'CLAIM' AS ProviderType,COUNT(claim.nTransactionDate) AS Usage
						from BL_Transaction_Lines as claim_icd
						inner join BL_Transaction_MST as claim on claim_icd.nTransactionID = claim.nTransactionID
						where  ISNULL(claim_icd.sDx4Code,'')<>''
						group by claim.nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx4Code, claim_icd.sDx4Description	
				
				UNION		
				select 	CAST(CAST(claim.nTransactionDate AS varchar) AS date) as nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx5Code as sICD9Code, 
						claim_icd.sDx5Description as sICD9Description, 'CLAIM' AS ProviderType,COUNT(claim.nTransactionDate) AS Usage
						from BL_Transaction_Lines as claim_icd
						inner join BL_Transaction_MST as claim on claim_icd.nTransactionID = claim.nTransactionID
						where  ISNULL(claim_icd.sDx5Code,'')<>''
						group by claim.nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx5Code, claim_icd.sDx5Description	
				
				UNION		
				select 	CAST(CAST(claim.nTransactionDate AS varchar) AS date) as nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx6Code as sICD9Code, 
						claim_icd.sDx6Description as sICD9Description, 'CLAIM' AS ProviderType,COUNT(claim.nTransactionDate) AS Usage
						from BL_Transaction_Lines as claim_icd
						inner join BL_Transaction_MST as claim on claim_icd.nTransactionID = claim.nTransactionID
						where ISNULL(claim_icd.sDx6Code,'')<>''
						group by claim.nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx6Code, claim_icd.sDx6Description	
				
				UNION		
				select 	CAST(CAST(claim.nTransactionDate AS varchar) AS date) as nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx7Code as sICD9Code, 
						claim_icd.sDx7Description as sICD9Description, 'CLAIM' AS ProviderType,COUNT(claim.nTransactionDate) AS Usage
						from BL_Transaction_Lines as claim_icd
						inner join BL_Transaction_MST as claim on claim_icd.nTransactionID = claim.nTransactionID
						where  ISNULL(claim_icd.sDx7Code,'')<>''
						group by claim.nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx7Code, claim_icd.sDx7Description	
				
				UNION		
				select 	CAST(CAST(claim.nTransactionDate AS varchar) AS date) as nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx8Code as sICD9Code, 
						claim_icd.sDx8Description as sICD9Description, 'CLAIM' AS ProviderType,COUNT(claim.nTransactionDate) AS Usage
						from BL_Transaction_Lines as claim_icd
						inner join BL_Transaction_MST as claim on claim_icd.nTransactionID = claim.nTransactionID
						where  ISNULL(claim_icd.sDx8Code,'')<>''
						group by claim.nTransactionDate,claim.nTransactionProviderID, claim_icd.sDx8Code, claim_icd.sDx8Description	
			)


			
			insert into @tempICD (TransactionDate,nProviderID, sICD9Code, sICD9Description, ProviderType,nUsage, ProviderName )
			select temp_icds.TransactionDate, temp_icds.nProviderID, temp_icds.sICD9Code,temp_icds.sICD9Description, temp_icds.ProviderType, temp_icds.Usage,
			ISNULL(provider.sFirstname,'')+ SPACE(1) +  
			CASE ISNULL(provider.sMiddleName,'') WHEN  '' THEN '' When sMiddleName then    
			sMiddleName + SPACE(1) END +ISNULL(provider.sLastName,'') AS Provider 
			from temp_icds
			inner join Provider_MST as provider on temp_icds.nProviderID = provider.nProviderID 
			
			
			IF @ProviderID = 0  
			BEGIN
				insert into @tempMain (sICD9Code, sICD9Description, ProviderName, Usage)
				select sICD9Code, sICD9Description, temp.ProviderName, sum(temp.nUsage) as Usage from @tempICD as temp
				where ProviderType = @ProviderType 
				and (CONVERT(DATE,temp.TransactionDate) BETWEEN @dtStartDate AND @dtEndDate )  
				group by sICD9Code, sICD9Description ,temp.ProviderName
			END
			ELSE
			BEGIN
				insert into @tempMain (sICD9Code, sICD9Description, ProviderName, Usage)
				select sICD9Code, sICD9Description,temp.ProviderName,sum(temp.nUsage) as Usage  from @tempICD as temp
				where ProviderType = @ProviderType 
				and temp.nProviderID = @ProviderID
				and (CONVERT(DATE,temp.TransactionDate) BETWEEN @dtStartDate AND @dtEndDate )  
				group by sICD9Code, sICD9Description, temp.ProviderName
			END
			
			
			DECLARE @TotalUsage INTEGER 
			select @TotalUsage = sum(Usage) FROM @tempMain ;
			
			declare @ctr int;
			
			With tblFinal as
			(
				select ROW_NUMBER() OVER (ORDER BY sum(Usage) asc) as RowNo,sICD9Code,sICD9Description,sum(Usage) as Usage,
				--round(CONVERT (DECIMAL(18,9),(CONVERT (DECIMAL(18,9), COUNT(sICD9Code))/ CONVERT (DECIMAL(18,9),@TotalUsage)) *100),1) AS Percentage
				CONVERT(FLOAT, CONVERT (FLOAT,(CONVERT (FLOAT, sum(Usage))/ CONVERT (FLOAT,@TotalUsage)) *100)) AS Percentage
				
				from @tempMain
				group by sICD9Code, sICD9Description
				--order by sum(Usage)desc
			)
			
			SELECT CAST(0 AS  BIT) AS [Select],
			t1.sICD9Code AS [ICD-9 Code], t1.sICD9Description AS [Description] , t1.USAGE AS [Usage], CONVERT(DECIMAL(18,2),CONVERT(VARCHAR,t1.Percentage)) AS [%],
			CONVERT(DECIMAL(18,2),CONVERT(VARCHAR,sum(t2.Percentage))) as [Running %]
			from tblFinal as t1
			inner join tblFinal as t2 on t2.RowNo>=t1.RowNo
			group by t1.RowNo, t1.sICD9Code, t1.sICD9Description, t1.Usage, t1.Percentage
			order by CONVERT(DECIMAL(18,2),CONVERT(VARCHAR,sum(t2.Percentage))) ASC 
		END

GO



GO
 -- 5 -- 
-- gsp_GetICD10Mapping With TVP-- 

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[gsp_GetICD10Mapping]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[gsp_GetICD10Mapping]

GO

	/****** Object:  UserDefinedTableType [dbo].[tvp_ICDCodes]    Script Date: 03/17/2014 11:08:10 ******/
	IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'tvp_ICDCodes' AND ss.name = N'dbo')
	DROP TYPE [dbo].[tvp_ICDCodes]
	GO

	/****** Object:  UserDefinedTableType [dbo].[tvp_ICDCodes]    Script Date: 03/17/2014 11:08:10 ******/
	CREATE TYPE [dbo].[tvp_ICDCodes] AS TABLE(
		[sICDCode] [varchar](50) NULL
	)

GO

GO


CREATE PROCEDURE  [dbo].[gsp_GetICD10Mapping]        
@TVP_ICDCodes dbo.tvp_ICDCodes READONLY            
AS            

BEGIN            

	SET NOCOUNT ON;				

	Declare @ICDCodes 
	Table ( ID Int Identity (1,1),
			[sICDCode] [varchar](50) NULL
		  )

	Insert Into @ICDCodes (sICDCode)
	Select sICDCode From @TVP_ICDCodes


	 select * into #TblMap from 
	 (    
		SELECT MAP.[sICD9Code], 
				CASE LEN(MAP.sICD10Code) WHEN 3 THEN MAP.sICD10Code
				ELSE SUBSTRING(MAP.sICD10Code,0,4) + '.' + SUBSTRING (MAP.sICD10Code,4,LEN(MAP.sICD10Code)-3)
				END  AS sICD10Code,
				MAP.[sFlag],
				MAP.[sICD9DecimalCode],
				MAP.[nApproximateFlag],              
				MAP.[nNoMapFLAG],              
				MAP.[nCombinationFLAG],              
				MAP.[nScenario],              
				MAP.[nChoiceList],            
				I9.sDescription,      
				I10.sShortDescription AS ICD10Description,
				tvp.ID  AS SortOrder        
	 FROM ICD9ToICD10Mapping AS MAP    
			INNER JOIN ICD9 AS I9 ON REPLACE(MAP.sICD9Code, '.', '') = REPLACE(I9.sICD9Code, '.', '')      
			INNER JOIN @ICDCodes as tvp ON MAP.sICD9Code = REPLACE(tvp.sICDCode, '.', '')    
			--INNER JOIN ICD10OrderFiles AS I10 ON REPLACE(MAP.sICD10Code, '.', '') = REPLACE(I10.sICD10Code, '.', '')    
			INNER JOIN ICD10OrderFiles AS I10 ON MAP.sICD10Code = I10.sICD10Code
	 WHERE MAP.[nChoiceList] <= 1    
	 )  as A  
 
 
	 select * into #mytable from 
	 ( 
 		SELECT map.sICD9Code AS sICD9Code_with, 
				CASE LEN(MAP.sICD10Code) WHEN 3 THEN MAP.sICD10Code
				ELSE SUBSTRING(MAP.sICD10Code,0,4)+'.'+SUBSTRING(MAP.sICD10Code,4,LEN(MAP.sICD10Code)-3)
				END  as sICD10Code_With,          
				MAP.[nChoiceList] as nChoiceList_With,  
				MAP.[nScenario] AS nScenario_with,  
				I10.sShortDescription AS ICD10Description_With
		FROM ICD9ToICD10Mapping AS MAP 
				INNER JOIN ICD9 AS I9 ON REPLACE(MAP.sICD9Code, '.', '') = REPLACE(I9.sICD9Code, '.', '')
				INNER JOIN @ICDCodes as tvp ON MAP.sICD9Code = REPLACE(tvp.sICDCode, '.', '')     
				--INNER JOIN ICD10OrderFiles AS I10 ON REPLACE(MAP.sICD10Code, '.', '') = REPLACE(I10.sICD10Code, '.', '')   
				INNER JOIN ICD10OrderFiles AS I10 ON MAP.sICD10Code = I10.sICD10Code
		WHERE MAP.[nChoiceList] > 1      
	) As B


	 select * into #TblFinalResult from 
	 ( 
		 SELECT *     
		 FROM #TblMap Mytable    
		 LEFT OUTER JOIN    
		 (    
			select * from #mytable    
		 ) AS C on Mytable.sICD9Code  = C.sICD9Code_with
				AND Mytable.nScenario  = C.nScenario_with
	 ) AS TblResult

            
  select * from #TblFinalResult order by SortOrder

  
  drop table #TblFinalResult
  drop table #mytable
  drop table #TblMap
  
  
END     

