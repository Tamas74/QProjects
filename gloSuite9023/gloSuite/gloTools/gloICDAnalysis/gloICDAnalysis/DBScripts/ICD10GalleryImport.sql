
  GO

 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICD9ToICD10Mapping]') AND type in (N'U'))   
    Begin
    
    ----Update Flags & sICD9DecimalCode of  ICD9ToICD10Mapping
         UPDATE dbo.ICD9ToICD10Mapping
		 Set nApproximateFlag=Substring(ltrim(rtrim(Srs.sFlag)),1,1),
		 nNoMapFLAG =Substring(ltrim(rtrim(Srs.sFlag)),2,1) ,
		 nCombinationFLAG=Substring(ltrim(rtrim(Srs.sFlag)),3,1), 
		 nScenario=Substring(ltrim(rtrim(Srs.sFlag)),4,1) ,
		 nChoiceList=Substring(ltrim(rtrim(Srs.sFlag)),5,1),
		 sICD9DecimalCode =CASE LEN(Srs.sICD9Code)
						   WHEN 3 THEN Srs.sICD9Code
						   ELSE SUBSTRING(Srs.sICD9Code,0,4)+'.'+SUBSTRING(Srs.sICD9Code,4,LEN(Srs.sICD9Code)-3)
						   END 
         FROM ICD9ToICD10Mapping as Srs
         
    End     
