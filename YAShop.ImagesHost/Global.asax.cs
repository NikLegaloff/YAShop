using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using YAShop.ImagesHost.Domain;

namespace YAShop.ImagesHost
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            int? i = DB.ExecuteScalar<int?>("select OBJECT_ID('Folder')");
            if (i == null)
            {
                DB.Execute("CREATE TABLE [dbo].[Folder]( [Id] [uniqueidentifier] NOT NULL,	[Name] [nvarchar](128) NOT NULL,	[ParentId] [uniqueidentifier] NULL,	[ContextId] [uniqueidentifier] NOT NULL, CONSTRAINT [PK__Folder__3214EC0724FB77D1] PRIMARY KEY CLUSTERED (	[Id] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]");
                DB.Execute("CREATE TABLE [dbo].[Image](	[Id] [uniqueidentifier] NOT NULL, [Name] [nvarchar](128) NOT NULL,	[FolderId] [uniqueidentifier] NULL,	[Hash] [uniqueidentifier] NOT NULL,	[Size] [int] NOT NULL,	[ContextId] [uniqueidentifier] NOT NULL, CONSTRAINT [PK__File__3214EC07A1AD2218] PRIMARY KEY CLUSTERED (	[Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]");
                DB.Execute("CREATE NONCLUSTERED INDEX [Folder_ContextId] ON [dbo].[Folder]([ContextId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
                DB.Execute("CREATE NONCLUSTERED INDEX [IX_File_Hash] ON [dbo].[Image](	[Hash] ASC,	[Size] ASC,	[ContextId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
            }
        }



        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}