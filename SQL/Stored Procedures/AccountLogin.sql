-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Peter Szadurski
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE AccountLogin 
	-- Add the parameters for the stored procedure here
	@_email varchar(255),
	@_password varchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF Exists
	(Select * from [User] where Lower(Email) = Lower(@_email) 
	and [Password] = @_password )
	Begin
		Return 0
	End
	Else
		Begin
			Return 1
		End
END
GO
