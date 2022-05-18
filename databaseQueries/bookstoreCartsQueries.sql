use  BookStore
--cReating Cart TAble-----

create table Carts
(
	CartId int identity(1,1) not null primary key,
	OrderQuantity int default 1,
	UserId int foreign key references Users(UserId) on delete no action,
	BookId int foreign key references Book(BookId) on delete no action
);
select * from Carts

drop table Carts;

-----Adding  to Cart Stored Procedure-----

create or alter proc AddCart
(
@OrderQuantity int,
@UserId int,
@BookId int
)
as
begin 
		if(exists(select * from Book where BookId=@BookId))
		begin
		insert into Carts(OrderQuantity,UserId,BookId)
		values(@OrderQuantity,@UserId,@BookId)
		end
		else
		begin
		select 1
		end
end;
------GetCart bY USErid Stored Procedure-----

create proc GetCartByUserId
(
	@UserId int
)
as
begin
	select CartId,OrderQuantity,UserId,c.BookId,BookName,AuthorName,
	DiscountPrice,ActualPrice,BookImage from Carts c join Book b on c.BookId=b.BookId 
	where UserId=@UserId;
end;

----Update Cart Stored Procedure-----

create proc UpdateCart
(
	@OrderQuantity int,
	@BookId int,
	@UserId int,
	@CartId int
)
as
begin
update Carts set BookId=@BookId,
				UserId=@UserId,
				OrderQuantity=@OrderQuantity
				where CartId=@CartId;
end;


-----Delete Cart  by cartID ANd BookId Stored Procedure-----


create proc DeleteCart
(
	@CartId int,
	@UserId int
)
as
begin
delete Carts where
		CartId=@CartId and UserId=@UserId;
end;