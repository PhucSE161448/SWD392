USE [master]
GO
/****** Object:  Database [MixFood]    Script Date: 27/02/2024 23:15:00 ******/
CREATE DATABASE [MixFood]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MixFood', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MixFood.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MixFood_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MixFood_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MixFood] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MixFood].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MixFood] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MixFood] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MixFood] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MixFood] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MixFood] SET ARITHABORT OFF 
GO
ALTER DATABASE [MixFood] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MixFood] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MixFood] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MixFood] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MixFood] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MixFood] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MixFood] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MixFood] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MixFood] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MixFood] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MixFood] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MixFood] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MixFood] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MixFood] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MixFood] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MixFood] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MixFood] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MixFood] SET RECOVERY FULL 
GO
ALTER DATABASE [MixFood] SET  MULTI_USER 
GO
ALTER DATABASE [MixFood] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MixFood] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MixFood] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MixFood] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MixFood] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MixFood] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MixFood', N'ON'
GO
ALTER DATABASE [MixFood] SET QUERY_STORE = ON
GO
ALTER DATABASE [MixFood] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MixFood]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Username] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[Role] [varchar](255) NOT NULL,
	[Status] [varchar](255) NOT NULL,
	[Phone] [varchar](255) NOT NULL,
	[Avatar] [varchar](255) NULL,
	[CreatedDate] [date] NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[DeletedDate] [date] NULL,
	[DeletedBy] [nvarchar](255) NULL,
	[IsDeleted] [bit] NOT NULL,
	[ConfirmationToken] [varchar](max) NULL,
 CONSTRAINT [account_id_primary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] NOT NULL,
	[Name] [varchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](255) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](255) NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [varchar](255) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [category_id_primary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredient]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredient](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Price] [decimal](8, 2) NOT NULL,
	[Calo] [int] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[Image_Url] [nvarchar](255) NOT NULL,
	[IngredientType_id] [int] NOT NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[ModifiedDate] [date] NULL,
	[DeletedBy] [nvarchar](255) NULL,
	[DeletedDate] [date] NULL,
	[IsDeleted] [bigint] NOT NULL,
 CONSTRAINT [ingredient_id_primary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IngredientProduct]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IngredientProduct](
	[Product_id] [int] NOT NULL,
	[Ingredient_id] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ingredient_id] ASC,
	[Product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IngredientSession]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IngredientSession](
	[Session_Id] [int] NOT NULL,
	[Ingredient_Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Session_Id] ASC,
	[Ingredient_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IngredientType]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IngredientType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[ModifiedDate] [date] NULL,
	[DeletedBy] [nvarchar](255) NULL,
	[DeletedDate] [date] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [ingredienttype_id_primary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IngredientType_TemplateStep]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IngredientType_TemplateStep](
	[IngredientType_Id] [int] NOT NULL,
	[TemplateStep_Id] [int] NOT NULL,
	[Quantity_Min] [int] NOT NULL,
	[Quantity_Max] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IngredientType_Id] ASC,
	[TemplateStep_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[Id] [int] NOT NULL,
	[Account_Id] [int] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[Image] [varchar](255) NOT NULL,
	[Status] [varchar](255) NOT NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[ModifiedDate] [date] NULL,
	[DeletedBy] [nvarchar](255) NULL,
	[DeletedDate] [date] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [news_id_primary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] NOT NULL,
	[Payment_method_id] [int] NOT NULL,
	[Account_id] [int] NOT NULL,
	[Status] [varchar](255) NOT NULL,
	[Create_date] [datetime] NOT NULL,
	[Total_price] [float] NOT NULL,
	[Store_id] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[Product_Id] [int] NOT NULL,
 CONSTRAINT [order_id_primary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[Id] [int] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Payment_type] [varchar](255) NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [payment_id_primary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Price] [decimal](8, 2) NOT NULL,
	[CreatedDate] [date] NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[DeletedDate] [date] NULL,
	[DeletedBy] [nvarchar](255) NULL,
	[IsDeleted] [bit] NOT NULL,
	[ProductTemplate_Id] [int] NOT NULL,
 CONSTRAINT [product_id_primary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductTemplate]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTemplate](
	[Id] [int] NOT NULL,
	[Category_Id] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Size] [varchar](255) NOT NULL,
	[Image_url] [nvarchar](255) NOT NULL,
	[Price] [decimal](8, 2) NOT NULL,
	[Status] [nvarchar](255) NOT NULL,
	[Store_id] [int] NOT NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[DeletedBy] [nvarchar](255) NULL,
	[DeletedDate] [date] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [producttemplate_id_primary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[Id] [int] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Ingredient_Id] [int] NOT NULL,
	[Order_Id] [int] NOT NULL,
	[Store_Id] [int] NOT NULL,
	[Start_Time] [time](7) NOT NULL,
	[End_Time] [time](7) NOT NULL,
	[Is_Delete] [bit] NOT NULL,
 CONSTRAINT [session_id_primary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Store]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [store_id_primary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TemplateStep]    Script Date: 27/02/2024 23:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemplateStep](
	[Id] [int] NOT NULL,
	[ProuctTemplate_Id] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[ModifiedDate] [date] NULL,
	[DeletedBy] [nvarchar](255) NULL,
	[DeletedDate] [date] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [templatestep_id_primary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Ingredient] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[IngredientType] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[News] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT ('0') FOR [IsDelete]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT ('0') FOR [IsDelete]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ProductTemplate] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Session] ADD  DEFAULT ('0') FOR [Is_Delete]
GO
ALTER TABLE [dbo].[Store] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TemplateStep] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Ingredient]  WITH CHECK ADD  CONSTRAINT [ingredient_ingredienttype_id_foreign] FOREIGN KEY([IngredientType_id])
REFERENCES [dbo].[IngredientType] ([Id])
GO
ALTER TABLE [dbo].[Ingredient] CHECK CONSTRAINT [ingredient_ingredienttype_id_foreign]
GO
ALTER TABLE [dbo].[IngredientProduct]  WITH CHECK ADD FOREIGN KEY([Ingredient_id])
REFERENCES [dbo].[Ingredient] ([Id])
GO
ALTER TABLE [dbo].[IngredientProduct]  WITH CHECK ADD FOREIGN KEY([Product_id])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[IngredientSession]  WITH CHECK ADD FOREIGN KEY([Ingredient_Id])
REFERENCES [dbo].[Ingredient] ([Id])
GO
ALTER TABLE [dbo].[IngredientSession]  WITH CHECK ADD FOREIGN KEY([Session_Id])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[IngredientType_TemplateStep]  WITH CHECK ADD  CONSTRAINT [IngredientType_templateStep_ingredienttype_id_foreign] FOREIGN KEY([IngredientType_Id])
REFERENCES [dbo].[IngredientType] ([Id])
GO
ALTER TABLE [dbo].[IngredientType_TemplateStep] CHECK CONSTRAINT [IngredientType_templateStep_ingredienttype_id_foreign]
GO
ALTER TABLE [dbo].[IngredientType_TemplateStep]  WITH CHECK ADD  CONSTRAINT [ingredienttype_templatestep_templatestep_id_foreign] FOREIGN KEY([TemplateStep_Id])
REFERENCES [dbo].[TemplateStep] ([Id])
GO
ALTER TABLE [dbo].[IngredientType_TemplateStep] CHECK CONSTRAINT [ingredienttype_templatestep_templatestep_id_foreign]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [news_account_id_foreign] FOREIGN KEY([Account_Id])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[News] CHECK CONSTRAINT [news_account_id_foreign]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_ProductID] FOREIGN KEY([Product_Id])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_ProductID]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [order_account_id_foreign] FOREIGN KEY([Account_id])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [order_account_id_foreign]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [order_payment_method_id_foreign] FOREIGN KEY([Payment_method_id])
REFERENCES [dbo].[Payment] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [order_payment_method_id_foreign]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [order_store_id_foreign] FOREIGN KEY([Store_id])
REFERENCES [dbo].[Store] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [order_store_id_foreign]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_ProductTemplate_ID] FOREIGN KEY([ProductTemplate_Id])
REFERENCES [dbo].[ProductTemplate] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_ProductTemplate_ID]
GO
ALTER TABLE [dbo].[ProductTemplate]  WITH CHECK ADD  CONSTRAINT [producttemplate_categoryid_foreign] FOREIGN KEY([Category_Id])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[ProductTemplate] CHECK CONSTRAINT [producttemplate_categoryid_foreign]
GO
ALTER TABLE [dbo].[ProductTemplate]  WITH CHECK ADD  CONSTRAINT [producttemplate_store_id_foreign] FOREIGN KEY([Store_id])
REFERENCES [dbo].[Store] ([Id])
GO
ALTER TABLE [dbo].[ProductTemplate] CHECK CONSTRAINT [producttemplate_store_id_foreign]
GO
ALTER TABLE [dbo].[TemplateStep]  WITH CHECK ADD  CONSTRAINT [templatestep_proucttemplate_id_foreign] FOREIGN KEY([ProuctTemplate_Id])
REFERENCES [dbo].[ProductTemplate] ([Id])
GO
ALTER TABLE [dbo].[TemplateStep] CHECK CONSTRAINT [templatestep_proucttemplate_id_foreign]
GO
USE [master]
GO
ALTER DATABASE [MixFood] SET  READ_WRITE 
GO
