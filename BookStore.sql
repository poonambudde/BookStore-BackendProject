---Create database
create database BookStore;

---use bookstore database
use BookStore

---create Users table
create table Users
(
UserId int IDENTITY(1,1) PRIMARY KEY,
FullName varchar(255),
Email varchar(255),
Password varchar(255),
MobileNumber bigint
);

select * from Users;

DROP TABLE Users;

---stored procedures for User Api
---Create procedured for User Registration
Create procedure spUserRegister       
(        
    @FullName varchar(255),
    @Email varchar(255),
    @Password varchar(255),
    @MobileNumber bigint       
)        
as         
Begin         
    Insert into Users (FullName,Email,Password,MobileNumber)         
    Values (@FullName,@Email,@Password,@MobileNumber);        
End

---Create procedured for User Login
create procedure spUserLogin
(
@Email varchar(255),
@Password varchar(255)
)
as
begin
select * from Users
where Email = @Email and Password = @Password
End;

---Create procedured for User Forgot Password
create procedure spUserForgotPassword
(
@Email varchar(Max)
)
as
begin
Update Users
set Password = 'Null'
where Email = @Email;
select * from Users where Email = @Email;
End;

---create procedure for user reset password 
create procedure spUserResetPassword
(
@Email varchar(Max),
@Password varchar(Max)
)
AS
BEGIN
UPDATE Users 
SET 
Password = @Password 
WHERE Email = @Email;
End;


---Create book table
create table BookTable(
BookId int identity (1,1)primary key,
BookName varchar(255),
AuthorName varchar(255),
TotalRating int,
RatingCount int,
OriginalPrice decimal,
DiscountPrice decimal,
BookDetails varchar(255),
BookImage varchar(255),
BookQuantity int
);

select *from BookTable;

DROP TABLE BookTable;

---stored procedures for Book Api
---procedured to add book
create procedure SPAddBook
(
@BookName varchar(255),
@AuthorName varchar(255),
@TotalRating int,
@RatingCount int,
@OriginalPrice decimal,
@DiscountPrice decimal,
@BookDetails varchar(255),
@BookImage varchar(255),
@BookQuantity int
)
as
BEGIN
Insert into BookTable(BookName, AuthorName, TotalRating, RatingCount, OriginalPrice, 
DiscountPrice, BookDetails, BookImage, BookQuantity)
values (@BookName, @AuthorName, @TotalRating, @RatingCount ,@OriginalPrice, @DiscountPrice,
@BookDetails, @BookImage, @BookQuantity
);
End;

---create procedure to getbookbybookid
create procedure spGetBookByBookId
(
@BookId int
)
as
BEGIN
select * from BookTable
where BookId = @BookId;
End;

---Procedure to deletebook
create procedure spDeleteBook
(
@BookId int
)
as
BEGIN
Delete BookTable 
where BookId = @BookId;
End;

--procedure to updatebook
create procedure spUpdateBook
(
@BookId int,
@BookName varchar(255),
@AuthorName varchar(255),
@TotalRating int,
@RatingCount int,
@OriginalPrice Decimal,
@DiscountPrice Decimal,
@BookDetails varchar(255),
@BookImage varchar(255),
@BookQuantity int
)
as
BEGIN
Update BookTable set BookName = @BookName, 
AuthorName = @AuthorName,
TotalRating = @TotalRating,
RatingCount = @RatingCount,
OriginalPrice= @OriginalPrice,
DiscountPrice = @DiscountPrice,
BookDetails = @BookDetails,
BookImage =@BookImage,
BookQuantity = @BookQuantity
where BookId = @BookId;
End;

-- create procedure to get all book 
create procedure spGetAllBook
as
BEGIN
	select * from BookTable;
End;


---Create cart table
create Table CartTable
(
CartId int primary key identity(1,1),
BooksQty int,
UserId int Foreign Key References Users(UserId),
BookId int Foreign Key References BookTable(BookId)
);

select * from CartTable;

DROP TABLE CartTable;
---create procedure to addcart
create Procedure spAddCart
(
@BooksQty int,
@UserId int,
@BookId int
)
As
Begin
Insert into CartTable (BooksQty,UserId,BookId) 
values (@BooksQty,@UserId,@BookId);
End;
DROP PROCEDURE spAddCart;
GO

---create procedure to UpdateCart
create procedure spUpdateCart
@BooksQty int,
@CartId int
As
Begin
update CartTable set BooksQty = @BooksQty
where CartId = @CartId
End

---Create procedure to deletecart
create procedure spDeleteCart
@CartId int
As
Begin 
Delete CartTable where CartId = @CartId
End

--create procedure to GetAllBookinCart by UserId
alter procedure spGetAllBookinCart
@UserId int
As
Begin
select CartTable.CartId,CartTable.UserId,CartTable.BookId,CartTable.BooksQty,
BookTable.BookName,BookTable.AuthorName,BookTable.TotalRating,BookTable.RatingCount,BookTable.OriginalPrice,BookTable.DiscountPrice,BookTable.BookDetails,BookTable.BookImage,BookTable.BookQuantity 
from CartTable inner join BookTable on CartTable.BookId = BookTable.BookId
where CartTable.UserId = @UserId
End



---create wishlist table
create Table WishlistTable
(
WishlistId int identity(1,1) primary key,
UserId INT FOREIGN KEY REFERENCES Users(UserId),
BookId INT FOREIGN KEY REFERENCES BookTable(BookId)
);

select * from WishlistTable

---create procedure to Add in Wishlist
create procedure spAddInWishlist
@UserId int,
@BookId int
As
Begin
Insert Into WishlistTable (UserId,BookId) values (@UserId,@BookId)
End
DROP PROCEDURE spAddInWishlist;
GO

--create procedure to delete from wishlist
create procedure spDeleteFromWishlist
@WishListId int
As
Begin
Delete WishlistTable where WishListId=@WishListId
End

---create procedure to get all Books from wishlist
create procedure spGetAllBooksinWishList
@UserId int
As
Begin
select WishlistTable.WishListId,WishlistTable.UserId,WishlistTable.BookId,
BookTable.BookName,BookTable.AuthorName,BookTable.TotalRating,BookTable.RatingCount,BookTable.OriginalPrice,BookTable.DiscountPrice,BookTable.BookDetails,BookTable.BookImage,BookTable.BookQuantity 
from WishlistTable inner join BookTable on WishlistTable.BookId=BookTable.BookId
where WishlistTable.userId=@UserId
End



---create address type table
create Table AddressTypeTable
(
	TypeId INT IDENTITY(1,1) PRIMARY KEY,
	AddressType varchar(255)
);
select * from AddressTypeTable
DROP TABLE AddressTypeTable;

---insert record for addresstype table
insert into AddressTypeTable values('Home'),('Office'),('Other');

---create address table
create Table AddressTable
(
AddressId INT IDENTITY(1,1) PRIMARY KEY,
Address varchar(255),
City varchar(100),
State varchar(100),
TypeId int 
FOREIGN KEY (TypeId) REFERENCES AddressTypeTable(TypeId),
UserId INT FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
select * from AddressTable
DROP TABLE AddressTable; 

---create procedure to AddAddress
--- Procedure To Add Address
create procedure spAddAddress
(
@Address varchar(max),
@City varchar(100),
@State varchar(100),
@TypeId int,
@UserId int
)
as
BEGIN
If Exists (select * from AddressTypeTable where TypeId = @TypeId)
begin
Insert into AddressTable 
values(@Address, @City, @State, @TypeId, @UserId);
end
Else
begin
select 2
end
End;

--create procedure for updateAddress
create procedure spUpdateAddress
(
	@AddressId int,
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int
)
as
BEGIN
If Exists (select * from AddressTypeTable where TypeId = @TypeId)
begin
Update AddressTable set
Address = @Address, City = @City,
State = @State , TypeId = @TypeId
where AddressId = @AddressId
end
Else
begin
select 2
end
End;

--create procedure to delete address
create Procedure spDeleteAddress
(
@AddressId int
)
as
BEGIN
Delete AddressTable where AddressId = @AddressId 
End;

-- Procedure To Get All Address By UserId
create Procedure spGetAddressByUserId
(
@UserId int
)
as
BEGIN
Select Address, City, State,a1.UserId, a2.TypeId
from AddressTable a1
Inner join AddressTypeTable a2 on a2.TypeId = a1.TypeId 
where UserId = @UserId;
END;

-- Procedure To Get All Address
create Procedure spGetAllAddress
As
Begin
select * from AddressTable
End