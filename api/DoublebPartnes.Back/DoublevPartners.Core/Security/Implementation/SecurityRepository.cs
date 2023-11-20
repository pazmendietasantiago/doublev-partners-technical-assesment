using DoublebPartnes.Core.Security.Interface;
using DoublebPartnes.Middleware.MicroORM;

namespace DoublebPartnes.Core.Security.Implementation;

public class SecurityRepository : ISecurityRepository
{
    private readonly SQLKernel _sqlKernel;

    public SecurityRepository()
    {
        _sqlKernel = SQLFactory.sqlKernel;
    }

    public async Task<bool> Login(string userName, string password)
    {
        const string query = @"IF (EXISTS(SELECT [UserId]
                                           FROM [Usuarios]
                                           WHERE [UserName] = @userName
                                             AND [Password] = @password))
                                    SELECT 1
                                ELSE
                                    SELECT 0";

        var parameters = new
        {
            userName,
            password
        };

        var isValidUser = await _sqlKernel.QuerySingleAsync<bool>(query, parameters);

        return isValidUser;
    }

    public async Task<bool> Logout(string token)
    {
        const string query = @"";

        try
        {
            await _sqlKernel.ExecuteAsync(query, new { token });

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}