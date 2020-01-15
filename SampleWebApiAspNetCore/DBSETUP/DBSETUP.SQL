---update appsettings.json for db connection string and run following SQL on DB



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webAppData](
	[guid] [uniqueidentifier] NULL,
	[lastModified] [datetime2](7) NOT NULL,
	[userData] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO




ALTER procedure [dbo].[GetUserDataByGuid]	 
(
	@guid varchar(36)
)
AS
BEGIN
		select  CONVERT(varchar(36), [guid]) as ID ,userData,lastModified from [dbo].[webAppData] where [guid]  =  CONVERT(uniqueidentifier,@guid)  ;
END
GO


alter PROCEDURE [dbo].[AddOrUpdateUserDataByGuid]
(
	@guid varchar(36),
	@lastModified datetime2,
	@userData nvarchar(MAX)
)
AS
BEGIN	
	SET NOCOUNT ON

    if @guid is null or @lastModified  is null 
        RAISERROR ('invalid input.',16,1);  

	declare @saved_guid uniqueidentifier

	select  top 1 @saved_guid=[guid] from [dbo].[webAppData] where  CONVERT(varchar(36), [guid]) =CONVERT(uniqueidentifier,@guid) 

	if @saved_guid is not NULL    
		update [dbo].[webAppData ] 
		set userData=@userData,
		lastModified=@lastModified
		where guid =@saved_guid;	     
	else  	
		INSERT INTO [dbo].[webAppData ]  (guid, lastModified, userData) VALUES(CONVERT(uniqueidentifier, @guid)  , @lastModified, @userData);	
END


