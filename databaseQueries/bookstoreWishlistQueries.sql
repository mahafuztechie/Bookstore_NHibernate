use  BookStore

---Table For Wishlist-----
create table Wishlist
(
	WishlistId int identity(1,1)not null primary key,
	UserId int not null foreign key references Users(UserId) on delete no action,
	bookId int not null foreign key references Book(bookId) on delete no action
);

select *from Wishlist