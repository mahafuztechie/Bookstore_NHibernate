use  BookStore

----Creating Address Table------
create Table Address
(
	AddressId int identity(1,1) primary key,
	Address varchar(max) not null,
	City varchar(100) not null,
	State varchar(100) not null,
	TypeId int not null 
	FOREIGN KEY (TypeId) REFERENCES AddressType(TypeId),
	UserId INT not null
	FOREIGN KEY (UserId) REFERENCES Users(UserId),
);

--Creating Type Address Table-----
create Table AddressType
(
	TypeId int identity(1,1) not null primary key,
	TypeName varchar(255) not null
);
Insert into AddressType
values('Home'),('Office'),('Other');

select *from AddressType
select *from Address

---adding address Store Procedure---

create or alter proc AddAddress
(
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
)
as
BEGIN
	If exists(select * from AddressType where TypeId=@TypeId)
		begin
			Insert into Address
			values(@Address, @City, @State, @TypeId, @UserId);
		end
	Else
		begin
			select 2
		end
end;

----Get All Addresss By UserID------Store Procedure----

create or alter Proc GetAllAddresses
(
	@UserId int
)
as
begin
	select Address, City, State, a.UserId, b.TypeId
	from Address a
    Inner join AddressType b on b.TypeId = a.TypeId 
	where 
	UserId = @UserId;
end;


----Update Address Store Procedure----
create or alter proc UpdateAddress
(
	@AddressId int,
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
)
as
BEGIN
	If exists(select * from AddressType where TypeId = @TypeId)
		begin
			Update Address set
			Address = @Address, City = @City,
			State = @State, TypeId = @TypeId,
			UserId = @UserId
			where
				AddressId = @AddressId
		end
	Else
		begin
			select 2
		end
end;
---delete Address By Addre ID ANd User Id----
create or alter Proc DeleteAddress
(
	@AddressId int,
	@UserId int
)
as
begin
	Delete Address
	where 
		AddressId=@AddressId and UserId=@UserId;
end;