namespace DoublebPartnes.Middleware.Handlers.Response;

public record DVPResponse
{
    public DVPResponse(int statusCode, object payload, string error = "")
    {
        StatusCode = statusCode;
        Payload = payload;
        Error = error;
    }

    public int StatusCode { get; init; }

    public object? Payload { get; init; }

    public string Error { get; init; }
}