USE [master]
GO
/****** Object:  Database [Customers]    Script Date: 2019/12/21 19:45:12 ******/
CREATE DATABASE [Customers] ON  PRIMARY 
( NAME = N'Customers', FILENAME = N'D:\data\Customers.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Customers_log', FILENAME = N'D:\data\Customers_log.ldf' , SIZE = 5696KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Customers].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Customers] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Customers] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Customers] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Customers] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Customers] SET ARITHABORT OFF 
GO
ALTER DATABASE [Customers] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Customers] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Customers] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Customers] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Customers] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Customers] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Customers] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Customers] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Customers] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Customers] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Customers] SET RECOVERY FULL 
GO
ALTER DATABASE [Customers] SET  MULTI_USER 
GO
if ( ((@@microsoftversion / power(2, 24) = 8) and (@@microsoftversion & 0xffff >= 760)) or 
		(@@microsoftversion / power(2, 24) >= 9) )begin 
	exec dbo.sp_dboption @dbname =  N'Customers', @optname = 'db chaining', @optvalue = 'OFF'
 end
GO
USE [Customers]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 2019/12/21 19:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreatorId] [int] NOT NULL,
	[LastModifierId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 2019/12/21 19:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Account] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Email] [varchar](200) NULL,
	[Mobile] [varchar](30) NULL,
	[CompanyId] [int] NOT NULL,
	[CompanyName] [nvarchar](500) NULL,
	[State] [int] NOT NULL,
	[UserType] [int] NOT NULL,
	[LastLoginTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreatorId] [int] NOT NULL,
	[LastModifierId] [int] NOT NULL,
	[LastModifyTime] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'用户状态  0正常 1冻结 2删除' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'用户类型  1 普通用户 2管理员 4超级管理员' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'UserType'
GO
USE [master]
GO
ALTER DATABASE [Customers] SET  READ_WRITE 
GO
