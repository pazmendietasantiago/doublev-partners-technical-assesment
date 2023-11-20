using DoublebPartnes.Core;
using DoublebPartnes.Domain.User;
using DoublebPartnes.Middleware.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DoublebPartnes.Back.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly Lazy<UserFacade> _facadeUser;

    public UserController()
    {
        _facadeUser = new Lazy<UserFacade>(() => DVPObjectFactory.Create<UserFacade>());
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
    {
        var result = await _facadeUser.Value.CreateUser(user);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _facadeUser.Value.GetUsers();

        return Ok(result);
    }

    [HttpPost]
    [Route("person")]
    public async Task<IActionResult> CreatePerson([FromBody] PersonDTO person)
    {
        var result = await _facadeUser.Value.CreatePerson(person);

        return Ok(result);
    }

    [HttpGet]
    [Route("person")]
    public async Task<IActionResult> GetPersons()
    {
        var result = await _facadeUser.Value.GetPersons();

        return Ok(result);
    }
}