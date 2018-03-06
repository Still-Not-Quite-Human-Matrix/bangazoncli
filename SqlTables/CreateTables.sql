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
  [PhoneNumber] INT NOT NULL,
  CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([CustomerID])
);
GO
