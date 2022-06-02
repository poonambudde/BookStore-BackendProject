----Create database
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

----stored procedures for User Api
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