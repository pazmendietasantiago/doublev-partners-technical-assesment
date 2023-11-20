create table Usuarios
(
    UserId       int identity
        primary key,
    UserName     nvarchar(50)  not null,
    Password     nvarchar(max) not null,
    CreationDate datetime default getdate()
);
