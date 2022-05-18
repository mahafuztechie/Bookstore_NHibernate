create database BookStore

use BookStore

-----create table for user----

create table Users(
UserId int identity(1,1)not null primary key,
FullName varchar(max) not null,
Email varchar(max) not null,
Password varchar(max) not null,
MobileNumber varchar(20) not null
);


select * from Users
-----creating store procedure for user table----

create Proc spRegisterUser
(
	@FullName Varchar(Max),
	@Email varchar(Max),
	@Password varchar(Max),
	@MobileNumber varchar(30) 
)
as
begin
		Insert Into Users (FullName, Email, Password, MobileNumber)
		Values (@FullName, @Email, @Password, @MobileNumber);
		
End;

------User Login Stored Procedure-------

create Proc spUserLogin
(
	@Email varchar(Max),
	@Password varchar(Max)
)
as
begin
	select * from Users
	where
		Email=@Email
		and
		Password=@Password
end;

create proc spUserForgotPassword
(
	@Email varchar(Max)
)
as
begin
	update Users
	set 
		Password ='Null'
	where 
		Email = @Email;
    select * from Users where Email = @Email;
end;

-----Reset Password Stored Procedure-----

create proc spUserResetPassword
(
	@Email varchar(Max),
	@Password varchar(Max)
)
AS
BEGIN
	update Users 
	SET 
		Password = @Password 
	where
		Email = @Email;
end;
