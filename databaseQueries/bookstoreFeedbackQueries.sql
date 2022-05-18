use BookStore

--- Table For Feedback----
create Table Feedback
(
    FeedbackId int identity(1,1) not null primary key,
    Comment varchar(max) not null,
    Rating float(3) not null,
    UserId int not null foreign key references Users(UserId) on delete no action,
    BookId int not null foreign key references Book(BookId) on delete no action
);
select *from Feedback