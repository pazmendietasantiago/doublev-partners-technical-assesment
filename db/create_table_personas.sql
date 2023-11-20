create table Personas
(
    PersonId             int identity
        primary key,
    FirstName            varchar(50)   not null,
    LastName             varchar(50)   not null,
    IdentificationNumber nvarchar(20)  not null,
    Email                nvarchar(100) not null,
    IdentificationType   nvarchar(20)  not null,
    CreationDate         datetime default getdate(),
    FullIdentification   as [IdentificationType] + ' ' + [IdentificationNumber],
    FullName             as [FirstName] + ' ' + [LastName]
);
