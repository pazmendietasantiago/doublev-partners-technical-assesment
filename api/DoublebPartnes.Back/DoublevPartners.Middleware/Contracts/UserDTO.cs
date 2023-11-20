namespace DoublebPartnes.Middleware.Contracts;

public class UserDTO
{
    public int Id { get; set; } = 0;

    public string User { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public DateTime? CreationDate { get; set; }
}