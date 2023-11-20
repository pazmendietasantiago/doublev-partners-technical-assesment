using DoublebPartnes.Core;
using DoublebPartnes.Domain.Security;
using DoublebPartnes.Middleware.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DoublebPartnes.Back.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SecurityController : ControllerBase
{
    private readonly Lazy<SecurityFacade> _facadeSecurity;

    public SecurityController()
    {
        _facadeSecurity = new Lazy<SecurityFacade>(() => DVPObjectFactory.Create<SecurityFacade>());
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO payload)
    {
        var result = await _facadeSecurity.Value.Login(payload);

        return Ok(result);
    }
}