USE [CallousHippo]
GO
/****** Object:  StoredProcedure [dbo].[CreateAccount]    Script Date: 4/2/2020 2:17:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Peter Szadurski>
-- Create date: 
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[CreateAccount] 
	-- Add the parameters for the stored procedure here
	@Username varchar(25),
	@Email varchar(255), @Password varchar(255), @DietId int, @GuiltLevel int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	If NOT EXISTS (Select * from [User] where (Lower(Username) 
	= Lower(@Username) OR Lower(Email) = Lower(@Email)))
	Begin
		Insert into [User] (Username, [Password], Email, DietId, GuiltLevel)
		Values (@Username, @Password, @Email, @DietId, @GuiltLevel)
		return 0;
	End
	else
		Begin
			Return 1;
		End
END
