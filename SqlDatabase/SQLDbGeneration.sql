USE [master]
GO
/****** Object:  Database [FRSData]    Script Date: 24.05.2021 9:27:49 ******/
CREATE DATABASE [FRSData]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FRSData', FILENAME = N'D:\SQLExpress\MSSQL14.SQLEXPRESS\MSSQL\DATA\FRSData.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FRSData_log', FILENAME = N'D:\SQLExpress\MSSQL14.SQLEXPRESS\MSSQL\DATA\FRSData_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [FRSData] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FRSData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FRSData] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FRSData] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FRSData] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FRSData] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FRSData] SET ARITHABORT OFF 
GO
ALTER DATABASE [FRSData] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FRSData] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FRSData] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FRSData] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FRSData] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FRSData] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FRSData] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FRSData] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FRSData] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FRSData] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FRSData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FRSData] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FRSData] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FRSData] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FRSData] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FRSData] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FRSData] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FRSData] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FRSData] SET  MULTI_USER 
GO
ALTER DATABASE [FRSData] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FRSData] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FRSData] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FRSData] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FRSData] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FRSData] SET QUERY_STORE = OFF
GO
USE [FRSData]
GO
/****** Object:  Table [dbo].[RegressionDataTable]    Script Date: 24.05.2021 9:27:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegressionDataTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegressionOwnerId] [int] NOT NULL,
	[ACoeff] [decimal](18, 0) NOT NULL,
	[BCoeff] [decimal](18, 0) NOT NULL,
	[PrecisionError] [decimal](18, 0) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegressionFiles]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegressionFiles](
	[RegressionDataId] [int] NOT NULL,
	[FileData] [varbinary](max) NOT NULL,
	[FileName] [nvarchar](255) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubInfo]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubInfo](
	[UserId] [int] NOT NULL,
	[AccessKey] [nvarchar](max) NOT NULL,
	[SecretAccessKey] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [varbinary](256) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddRegressionData]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[AddRegressionData]
@RegressionOwnerId INT,
@ACoeff decimal(18,0),
@BCoeff decimal(18,0)
AS
BEGIN
INSERT INTO RegressionDataTable(RegressionOwnerId, ACoeff, BCoeff)
VALUES(@RegressionOwnerId, @ACoeff, @BCoeff)
SELECT SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[AddSubInfo]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[AddSubInfo]
@UserId INT,
@AccessKey NVARCHAR,
@SecretAccessKey NVARCHAR
AS
BEGIN
INSERT INTO SubInfo(UserId, AccessKey, SecretAccessKey)
VALUES(@UserId, @AccessKey, @SecretAccessKey)
END
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUser]
@Username NVARCHAR(50),
@Password VARBINARY(256)
AS
BEGIN
INSERT INTO Users(Username,Password)
VALUES(@Username,@Password)
SELECT SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[Authentication]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Authentication]
@Username NVARCHAR(50),
@Password NVARCHAR(256)
AS
BEGIN
	SELECT COUNT(*) FROM Users WHERE Username=@Username AND Password=@Password
END
GO
/****** Object:  StoredProcedure [dbo].[GetIdByUsername]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetIdByUsername]
@Username NVARCHAR(50)
AS
BEGIN
SELECT Id FROM Users WHERE Username = @Username
END
GO
/****** Object:  StoredProcedure [dbo].[GetRegressionDataById]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRegressionDataById]
@DataId INT
AS
BEGIN
SELECT * FROM RegressionDataTable WHERE Id = @DataId
END
GO
/****** Object:  StoredProcedure [dbo].[GetRegressionDataByUserId]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRegressionDataByUserId]
@UserId INT
AS
BEGIN
SELECT *
FROM dbo.RegressionDataTable WHERE RegressionOwnerId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[GetRegressionDataFile]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRegressionDataFile]
@RegressionDataId INT
AS
BEGIN
SELECT * FROM RegressionFiles WHERE @RegressionDataId = RegressionDataId
END
GO
/****** Object:  StoredProcedure [dbo].[GetSubInfo]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetSubInfo]
@UserId INT
AS
BEGIN
SELECT * FROM SubInfo WHERE UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserById]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserById]
@ID INT
AS
BEGIN
SELECT*FROM Users WHERE Id =@ID
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByUsername]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetUserByUsername]
@Username nvarchar(50)
AS
BEGIN
SELECT Id FROM Users WHERE Username = @Username
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveUser]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveUser]
@ID INT
AS
BEGIN
DELETE FROM Users WHERE Id = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRegressionData]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UpdateRegressionData]
@DataId INT,
@ACoeff decimal(18,0),
@BCoeff decimal(18,0),
@PrecisionError decimal(18,0)
AS
BEGIN
UPDATE RegressionDataTable
SET ACoeff = @ACoeff, BCoeff = @BCoeff, PrecisionError = @PrecisionError
WHERE Id = @DataId
END
GO
/****** Object:  StoredProcedure [dbo].[UploadRegressionFile]    Script Date: 24.05.2021 9:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UploadRegressionFile]
@RegressionDataId int,
@FileName NVARCHAR(500),
@FileData VARBINARY
AS
BEGIN
INSERT INTO dbo.RegressionFiles (RegressionDataId,FileName, FileData)
VALUES (@RegressionDataId, @FileName, @FileData )
END
GO
USE [master]
GO
ALTER DATABASE [FRSData] SET  READ_WRITE 
GO
