using DoublebPartnes.Core.User.Interface;
using DoublebPartnes.Middleware.Contracts;
using DoublebPartnes.Middleware.MicroORM;

namespace DoublebPartnes.Core.User.Implementation;

public class UserRepository : IUserRepository
{
    private readonly SQLKernel _sqlKernel;

    public UserRepository()
    {
        _sqlKernel = SQLFactory.sqlKernel;
    }

    public async Task<bool> CreateUser(UserDTO user)
    {
        const string query = @"INSERT INTO [Usuarios] (UserName, Password) VALUES (@user, @password)";

        try
        {
            var parameters = new
            {
                user = user.User,
                password = user.Password
            };

            await _sqlKernel.ExecuteAsync(query, parameters);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<IEnumerable<UserDTO>> GetUsers()
    {
        const string query = @"SELECT [UserId] AS [Id], UserName AS [user], [CreationDate]
                                FROM [Usuarios]
                                ORDER BY [Id] DESC";

        try
        {
            var users = await _sqlKernel.SqlQueryAsync<UserDTO>(query);

            return users;
        }
        catch (Exception e)
        {
            return Enumerable.Empty<UserDTO>();
        }
    }

    public async Task<bool> CreatePerson(PersonDTO person)
    {
        const string query =
            @"INSERT INTO [Personas] (FirstName, LastName, IdentificationNumber, Email, IdentificationType) VALUES (@name, @lastName, @dni, @email, @dniType)";

        try
        {
            var parameters = new
            {
                name = person.Name,
                lastName = person.LastName,
                dni = person.Dni,
                email = person.Email,
                dniType = person.DniType
            };

            await _sqlKernel.ExecuteAsync(query, parameters);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<IEnumerable<PersonDTO>> GetPersons()
    {
        const string query = @"SELECT [PersonId]             AS [id],
                                       FirstName              AS [name],
                                       [LastName],
                                       [FullName]             AS [completeName],
                                       [IdentificationType]   AS [dniType],
                                       [IdentificationNumber] AS [dni],
                                       [Email],
                                       FullIdentification     AS [completeIdentification],
                                       [CreationDate]
                                FROM [Personas]
                                ORDER BY [CreationDate] DESC";

        try
        {
            var persons = await _sqlKernel.SqlQueryAsync<PersonDTO>(query);

            return persons;
        }
        catch (Exception e)
        {
            return Enumerable.Empty<PersonDTO>();
        }
    }
}