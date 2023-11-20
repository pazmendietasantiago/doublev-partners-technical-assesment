using DoublebPartnes.Core;
using DoublebPartnes.Core.User.Implementation;
using DoublebPartnes.Middleware.Contracts;
using DoublebPartnes.Middleware.Handlers.Response;

namespace DoublebPartnes.Domain.User;

public class UserFacade
{
    private readonly Lazy<UserRepository> _userRepository;

    public UserFacade()
    {
        _userRepository = new Lazy<UserRepository>(() => DVPObjectFactory.Create<UserRepository>());
    }

    public async Task<DVPResponse> CreateUser(UserDTO user)
    {
        var hasUserCreated = await _userRepository.Value.CreateUser(user);

        var statusCode = hasUserCreated
            ? 200
            : 409;

        return new DVPResponse(statusCode, hasUserCreated);
    }

    public async Task<DVPResponse> GetUsers()
    {
        var users = await _userRepository.Value.GetUsers();

        var response = new DVPResponse(200, users);

        return response;
    }

    public async Task<DVPResponse> CreatePerson(PersonDTO person)
    {
        var hasPersonCreated = await _userRepository.Value.CreatePerson(person);

        var statusCode = hasPersonCreated
            ? 200
            : 409;

        return new DVPResponse(statusCode, hasPersonCreated);
    }

    public async Task<DVPResponse> GetPersons()
    {
        var persons = await _userRepository.Value.GetPersons();

        var response = new DVPResponse(200, persons);

        return response;
    }
}