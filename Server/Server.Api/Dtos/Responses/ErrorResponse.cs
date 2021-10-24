using System;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO used to unify error responses from the API
    /// </summary>
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