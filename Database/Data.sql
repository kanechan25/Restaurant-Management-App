USE [master]
GO
/****** Object:  Database [RestaurantManagement]    Script Date: 09/16/2020 9:00:16 PM ******/
CREATE DATABASE [RestaurantManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RestaurantManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.KANE\MSSQL\DATA\RestaurantManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RestaurantManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.KANE\MSSQL\DATA\RestaurantManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RestaurantManagement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RestaurantManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RestaurantManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RestaurantManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RestaurantManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RestaurantManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RestaurantManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [RestaurantManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RestaurantManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RestaurantManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RestaurantManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RestaurantManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RestaurantManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RestaurantManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RestaurantManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RestaurantManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RestaurantManagement] SET  ENABLE_BROKER 
GO
ALTER DATABASE [RestaurantManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RestaurantManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RestaurantManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RestaurantManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RestaurantManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RestaurantManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RestaurantManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RestaurantManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [RestaurantManagement] SET  MULTI_USER 
GO
ALTER DATABASE [RestaurantManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RestaurantManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RestaurantManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RestaurantManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RestaurantManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RestaurantManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'RestaurantManagement', N'ON'
GO
ALTER DATABASE [RestaurantManagement] SET QUERY_STORE = OFF
GO
USE [RestaurantManagement]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 09/16/2020 9:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[UserName] [nvarchar](100) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[PassWord] [nvarchar](1000) NOT NULL,
	[Type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 09/16/2020 9:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DateCheckIn] [date] NOT NULL,
	[DateCheckOut] [date] NOT NULL,
	[idTable] [int] NOT NULL,
	[totalPrice] [float] NOT NULL,
	[discount] [int] NOT NULL,
	[status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillInfo]    Script Date: 09/16/2020 9:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idBill] [int] NOT NULL,
	[idFood] [int] NOT NULL,
	[count] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food]    Script Date: 09/16/2020 9:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[idCategory] [int] NOT NULL,
	[price] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodCategory]    Script Date: 09/16/2020 9:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TableFood]    Script Date: 09/16/2020 9:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableFood](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[status] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Type]) VALUES (N'1', N'Anonymous', N'1', 1)
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Type]) VALUES (N'jackma', N'Jack Ma', N'alibaba', 1)
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Type]) VALUES (N'khoa', N'Tran Van Khoa', N'tranvankhoa', 0)
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Type]) VALUES (N'linhvien', N'Tran Thuy Linh', N'linhlinh', 0)
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Type]) VALUES (N'tonyma', N'Ma Hoa Dang', N'tencent', 0)
GO
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([id], [DateCheckIn], [DateCheckOut], [idTable], [totalPrice], [discount], [status]) VALUES (1, CAST(N'2021-09-16' AS Date), CAST(N'2021-09-16' AS Date), 2, 1704000, 20, 1)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DateCheckOut], [idTable], [totalPrice], [discount], [status]) VALUES (2, CAST(N'2021-09-16' AS Date), CAST(N'2021-09-16' AS Date), 7, 652500, 25, 1)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DateCheckOut], [idTable], [totalPrice], [discount], [status]) VALUES (3, CAST(N'2021-09-16' AS Date), CAST(N'2021-09-16' AS Date), 9, 180000, 0, 1)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DateCheckOut], [idTable], [totalPrice], [discount], [status]) VALUES (4, CAST(N'2021-09-16' AS Date), CAST(N'2021-09-16' AS Date), 2, 660000, 0, 1)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DateCheckOut], [idTable], [totalPrice], [discount], [status]) VALUES (5, CAST(N'2021-09-16' AS Date), CAST(N'2021-09-16' AS Date), 9, 970000, 50, 1)
SET IDENTITY_INSERT [dbo].[Bill] OFF
GO
SET IDENTITY_INSERT [dbo].[BillInfo] ON 

INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (1, 1, 1, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (2, 1, 11, 3)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (3, 1, 18, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (5, 2, 14, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (6, 2, 19, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (7, 2, 21, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (8, 2, 24, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (10, 3, 7, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (11, 3, 18, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (12, 3, 6, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (13, 4, 6, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (14, 4, 20, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (15, 4, 4, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (16, 5, 4, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (17, 5, 5, 3)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (18, 5, 23, 2)
SET IDENTITY_INSERT [dbo].[BillInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[Food] ON 

INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (1, N'Muc mot nang nuong sate', 1, 300000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (2, N'Tom hum hoang de chien bo', 1, 2000000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (3, N'Ca thu tuoi xot me', 1, 350000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (4, N'Canh hau chan chau', 1, 250000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (5, N'Oc Asian xao rong bien', 1, 450000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (6, N'Rau cu qua luoc', 2, 45000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (7, N'Su hao xao thit', 2, 75000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (8, N'Canh nam huong ngu qua', 2, 120000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (9, N'Rau muong luoc', 2, 25000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (10, N'Doi bo trong trung', 3, 150000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (11, N'Thit trau gac bep', 3, 500000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (12, N'Nhong ong xao rau rung', 3, 250000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (13, N'Canh ca keo', 3, 150000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (14, N'Thit bo xao nam', 4, 150000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (15, N'Nam de nuong sa 3333333', 4, 450000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (16, N'Trau luoc ca con', 4, 5000000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (17, N'Thit lon rang chay xem', 4, 150000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (18, N'Caramen', 5, 15000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (19, N'Cake', 5, 25000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (20, N'Sweetsoup', 5, 35000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (21, N'Orange', 6, 250000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (22, N'Apple', 6, 55000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (23, N'Watermelon', 6, 45000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (24, N'Nuoc suoi', 7, 10000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (25, N'Cocacola', 7, 20000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (26, N'Bia Heineken', 7, 30000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (27, N'Tra xanh 0 do', 7, 25000)
SET IDENTITY_INSERT [dbo].[Food] OFF
GO
SET IDENTITY_INSERT [dbo].[FoodCategory] ON 

INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (1, N'Seafood')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (2, N'Vegetable')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (3, N'Cuisine')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (4, N'Meat-based')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (5, N'Dessert')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (6, N'Fruit')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (7, N'Beverage')
SET IDENTITY_INSERT [dbo].[FoodCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[TableFood] ON 

INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (1, N'Table 1', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (2, N'Table 2', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (3, N'Table 3', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (4, N'Table 4', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (5, N'Table 5', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (6, N'Table 6', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (7, N'Table 7', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (8, N'Table 8', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (9, N'Table 9', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (10, N'Table 10', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (11, N'Table 11', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (12, N'Table 12', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (13, N'Table 13', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (14, N'Table 14', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (15, N'Table 15', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (16, N'Table 16', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (17, N'Table 17', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (18, N'Table 18', N'Empty')
SET IDENTITY_INSERT [dbo].[TableFood] OFF
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT (N'1') FOR [PassWord]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT (getdate()) FOR [DateCheckIn]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT (getdate()) FOR [DateCheckOut]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [totalPrice]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [discount]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[BillInfo] ADD  DEFAULT ((0)) FOR [count]
GO
ALTER TABLE [dbo].[Food] ADD  DEFAULT (N'No Name') FOR [name]
GO
ALTER TABLE [dbo].[Food] ADD  DEFAULT ((0)) FOR [price]
GO
ALTER TABLE [dbo].[FoodCategory] ADD  DEFAULT (N'No Name') FOR [name]
GO
ALTER TABLE [dbo].[TableFood] ADD  DEFAULT (N'the table no name') FOR [name]
GO
ALTER TABLE [dbo].[TableFood] ADD  DEFAULT ('Empty') FOR [status]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([idTable])
REFERENCES [dbo].[TableFood] ([id])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idBill])
REFERENCES [dbo].[Bill] ([id])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idFood])
REFERENCES [dbo].[Food] ([id])
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD FOREIGN KEY([idCategory])
REFERENCES [dbo].[FoodCategory] ([id])
GO
/****** Object:  StoredProcedure [dbo].[USP_GetListBillByDate]    Script Date: 09/16/2020 9:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC USP_UpdateAccount
@userName nvarchar(100),
@displayName nvarchar(100),
@passWord nvarchar(100),
@newPassword nvarchar(100)
AS
BEGIN
	declare @isRightPass int = 0

	select @isRightPass = count(*) from dbo.account where UserName = @userName and PassWord = @passWord

	if (@isRightPass = 1)
	begin
		if (@newPassword = null or @newPassword = '')
		begin
			update dbo.account set DisplayName = @displayName where UserName = @userName
		end
		else
			update dbo.account set DisplayName = @displayName, PassWord = @passWord where UserName = @userName
	end
END
GO


create proc [dbo].[USP_GetListBillByDate]
@checkIn date, @checkOut date
AS
BEGIN
	SELECT t.name as [Name Table], b.totalPrice as [Total Money (Vnd)], b.DateCheckIn, b.DateCheckOut, b.discount [Discount (%)]
		FROM dbo.Bill as b, dbo.TableFood as t
			where b.DateCheckIn >= @checkIn AND b.DateCheckOut <= @checkOut and b.status = 1
				and t.id = b.idTable
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_GetTableList]    Script Date: 09/16/2020 9:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Add Table
		DECLARE @j INT = 1
		WHILE @j<=10
		BEGIN
			UPDATE TableFood SET id = @j WHERE name = N'Table ' + CAST (@j AS nvarchar(100))
			SET @j = @j + 1
		END
SELECT * FROM dbo.TableFood
DELETE dbo.TableFood
GO*/

CREATE PROC [dbo].[USP_GetTableList]
AS SELECT * FROM dbo.TableFood
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBill]    Script Date: 09/16/2020 9:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_InsertBill]
@idTable INT
AS
BEGIN
	INSERT INTO dbo.Bill(idTable, status, discount) VALUES (@idTable, 0 , 0)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBillInfo]    Script Date: 09/16/2020 9:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*CREATE PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
BEGIN
	INSERT INTO dbo.BillInfo(idBill , idFood , count) VALUES (@idBill , @idFood , @count )

END
GO*/

--DROP PROCEDURE [USP_InsertBillInfo];  
--GO 

CREATE PROC [dbo].[USP_InsertBillInfo]
@idBill INT, @idFood INT, @count INT
AS
BEGIN
	DECLARE @isExitsBillInfo INT;
	DECLARE @foodcount INT = 1
	SELECT @isExitsBillInfo = id, @foodcount = bi.count FROM dbo.BillInfo AS bi WHERE idBill = @idBill AND idFood = @idFood
	IF (@isExitsBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @foodCount + @count
		IF (@newCount > 0)
			UPDATE dbo.BillInfo SET count = @foodcount + @count WHERE idFood = @idFood
		ELSE
			DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
	END
	ELSE
	BEGIN
		INSERT INTO dbo.BillInfo(idBill , idFood , count) VALUES (@idBill , @idFood , @count )
	END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Login]    Script Date: 09/16/2020 9:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_Login]
@userName nvarchar(100),
@passWord nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWord = @passWord
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SwitchTable]    Script Date: 09/16/2020 9:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_SwitchTable]

@idTable1 int, @idTable2 int
AS BEGIN
	declare @idFirstBill int
	declare	@idSecondBill int
	select @idFirstBill = id from dbo.Bill where idTable = @idTable1 and status = 0
	select @idSecondBill = id from dbo.Bill where idTable = @idTable2 and status = 0
	if (@idFirstBill is null)
	BEGIN
		insert dbo.Bill(idTable , status) values (@idTable1, 0)
		select @idFirstBill = max(id) from dbo.Bill where idTable = @idTable1 and status = 0
		update dbo.TableFood set
		status = N'Empty' where id = @idTable1

	END

	if (@idSecondBill is null)
	BEGIN
		insert dbo.Bill(idTable , status) values (@idTable2, 0)
		select @idSecondBill = max(id) from dbo.Bill where idTable = @idTable2 and status = 0
		update dbo.TableFood set
		status = N'Empty' where id = @idTable2
	END
	select id into IDBillInfoTable from dbo.BillInfo where idBill = @idSecondBill
	update dbo.BillInfo set idBill = @idSecondBill where idBill = @idFirstBill
	update dbo.BillInfo set idBill = @idFirstBill where id in (select * from IDBillInfoTable)
	drop table IDBillInfoTable
END
GO
/****** Object:  StoredProcedure [dbo].[USP_UpdateAccount]    Script Date: 09/16/2020 9:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[USP_UpdateAccount]
@userName nvarchar(100), @displayName nvarchar(100), @passWord nvarchar(100), @newPassword nvarchar(100)
AS
BEGIN
	declare @isRightPass int
	select @isRightPass = count(*) from dbo.Account where UserName = @userName and PassWord = @passWord
	if (@isRightPass = 1)
	BEGIN
		IF (@newPassword = null or @newPassword = '')
		BEGIN
			update dbo.Account set DisplayName = @displayName where UserName = @userName
		END
		ELSE
			update dbo.Account set DisplayName = @displayName, PassWord = @newPassword where UserName = @userName
	END

END
GO
USE [master]
GO
ALTER DATABASE [RestaurantManagement] SET  READ_WRITE 
GO
