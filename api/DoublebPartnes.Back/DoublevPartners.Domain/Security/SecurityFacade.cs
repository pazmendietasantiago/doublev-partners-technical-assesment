using DoublebPartnes.Core;
using DoublebPartnes.Core.Security.Implementation;
using DoublebPartnes.Middleware.Contracts;
using DoublebPartnes.Middleware.Handlers.Response;
using DoublebPartnes.Middleware.Utils;

namespace DoublebPartnes.Domain.Security;

public class SecurityFacade
{
    private readonly Lazy<SecurityRepository> _repositorySecurity;

    public SecurityFacade()
    {
        _repositorySecurity = new Lazy<SecurityRepository>(() => DVPObjectFactory.Create<SecurityRepository>());
    }

    public async Task<DVPResponse> Login(LoginDTO payload)
    {
        bool isValidUser = await _repositorySecurity
            .Value
            .Login(
                StringUtils.ConvertToStringFromBase64(payload.UserName), 
                StringUtils.ConvertToStringFromBase64(payload.Password));

        int statusCode = isValidUser 
            ? 200 
            : 403;
        
        var response = new DVPResponse(statusCode, isValidUser);
            
        return response;
    }
}