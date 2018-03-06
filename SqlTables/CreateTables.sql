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
  [CustomerID] INT NOT NULL,
  [FirstName] VARCHAR(30) NOT NULL,
  [LastName] VARCHAR(30) NOT NULL,
  [CreatedDate] DATE NOT NULL,
  [LastActiveDate] DATE NOT NULL,
  [StreetAddress] VARCHAR(50) NOT NULL,
  [City] VARCHAR(30) NOT NULL,
  [State] CHAR(2) NOT NULL,
  [ZipCode] INT NOT NULL,
  [PhoneNumber] VARCHAR(20) NOT NULL,
  CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([CustomerID])
);
GO
CREATE TABLE [DBO].[Payment]
(
  [PaymentID] INT NOT NULL,
  [PaymentType] VARCHAR(30) NOT NULL,
  [PaymentAccountNum] INT NOT NULL,
  CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED ([PaymentID])
);
GO
CREATE TABLE [dbo].[Product]
(
  [ProductID] INT NOT NULL,
  [Name] VARCHAR(30) NOT NULL,
  CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductID])
);
GO
CREATE TABLE [dbo].[Order]
(
  [OrderID] INT NOT NULL,
  [PaymentID] INT NOT NULL,
  [CustomerID] INT NOT NULL,
  CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([OrderID])
);
GO
CREATE TABLE [dbo].[OrderItem]
(
  [OrderItemID] INT NOT NULL,
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
ALTER TABLE [dbo].[OrderItem] ADD CONSTRAINT [FK_ItemProductID]
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX [IFK_OrderItemProductID] ON [dbo].[Product] ([ProductId]);
GO
ALTER TABLE [dbo].[OrderItem] ADD CONSTRAINT [FK_OrderID]
    FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Order] ([OrderID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX [IFK_OrderOrderID] ON [dbo].[Order] ([OrderId]);
GO