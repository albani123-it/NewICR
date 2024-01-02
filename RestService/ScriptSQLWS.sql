USE [iDecisionCRM_F3.0]
GO
/****** Object:  Table [dbo].[ws_tracer]    Script Date: 03/17/2015 12:08:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ws_tracer](
	[id_wstracer] [int] IDENTITY(1,1) NOT NULL,
	[passkey] [varchar](50) NULL,
	[userid] [varchar](50) NULL,
	[login_datetime] [datetime] NULL,
	[last_access] [datetime] NULL,
	[status] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO



USE [iDecisionCRM_F3.0]
GO
/****** Object:  StoredProcedure [dbo].[ws_updatePassKey]    Script Date: 03/09/2015 17:09:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: IZ
-- ALTER date	: 05 Maret 2015
-- Description	: Log Out
-- =============================================
CREATE PROCEDURE [dbo].[ws_updatePassKey]
	-- Add the parameters for the stored procedure here
	@passkey varchar(16)
AS
BEGIN	
	update ws_tracer set last_access=GETDATE() where passkey=@passkey

end
GO
/****** Object:  StoredProcedure [dbo].[ws_timeOut]    Script Date: 03/09/2015 17:09:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: IZ
-- ALTER date	: 05 Maret 2015
-- Description	: Log Out
-- =============================================
CREATE PROCEDURE [dbo].[ws_timeOut]
	-- Add the parameters for the stored procedure here
	@passkey varchar(16)
AS
BEGIN	
	select datediff(MINUTE,last_access,GETDATE()) as OutTime,GETDATE() as tgl_skrg,last_access from ws_tracer where passkey=@passkey

end
GO
/****** Object:  StoredProcedure [dbo].[ws_logOut]    Script Date: 03/09/2015 17:09:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: IZ
-- ALTER date	: 23 Dec 2014
-- Description	: Log Out
-- =============================================
CREATE PROCEDURE [dbo].[ws_logOut]
	-- Add the parameters for the stored procedure here
	@pass varchar(16)
AS
BEGIN	
	update ws_tracer set last_access=getdate(),status='Logout' where passkey=@pass
END
GO
/****** Object:  StoredProcedure [dbo].[ws_insWSTracer]    Script Date: 03/09/2015 17:09:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Iqbal Zuhdi
-- ALTER date	: 24 Februari 2015
-- Description	: Insert WS_Tracer
-- =============================================
CREATE PROCEDURE [dbo].[ws_insWSTracer]
	-- Add the parameters for the stored procedure here
	@passKey varchar(30),
	@NPK varchar(8),
	@status varchar(10)
AS
BEGIN
	INSERT INTO ws_tracer 
	(
	[passkey],
	[userid],
	[login_datetime],
	[last_access],
	[status]
	)
	VALUES
	(
	@passkey,
	@npk,
	GETDATE(),
	GETDATE(),
	@status
	)
END
GO
/****** Object:  StoredProcedure [dbo].[ws_getTasklist]    Script Date: 03/09/2015 17:09:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: IZ
-- ALTER date	: 23 Dec 2014
-- Description	: Get User
-- =============================================
CREATE PROCEDURE [dbo].[ws_getTasklist]
	-- Add the parameters for the stored procedure here
	@unique_code varchar(20)
AS
begin

select * from tasklist where tsk_unique_code=@unique_code

	
END
GO
/****** Object:  StoredProcedure [dbo].[ws_checkPassKey]    Script Date: 03/09/2015 17:09:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: IZ
-- ALTER date	: 23 Dec 2014
-- Description	: Log Out
-- =============================================
CREATE PROCEDURE [dbo].[ws_checkPassKey]
	-- Add the parameters for the stored procedure here
	@passkey varchar(16)
AS
BEGIN	
	select COUNT(*)as rowZ,userid,last_access from ws_tracer where passkey=@passkey group by userid,last_access
END
GO
/****** Object:  StoredProcedure [dbo].[ws_checkLoginPasskey]    Script Date: 03/09/2015 17:09:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: IZ
-- ALTER date	: 09 Maret 2014
-- Description	: Log in
-- =============================================
CREATE PROCEDURE [dbo].[ws_checkLoginPasskey]
	-- Add the parameters for the stored procedure here
	@npk varchar(16)
AS
BEGIN	
	select COUNT(*)as rowZ,passkey,userid from ws_tracer where userid=@npk group by passkey,userid
END
GO
