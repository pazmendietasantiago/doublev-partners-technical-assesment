using DoublebPartnes.Middleware.Contracts;

namespace DoublebPartnes.Core.User.Interface;

public interface IUserRepository
{
    public Task<bool> CreateUser(UserDTO user);

    public Task<IEnumerable<UserDTO>> GetUsers();

    public Task<bool> CreatePerson(PersonDTO person);

    public Task<IEnumerable<PersonDTO>> GetPersons();
}