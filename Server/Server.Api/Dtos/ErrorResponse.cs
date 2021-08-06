using System;

namespace Server.Api.Dtos
{
    public class ErrorResponse
{
    public string Type { get; set; }
    public string Message { get; set; }

    public ErrorResponse(Exception ex)
    {
        Type = ex.GetType().Name;
        Message = ex.Message;
    }
}
}