namespace DoublebPartnes.Middleware.Contracts;

public class PersonDTO
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Dni { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string DniType { get; set; } = string.Empty;

    private DateTime? CreationDate { get; set; }

    public string FullIdentification { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;
}