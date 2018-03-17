USE [SNQHM_bangazoncli_db]
GO

ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_CusomerID]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 3/16/2018 9:10:21 AM ******/
DROP TABLE [dbo].[Product]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 3/16/2018 9:10:21 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Price] [money] NOT NULL,
	[Owner] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Product]  ADD  CONSTRAINT [FK_CusomerID] 
FOREIGN KEY([Owner]) REFERENCES [dbo].[Customer] ([CustomerID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_CusomerID]
GO


