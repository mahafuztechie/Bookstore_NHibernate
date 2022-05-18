use  BookStore

-------creating Book Table-------
create table Book(
BookId int identity(1,1) not null primary key,
BookName varchar(70) not null,
AuthorName varchar(80) not null,
Rating int,
RatingCount int ,
DiscountPrice int,
ActualPrice int not null,
Description varchar(max) not null,
BookImage varchar(250),
BookQuantity int not null
);

ALTER TABLE Book
DROP COLUMN Rating;

ALTER TABLE Book
ADD Rating float;

ALTER TABLE Book
alter column Rating float(3);

ALTER TABLE Book
ADD CartId int foreign Key references Carts(CartId);

ALTER TABLE Book
DROP COLUMN CartId;

drop Table Book;

select *from Book
insert into book values('java', 'john', 567, 500, 2599, 'java basics', 'java.jpg', 48, 4.3, 1009);

---------Adding Book For STore Procedure------

 create or alter proc Addbook
(
	@BookName varchar(max),
	@AuthorName varchar(80),
	@Rating float,
	@RatingCount int,
	@DiscountPrice int,
	@ActualPrice int,
	@Description varchar(max),
	@BookImage varchar(250),
	@BookQuantity int
)
as
begin
insert into Book (BookName,AuthorName,Rating,RatingCount,DiscountPrice,ActualPrice,Description,BookImage,BookQuantity)
values(@BookName,@AuthorName,@Rating,@RatingCount,@DiscountPrice,@ActualPrice,@Description,@BookImage,@BookQuantity);
end;

-----Store procedure For Upadating Book=----------

create or alter proc Updatebook
(
@BookId int,
@BookName varchar(max),
@AuthorName varchar(250),
@Rating float,
@RatingCount int,
@DiscountPrice int,
@ActualPrice int,
@Description varchar(max),
@BookImage varchar(250),
@BookQuantity int
)
as
begin
update Book set 
BookName=@BookName,
AuthorName=@AuthorName,
Rating=@Rating,
RatingCount=@RatingCount,
DiscountPrice=@DiscountPrice,
ActualPrice=@ActualPrice,
Description=@Description,
BookImage=@BookImage,
BookQuantity=@BookQuantity
where BookId=@BookId			
end;

-----For Deleting Book Store Procedure--------
create proc Deletebook
(
@BookId int
)
as
begin
delete from Book Where BookId=@BookId
end;

---Getting Book By Book Id Store Procedure---
create proc GetBookById
(
@BookId int
)
as 
begin
select * from Book where BookId=@BookId
end;

-----Get All Books -----
create proc GetAllBooks
as 
begin
select * from Book
end;