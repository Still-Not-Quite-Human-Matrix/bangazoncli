/*******************************************************************************
   Drop database if it exists
********************************************************************************/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'SNQHM_bangazoncli_db')
BEGIN
    ALTER DATABASE [SNQHM_bangazoncli_db] SET OFFLINE WITH ROLLBACK IMMEDIATE;
    ALTER DATABASE [SNQHM_bangazoncli_db] SET ONLINE;
    DROP DATABASE [SNQHM_bangazoncli_db];
END

GO

/*******************************************************************************
   Create database
********************************************************************************/
CREATE DATABASE [SNQHM_bangazoncli_db];
GO

USE [SNQHM_bangazoncli_db];
GO

/*******************************************************************************
   Create Tables
********************************************************************************/
CREATE TABLE [dbo].[Customer]
(
  [CustomerID] INT NOT NULL IDENTITY (1, 1),
  [FirstName] VARCHAR(30) NOT NULL,
  [LastName] VARCHAR(30) NOT NULL,
  [CreatedDate] DATE NOT NULL,
  [LastActiveDate] DATE NOT NULL,
  [StreetAddress] VARCHAR(50) NOT NULL,
  [City] VARCHAR(30) NOT NULL,
  [State] CHAR(2) NOT NULL,
  [ZipCode] INT NOT NULL,
  [PhoneNumber] BIGINT NOT NULL,
  CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([CustomerID])
);
GO
CREATE TABLE [DBO].[Payment]
(
  [PaymentID] INT NOT NULL IDENTITY (1, 1),
  [PaymentType] VARCHAR(30) NOT NULL,
  [PaymentAccountNum] INT NOT NULL,
  [CusID] INT NOT NULL
  CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED ([PaymentID])
);
GO
CREATE TABLE [dbo].[Product]
(
  [ProductID] INT NOT NULL IDENTITY (1, 1),
  [Name] VARCHAR(30) NOT NULL,
  [Price] MONEY NOT NULL,
  [Owner] INT NOT NULL,
  [Count] [int] NOT NULL,
  [Description] [nvarchar](200) NULL,
  CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductID])
);
GO
CREATE TABLE [dbo].[Order]
(
  [OrderID] INT NOT NULL IDENTITY (1, 1),
  [PaymentID] INT NOT NULL,
  [CustomerID] INT NOT NULL,
  CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([OrderID])
);
GO
CREATE TABLE [dbo].[OrderItem]
(
  [OrderItemID] INT NOT NULL IDENTITY (1, 1),
  [OrderID] INT NOT NULL,
  [ProductID] INT NOT NULL,
  CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED ([OrderItemID])
);
GO
/*******************************************************************************
   Create Foreign Keys
********************************************************************************/
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [FK_PaymentID]
    FOREIGN KEY ([PaymentID]) REFERENCES [dbo].[Payment] ([PaymentID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX [IFK_OrderPaymentID] ON [dbo].[Payment] ([PaymentID]);
GO
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [FK_CustomerID]
    FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([CustomerID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX [IFK_OrderCustomerID] ON [dbo].[Customer] ([CustomerID]);
GO
ALTER TABLE [dbo].[OrderItem] ADD CONSTRAINT [FK_ProductID]
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX [IFK_OrderItemProductID] ON [dbo].[Product] ([ProductId]);
GO
ALTER TABLE [dbo].[OrderItem] ADD CONSTRAINT [FK_OrderID]
    FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Order] ([OrderID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX [IFK_OrderOrderID] ON [dbo].[Order] ([OrderId]);
GO
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [FK_Owner]
    FOREIGN KEY ([Owner]) REFERENCES [dbo].[Customer] ([CustomerID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX [IFK_ProductOwnder] ON [dbo].[Customer] ([CustomerID]);
GO
ALTER TABLE [dbo].[Payment] ADD CONSTRAINT [FK_CusID]
    FOREIGN KEY ([CusID]) REFERENCES [dbo].[Customer] ([CustomerID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX [IFK_PaymentCusID] ON [dbo].[Customer] ([CustomerID]);
GO
