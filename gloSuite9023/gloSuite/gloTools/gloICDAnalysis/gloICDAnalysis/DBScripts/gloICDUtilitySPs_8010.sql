
GO

/****** Object:  StoredProcedure [dbo].[gsp_GetICDUsageByProviderRevised]    Script Date: 12/04/2014 15:30:14 ******/
IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'[dbo].[gsp_GetICDUsageByProviderRevised]')
                    AND type IN ( N'P', N'PC' ) ) 
    DROP PROCEDURE [dbo].[gsp_GetICDUsageByProviderRevised]
GO


GO

/****** Object:  StoredProcedure [dbo].[gsp_GetICDUsageByProviderRevised]    Script Date: 12/04/2014 15:30:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


		-- =============================================
		-- Author:		<Author,,Name>
		-- Create date: <Create Date,,>
		-- Description:	<Description,,>
		-- =============================================
CREATE PROCEDURE [dbo].[gsp_GetICDUsageByProviderRevised]
    @ProviderID NUMERIC(18, 0) = 0 ,
    @ProviderType AS VARCHAR(10) ,
    @dtStartDate AS DATE ,
    @dtEndDate AS DATE
AS 
    BEGIN
        SET NOCOUNT ON ;

        DECLARE @tempICD AS TABLE
            (
              TransactionDate DATE ,
              nProviderID NUMERIC(18, 0) ,
              sICD9Code VARCHAR(50) ,
              sICD9Description VARCHAR(MAX) ,
              ProviderType VARCHAR(10) ,
              ProviderName VARCHAR(255) ,
              nUsage INT
            ) ;  

        DECLARE @tempMain AS TABLE
            (
              sICD9Code VARCHAR(50) ,
              sICD9Description VARCHAR(MAX) ,
              ProviderName VARCHAR(255) ,
              Usage INT ,
              Percentage DECIMAL(18, 2) ,
              RunningPercentage DECIMAL(18, 2)
            ) ;  
			--Bug #75687: gloEMR > Tools > ICD9 Usage & ICD10 Mapping report > showing timeout exception
        DECLARE @tempFinal AS TABLE
            (
              ncnt NUMERIC(18, 0) IDENTITY ,
              nRowNo NUMERIC(18, 0) ,
              sICD9Code VARCHAR(50) ,
              sICD9Description VARCHAR(MAX) ,
              Usage INT ,
              Percentage FLOAT ,
              RunningPer FLOAT
            ) ;

        WITH    temp_icds
                  AS ( SELECT   CAST(exam.dtDOS AS DATE) AS [TransactionDate] ,
                                exam.nProviderID ,
                                exam_icd.sICD9Code ,
                                exam_icd.sICD9Description ,
                                'EXAM' AS ProviderType ,
                                COUNT(exam.dtDOS) AS Usage
                       FROM     ExamICD9CPT AS exam_icd
                                INNER JOIN PatientExams AS exam ON exam_icd.nExamID = exam.nExamID
                       WHERE    exam_icd.nICDRevision = 9
				--Bug #75687: gloEMR > Tools > ICD9 Usage & ICD10 Mapping report > showing timeout exception
                                AND ( CONVERT(DATE, dtDOS) BETWEEN @dtStartDate
                                                           AND
                                                              @dtEndDate )
                       GROUP BY exam_icd.sICD9Code ,
                                exam_icd.sICD9Description ,
                                exam.nProviderID ,
                                exam.dtDOS
                       UNION
                       SELECT   CAST(CAST(claim.nTransactionDate AS VARCHAR) AS DATE) AS nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx1Code AS sICD9Code ,
                                claim_icd.sDx1Description AS sICD9Description ,
                                'CLAIM' AS ProviderType ,
                                COUNT(claim.nTransactionDate) AS Usage
                       FROM     BL_Transaction_Diagnosis AS claim_icd
                                INNER JOIN BL_Transaction_MST AS claim ON claim_icd.nTransactionID = claim.nTransactionID
                       WHERE    claim_icd.nICDRevision = 9 --AND ISNULL(claim_icd.sDx1Code,'')<>''
				--Bug #75687: gloEMR > Tools > ICD9 Usage & ICD10 Mapping report > showing timeout exception
                                AND ( CONVERT(DATE, CAST(claim.nTransactionDate AS VARCHAR)) BETWEEN @dtStartDate
                                                              AND
                                                              @dtEndDate )
                       GROUP BY claim.nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx1Code ,
                                claim_icd.sDx1Description
                       UNION
                       SELECT   CAST(CAST(claim.nTransactionDate AS VARCHAR) AS DATE) AS nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx1Code AS sICD9Code ,
                                claim_icd.sDx1Description AS sICD9Description ,
                                'CLAIM' AS ProviderType ,
                                COUNT(claim.nTransactionDate) AS Usage
                       FROM     BL_Transaction_Lines AS claim_icd
                                INNER JOIN BL_Transaction_MST AS claim ON claim_icd.nTransactionID = claim.nTransactionID
                       WHERE    claim.nICDRevision = 9
                                AND ISNULL(claim_icd.sDx1Code, '') <> ''
				--Bug #75687: gloEMR > Tools > ICD9 Usage & ICD10 Mapping report > showing timeout exception
                                AND ( CONVERT(DATE, CAST(claim.nTransactionDate AS VARCHAR)) BETWEEN @dtStartDate
                                                              AND
                                                              @dtEndDate )
                       GROUP BY claim.nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx1Code ,
                                claim_icd.sDx1Description
                       UNION
                       SELECT   CAST(CAST(claim.nTransactionDate AS VARCHAR) AS DATE) AS nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx2Code AS sICD9Code ,
                                claim_icd.sDx2Description AS sICD9Description ,
                                'CLAIM' AS ProviderType ,
                                COUNT(claim.nTransactionDate) AS Usage
                       FROM     BL_Transaction_Lines AS claim_icd
                                INNER JOIN BL_Transaction_MST AS claim ON claim_icd.nTransactionID = claim.nTransactionID
                       WHERE    claim.nICDRevision = 9
                                AND ISNULL(claim_icd.sDx2Code, '') <> ''
						--Bug #75687: gloEMR > Tools > ICD9 Usage & ICD10 Mapping report > showing timeout exception
                                AND ( CONVERT(DATE, CAST(claim.nTransactionDate AS VARCHAR)) BETWEEN @dtStartDate
                                                              AND
                                                              @dtEndDate )
                       GROUP BY claim.nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx2Code ,
                                claim_icd.sDx2Description
                       UNION
                       SELECT   CAST(CAST(claim.nTransactionDate AS VARCHAR) AS DATE) AS nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx3Code AS sICD9Code ,
                                claim_icd.sDx3Description AS sICD9Description ,
                                'CLAIM' AS ProviderType ,
                                COUNT(claim.nTransactionDate) AS Usage
                       FROM     BL_Transaction_Lines AS claim_icd
                                INNER JOIN BL_Transaction_MST AS claim ON claim_icd.nTransactionID = claim.nTransactionID
                       WHERE    claim.nICDRevision = 9
                                AND ISNULL(claim_icd.sDx3Code, '') <> ''
						--Bug #75687: gloEMR > Tools > ICD9 Usage & ICD10 Mapping report > showing timeout exception
                                AND ( CONVERT(DATE, CAST(claim.nTransactionDate AS VARCHAR)) BETWEEN @dtStartDate
                                                              AND
                                                              @dtEndDate )
                       GROUP BY claim.nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx3Code ,
                                claim_icd.sDx3Description
                       UNION
                       SELECT   CAST(CAST(claim.nTransactionDate AS VARCHAR) AS DATE) AS nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx4Code AS sICD9Code ,
                                claim_icd.sDx4Description AS sICD9Description ,
                                'CLAIM' AS ProviderType ,
                                COUNT(claim.nTransactionDate) AS Usage
                       FROM     BL_Transaction_Lines AS claim_icd
                                INNER JOIN BL_Transaction_MST AS claim ON claim_icd.nTransactionID = claim.nTransactionID
                       WHERE    claim.nICDRevision = 9
                                AND ISNULL(claim_icd.sDx4Code, '') <> ''
						--Bug #75687: gloEMR > Tools > ICD9 Usage & ICD10 Mapping report > showing timeout exception
                                AND ( CONVERT(DATE, CAST(claim.nTransactionDate AS VARCHAR)) BETWEEN @dtStartDate
                                                              AND
                                                              @dtEndDate )
                       GROUP BY claim.nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx4Code ,
                                claim_icd.sDx4Description
                       UNION
                       SELECT   CAST(CAST(claim.nTransactionDate AS VARCHAR) AS DATE) AS nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx5Code AS sICD9Code ,
                                claim_icd.sDx5Description AS sICD9Description ,
                                'CLAIM' AS ProviderType ,
                                COUNT(claim.nTransactionDate) AS Usage
                       FROM     BL_Transaction_Lines AS claim_icd
                                INNER JOIN BL_Transaction_MST AS claim ON claim_icd.nTransactionID = claim.nTransactionID
                       WHERE    claim.nICDRevision = 9
                                AND ISNULL(claim_icd.sDx5Code, '') <> ''
						--Bug #75687: gloEMR > Tools > ICD9 Usage & ICD10 Mapping report > showing timeout exception
                                AND ( CONVERT(DATE, CAST(claim.nTransactionDate AS VARCHAR)) BETWEEN @dtStartDate
                                                              AND
                                                              @dtEndDate )
                       GROUP BY claim.nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx5Code ,
                                claim_icd.sDx5Description
                       UNION
                       SELECT   CAST(CAST(claim.nTransactionDate AS VARCHAR) AS DATE) AS nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx6Code AS sICD9Code ,
                                claim_icd.sDx6Description AS sICD9Description ,
                                'CLAIM' AS ProviderType ,
                                COUNT(claim.nTransactionDate) AS Usage
                       FROM     BL_Transaction_Lines AS claim_icd
                                INNER JOIN BL_Transaction_MST AS claim ON claim_icd.nTransactionID = claim.nTransactionID
                       WHERE    claim.nICDRevision = 9
                                AND ISNULL(claim_icd.sDx6Code, '') <> ''
						--Bug #75687: gloEMR > Tools > ICD9 Usage & ICD10 Mapping report > showing timeout exception
                                AND ( CONVERT(DATE, CAST(claim.nTransactionDate AS VARCHAR)) BETWEEN @dtStartDate
                                                              AND
                                                              @dtEndDate )
                       GROUP BY claim.nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx6Code ,
                                claim_icd.sDx6Description
                       UNION
                       SELECT   CAST(CAST(claim.nTransactionDate AS VARCHAR) AS DATE) AS nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx7Code AS sICD9Code ,
                                claim_icd.sDx7Description AS sICD9Description ,
                                'CLAIM' AS ProviderType ,
                                COUNT(claim.nTransactionDate) AS Usage
                       FROM     BL_Transaction_Lines AS claim_icd
                                INNER JOIN BL_Transaction_MST AS claim ON claim_icd.nTransactionID = claim.nTransactionID
                       WHERE    claim.nICDRevision = 9
                                AND ISNULL(claim_icd.sDx7Code, '') <> ''
						--Bug #75687: gloEMR > Tools > ICD9 Usage & ICD10 Mapping report > showing timeout exception
                                AND ( CONVERT(DATE, CAST(claim.nTransactionDate AS VARCHAR)) BETWEEN @dtStartDate
                                                              AND
                                                              @dtEndDate )
                       GROUP BY claim.nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx7Code ,
                                claim_icd.sDx7Description
                       UNION
                       SELECT   CAST(CAST(claim.nTransactionDate AS VARCHAR) AS DATE) AS nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx8Code AS sICD9Code ,
                                claim_icd.sDx8Description AS sICD9Description ,
                                'CLAIM' AS ProviderType ,
                                COUNT(claim.nTransactionDate) AS Usage
                       FROM     BL_Transaction_Lines AS claim_icd
                                INNER JOIN BL_Transaction_MST AS claim ON claim_icd.nTransactionID = claim.nTransactionID
                       WHERE    claim.nICDRevision = 9
                                AND ISNULL(claim_icd.sDx8Code, '') <> ''
						--Bug #75687: gloEMR > Tools > ICD9 Usage & ICD10 Mapping report > showing timeout exception
                                AND ( CONVERT(DATE, CAST(claim.nTransactionDate AS VARCHAR)) BETWEEN @dtStartDate
                                                              AND
                                                              @dtEndDate )
                       GROUP BY claim.nTransactionDate ,
                                claim.nTransactionProviderID ,
                                claim_icd.sDx8Code ,
                                claim_icd.sDx8Description
                     )
            INSERT  INTO @tempICD
                    ( TransactionDate ,
                      nProviderID ,
                      sICD9Code ,
                      sICD9Description ,
                      ProviderType ,
                      nUsage ,
                      ProviderName 
                    )
                    SELECT  temp_icds.TransactionDate ,
                            temp_icds.nProviderID ,
                            temp_icds.sICD9Code ,
                            temp_icds.sICD9Description ,
                            temp_icds.ProviderType ,
                            temp_icds.Usage ,
                            ISNULL(provider.sFirstname, '') + SPACE(1)
                            + CASE ISNULL(provider.sMiddleName, '')
                                WHEN '' THEN ''
                                WHEN sMiddleName THEN sMiddleName + SPACE(1)
                              END + ISNULL(provider.sLastName, '') AS Provider
                    FROM    temp_icds
                            INNER JOIN Provider_MST AS provider ON temp_icds.nProviderID = provider.nProviderID 
			
			
        IF @ProviderID = 0 
            BEGIN
                INSERT  INTO @tempMain
                        ( sICD9Code ,
                          sICD9Description ,
                          ProviderName ,
                          Usage
                        )
                        SELECT  sICD9Code ,
                                sICD9Description ,
                                temp.ProviderName ,
                                SUM(temp.nUsage) AS Usage
                        FROM    @tempICD AS temp
                        WHERE   ProviderType = @ProviderType
                                AND ( CONVERT(DATE, temp.TransactionDate) BETWEEN @dtStartDate
                                                              AND
                                                              @dtEndDate )
                        GROUP BY sICD9Code ,
                                sICD9Description ,
                                temp.ProviderName
            END
        ELSE 
            BEGIN
                INSERT  INTO @tempMain
                        ( sICD9Code ,
                          sICD9Description ,
                          ProviderName ,
                          Usage
                        )
                        SELECT  sICD9Code ,
                                sICD9Description ,
                                temp.ProviderName ,
                                SUM(temp.nUsage) AS Usage
                        FROM    @tempICD AS temp
                        WHERE   ProviderType = @ProviderType
                                AND temp.nProviderID = @ProviderID
                                AND ( CONVERT(DATE, temp.TransactionDate) BETWEEN @dtStartDate
                                                              AND
                                                              @dtEndDate )
                        GROUP BY sICD9Code ,
                                sICD9Description ,
                                temp.ProviderName
            END
			
			
        DECLARE @TotalUsage INTEGER 
        SELECT  @TotalUsage = SUM(Usage)
        FROM    @tempMain ;
			
        DECLARE @ctr INT ;
			
        WITH    tblFinal
                  AS ( SELECT   ROW_NUMBER() OVER ( ORDER BY SUM(Usage) ASC ) AS RowNo ,
                                sICD9Code ,
                                sICD9Description ,
                                SUM(Usage) AS Usage ,
				--round(CONVERT (DECIMAL(18,9),(CONVERT (DECIMAL(18,9), COUNT(sICD9Code))/ CONVERT (DECIMAL(18,9),@TotalUsage)) *100),1) AS Percentage
                                CONVERT(FLOAT, CONVERT (FLOAT, ( CONVERT (FLOAT, SUM(Usage))
                                                              / CONVERT (FLOAT, @TotalUsage) )
                                * 100)) AS Percentage
                       FROM     @tempMain
                       GROUP BY sICD9Code ,
                                sICD9Description
				--order by sum(Usage)desc
                                
                     )
            --Bug #75687: gloEMR > Tools > ICD9 Usage & ICD10 Mapping report > showing timeout exception

			--SELECT CAST(0 AS  BIT) AS [Select],
			--t1.sICD9Code AS [ICD-9 Code], t1.sICD9Description AS [Description] , t1.USAGE AS [Usage], CONVERT(DECIMAL(18,2),CONVERT(VARCHAR,t1.Percentage)) AS [%],
			--CONVERT(DECIMAL(18,2),CONVERT(VARCHAR,sum(t2.Percentage))) as [Running %]
			--from tblFinal as t1
			--inner join tblFinal as t2 on t2.RowNo>=t1.RowNo
			--group by t1.RowNo, t1.sICD9Code, t1.sICD9Description, t1.Usage, t1.Percentage
			--order by CONVERT(DECIMAL(18,2),CONVERT(VARCHAR,sum(t2.Percentage))) ASC 

			INSERT    INTO @tempFinal
                            ( nRowNo ,
                              sICD9Code ,
                              sICD9Description ,
                              Usage ,
                              Percentage ,
                              RunningPer
                            )
                            SELECT  RowNo ,
                                    sICD9Code ,
                                    sICD9Description ,
                                    Usage ,
                                    Percentage ,
                                    0 AS RunningPer
                            FROM    tblFinal
                            ORDER BY Percentage DESC ,
                                    sICD9Description ASC 
			
        BEGIN TRY			
            DECLARE @ncnt NUMERIC(18, 0) ,
                @Percentage FLOAT ,
                @RunningPer FLOAT
            SET @RunningPer = 0 
			
            DECLARE _CURSOR CURSOR FOR 
            SELECT ncnt,Percentage
            FROM @tempFinal
            ORDER BY ncnt ASC

            OPEN _CURSOR
            FETCH NEXT FROM  _CURSOR
			INTO @ncnt,@Percentage
            WHILE @@FETCH_STATUS = 0 
                BEGIN
                    SET @RunningPer = @RunningPer + @Percentage
                    UPDATE  @tempFinal
                    SET     RunningPer = @RunningPer
                    WHERE   ncnt = @ncnt
				
                    FETCH NEXT FROM  _CURSOR
				INTO @ncnt,@Percentage ;
                END
            CLOSE _CURSOR
            DEALLOCATE _CURSOR

        END TRY
        BEGIN CATCH
            CLOSE _CURSOR
            DEALLOCATE _CURSOR
        END CATCH
			
			--select CAST(0 AS  BIT) AS [Select],sICD9Code AS [ICD-9 Code],sICD9Description AS [Description], USAGE AS [Usage],CONVERT(DECIMAL(18,2),CONVERT(VARCHAR,Percentage)) AS [%],CONVERT(DECIMAL(18,2),CONVERT(VARCHAR,RunningPer)) as [Running %] from @tempFinal order by RunningPer
			
			
        CREATE TABLE #HashFinal
            (
              ncnt NUMERIC(18, 0) IDENTITY ,
              nRowNo NUMERIC(18, 0) ,
              sICD9Code VARCHAR(50) ,
              sICD9Description VARCHAR(MAX) ,
              Usage INT ,
              Percentage FLOAT ,
              RunningPer FLOAT
            )
			
        INSERT  INTO #HashFinal
                ( nRowNo ,
                  sICD9Code ,
                  sICD9Description ,
                  Usage ,
                  Percentage ,
                  RunningPer 
                )
                SELECT  nRowNo ,
                        sICD9Code ,
                        sICD9Description ,
                        Usage ,
                        Percentage ,
                        RunningPer
                FROM    @tempFinal 
					
			
        CREATE TABLE #HashIcdSnomed1to1
            (
              sICD9Code VARCHAR(10) ,
              sSnomedCode VARCHAR(20)
            )
						
			
        DECLARE @sSnoMedServerName VARCHAR(500)= ''        
        DECLARE @sSnoMedDatabaseName VARCHAR(500)= '' 
        SELECT  @sSnoMedServerName = sSettingsValue
        FROM    settings
        WHERE   sSettingsName = 'GLOSMSERVERNAME'        
        SELECT  @sSnoMedDatabaseName = sSettingsValue
        FROM    settings
        WHERE   sSettingsName = 'GLOSMDBNAME'  
			
        DECLARE @strSql1to1 VARCHAR(4000)
			
        SET @strSql1to1 = 'Insert into #HashIcdSnomed1to1 (sICD9Code,sSnomedCode)SELECT Distinct IcdSnomed.ICD_CODE ,IcdSnomed.SNOMED_CID  FROM  
			                 #HashFinal Inner join ['
            + @sSnoMedServerName + '].[' + @sSnoMedDatabaseName
            + '].[dbo].Core_ICD9CM_SNOMED_MAP_1TO1 as IcdSnomed
			                 on #HashFinal.sICD9Code=IcdSnomed.ICD_CODE'
        EXEC (@strSql1to1)
           
           
        SET @strSql1to1 = 'Insert into #HashIcdSnomed1to1 (sICD9Code,sSnomedCode)
                               SELECT Distinct IcdSnomed.ICD_CODE, IcdSnomed.SNOMED_CID  FROM ['
            + @sSnoMedServerName + '].[' + @sSnoMedDatabaseName
            + '].[dbo].Core_ICD9CM_SNOMED_MAP_1TOm as IcdSnomed 
                               Where IcdSnomed .ICD_CODE in (select ICD_CODE  FROM ['
            + @sSnoMedServerName + '].[' + @sSnoMedDatabaseName
            + '].[dbo].Core_ICD9CM_SNOMED_MAP_1TOm 
                               Where IsNull(SNOMED_CID,'''') <> '''' group by ICD_CODE  having COUNT(ICD_CODE)=1 ) '
        EXEC (@strSql1to1)
                               
            
        SELECT  CAST(0 AS BIT) AS [Select] ,
                #HashFinal.sICD9Code AS [ICD-9 Code] ,
                #HashIcdSnomed1to1.sSnomedCode AS SNOMED ,
                sICD9Description AS [Description] ,
                USAGE AS [Usage] ,
                CONVERT(DECIMAL(18, 2), CONVERT(VARCHAR, Percentage)) AS [%] ,
                CONVERT(DECIMAL(18, 2), CONVERT(VARCHAR, RunningPer)) AS [Running %]
        FROM    #HashFinal
                LEFT OUTER JOIN #HashIcdSnomed1to1 ON #HashFinal.sICD9Code = #HashIcdSnomed1to1.sICD9Code
        ORDER BY RunningPer
        
        DROP TABLE #HashFinal
        DROP TABLE #HashIcdSnomed1to1
    END



GO



-- 1 -- 
 -- TVP_ICD10_Import -- 

GO
IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'[dbo].[ICD10_GetICD10UnimportedCodes]')
                    AND type IN ( N'P', N'PC' ) ) 
    DROP PROCEDURE [dbo].[ICD10_GetICD10UnimportedCodes]

GO
GO
IF EXISTS ( SELECT  *
            FROM    sys.types st
                    JOIN sys.schemas ss ON st.schema_id = ss.schema_id
            WHERE   st.name = N'TVP_ICD10_Import'
                    AND ss.name = N'dbo' ) 
    DROP TYPE [dbo].[TVP_ICD10_Import] 
 GO 
CREATE TYPE [dbo].[TVP_ICD10_Import] AS TABLE (
[sICD9Code] [varchar] (50) NULL ,
[sICD10Code] [varchar] (50) NULL
) 



GO

GO


CREATE PROCEDURE [dbo].[ICD10_GetICD10UnimportedCodes]
    @TVPICD10Codes TVP_ICD10_Import READONLY
AS 
    BEGIN
        SET NOCOUNT ON ;
			
			
        SELECT DISTINCT
                CAST(0 AS BIT) AS [Select] ,
                IG.sICD9Code AS sICD10Code ,
                IG.sDescriptionLong ,
                IMAP.sICD9DecimalCode ,
                CASE WHEN ISNULL(I9.sICD9Code, '') = '' THEN ''
                     ELSE 'a'
                END AS 'Code Present'
        FROM    dbo.ICD9Gallery IG
                INNER JOIN @TVPICD10Codes AS TVP ON IG.nICDRevision = 10
                                                    AND IG.sICD9Code = TVP.sICD10Code
                INNER JOIN dbo.ICD9ToICD10Mapping IMAP ON REPLACE(TVP.sICD10Code,
                                                              '.', '') = IMAP.sICD10Code
                                                          AND TVP.sICD9Code = IMAP.sICD9Code
                LEFT OUTER JOIN dbo.ICD9 I9 ON I9.nICDRevision = 10
                                               AND IG.sICD9Code = I9.sICD9Code
        ORDER BY IMAP.sICD9DecimalCode ,
                IG.sICD9Code
    END




GO
 -- 1 -- 
 -- TVP_ICD10Codes -- 

GO
IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'[dbo].[ICD10_SaveMasterCodes]')
                    AND type IN ( N'P', N'PC' ) ) 
    DROP PROCEDURE [dbo].[ICD10_SaveMasterCodes]

GO
GO
IF EXISTS ( SELECT  *
            FROM    sys.types st
                    JOIN sys.schemas ss ON st.schema_id = ss.schema_id
            WHERE   st.name = N'TVP_ICD10Codes'
                    AND ss.name = N'dbo' ) 
    DROP TYPE [dbo].[TVP_ICD10Codes] 
 GO 
CREATE TYPE [dbo].[TVP_ICD10Codes] AS TABLE (
[sICDCode] [varchar] (50) NULL ,
[sDescription] [varchar] (255) NULL ,
[nSpecialtyID] [numeric] (18, 0) NULL ,
[nClinicID] [numeric] (18, 0) NULL ,
[bIsBlocked] [bit] NULL ,
[bInActive] [bit] NULL ,
[nImmediacyDefault] [int] NULL ,
[nICDRevision] [smallint] NULL ,
[nCodeType] [smallint] NULL ,
[sConceptID] [varchar] (100) NULL ,
[sDescriptionID] [varchar] (100) NULL ,
[sSnomedID] [varchar] (100) NULL ,
[sSnomedDescription] [varchar] (500) NULL ,
[sSnomedDefination] [varchar] (MAX) NULL
) 



GO

GO

--exec gsp_GetCurrentCodes '',0,0
CREATE PROCEDURE [dbo].[ICD10_SaveMasterCodes]
    @TVP TVP_ICD10Codes READONLY
AS 
    BEGIN  
      
        DECLARE @TVP_ForInsertion TVP_ICD10Codes

        DECLARE @nClinicID NUMERIC(18, 0)
        SELECT TOP 1
                @nClinicID = nClinicID
        FROM    dbo.Clinic_MST

        INSERT  INTO @TVP_ForInsertion
                ( sICDCode ,
                  sDescription ,
                  nSpecialtyID ,
                  nClinicID ,
                  bIsBlocked ,
                  bInActive ,
                  nImmediacyDefault ,
                  nICDRevision ,
                  nCodeType ,
                  sConceptID ,
                  sDescriptionID ,
                  sSnomedID ,
                  sSnomedDescription ,
                  sSnomedDefination
                        
                )
                SELECT DISTINCT
                        TVP.sICDCode ,
                        TVP.sDescription ,
                        TVP.nSpecialtyID ,
                        @nClinicID ,
                        TVP.bIsBlocked ,
                        TVP.bInActive ,
                        TVP.nImmediacyDefault ,
                        TVP.nICDRevision ,
                        TVP.nCodeType ,
                        TVP.sConceptID ,
                        TVP.sDescriptionID ,
                        TVP.sSnomedID ,
                        TVP.sSnomedDescription ,
                        TVP.sSnomedDefination
                FROM    @TVP TVP
      
        INSERT  INTO dbo.ICD9
                ( nICD9ID ,
                  sICD9Code ,
                  sDescription ,
                  nSpecialtyID ,
                  nClinicID ,
                  bIsBlocked ,
                  bInActive ,
                  nImmediacyDefault ,
                  nICDRevision ,
                  nCodeType ,
                  sConceptID ,
                  sDescriptionID ,
                  sSnomedID ,
                  sSnomedDescription ,
                  sSnomedDefination
                )
                SELECT  dbo.GetUniqueID_V2() ,
                        TVP.sICDCode ,
                        TVP.sDescription ,
                        TVP.nSpecialtyID ,
                        TVP.nClinicID ,
                        TVP.bIsBlocked ,
                        TVP.bInActive ,
                        TVP.nImmediacyDefault ,
                        TVP.nICDRevision ,
                        TVP.nCodeType ,
                        TVP.sConceptID ,
                        TVP.sDescriptionID ,
                        TVP.sSnomedID ,
                        TVP.sSnomedDescription ,
                        TVP.sSnomedDefination
                FROM    @TVP_ForInsertion TVP
                        LEFT OUTER JOIN ICD9 I9 ON TVP.sICDCode = I9.sICD9Code
                                                   AND TVP.nICDRevision = I9.nICDRevision
                WHERE   I9.sICD9Code IS NULL
      
    END 