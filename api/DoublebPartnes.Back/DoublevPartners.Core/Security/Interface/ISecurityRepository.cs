namespace DoublebPartnes.Core.Security.Interface;

public interface ISecurityRepository
{
    public Task<bool> Login(string userName, string password);
    
    public Task<bool> Logout(string token);
}