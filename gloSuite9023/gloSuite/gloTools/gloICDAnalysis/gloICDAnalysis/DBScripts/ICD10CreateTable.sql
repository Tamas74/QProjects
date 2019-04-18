/****** Object:  Table [dbo].[ICD10OrderFiles]    Script Date: 02/06/2014 18:33:24 ******/
Print '--Started Script execution---'
GO

 -- 1 -- 
 -- ICD10OrderFiles -- 


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICD10OrderFiles]') AND type in (N'U'))
BEGIN

	CREATE TABLE [dbo].[ICD10OrderFiles](
	[nICD10OrderFilesID] [numeric] (18, 0) NOT NULL ,
	[sICD10Code] [varchar] (10) NOT NULL ,
	[nCodeType] [smallint] NOT NULL ,
	[sShortDescription] [varchar] (80) NOT NULL ,
	[sLongDescription] [varchar] (300) NOT NULL
	, CONSTRAINT [PK_ICD10Analysis] PRIMARY KEY CLUSTERED ( [nICD10OrderFilesID] )
	) ON [PRIMARY]

END

GO
/****** Object:  Table [dbo].[ICD10DiffFile]    Script Date: 03/05/2014 18:01:43 ******/
--IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICD10DiffFile]') AND type in (N'U'))
--BEGIN

--CREATE TABLE [dbo].[ICD10DiffFile](
--	[sIndicator] [varchar](1) NOT NULL,
--	[sICD10Code] [varchar](10) NOT NULL,
--	[sDescription] [varchar](200) NULL
--) ON [PRIMARY]

--END

GO
SET ANSI_PADDING OFF
GO


/****** Object:  Table [dbo].[ICD9ToICD10Mapping]    Script Date: 03/24/2014 10:58:47 ******/
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICD9ToICD10Mapping]') AND type in (N'U'))
BEGIN 
	CREATE TABLE [dbo].[ICD9ToICD10Mapping](
		[nICD9ToICD10MappingID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
		[sICD9Code] [varchar](10) NOT NULL,
		[sICD10Code] [varchar](10) NOT NULL,
		[sFlag] [varchar](10) NOT NULL,
		[sICD9DecimalCode] [varchar](10) NULL,
		[nApproximateFlag] [varchar](50) NULL,
		[nNoMapFLAG] [smallint] NULL,
		[nCombinationFLAG] [smallint] NULL,
		[nScenario] [smallint] NULL,
		[nChoiceList] [smallint] NULL,
	 CONSTRAINT [PK_ICD9ToICD10] PRIMARY KEY CLUSTERED 
	(
		[nICD9ToICD10MappingID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

SET ANSI_PADDING OFF
GO
Print '--Completed Script execution---'
GO
