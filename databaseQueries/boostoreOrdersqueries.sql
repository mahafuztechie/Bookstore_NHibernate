use BookStore

---Creating Order Table ------
create table Orders(
	OrderId int identity(1,1) not null primary key,
	TotalPrice int not null,
	OrderBookQuantity int not null,
	OrderDate Date not null,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references Book(BookId),
	AddressId int not null foreign key (AddressId) references Address(AddressId)
);

select *from Orders
