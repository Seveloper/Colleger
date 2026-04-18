using System.Net;
using System.Text.Json.Serialization;

namespace Domain.Common;

public record ApiResponse<T>
{
    public int ResponseCode { get; set; }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public List<Message> Messages { get; set; } = [];
    public T? Data { get; set; }

    [JsonIgnore]
    public bool HasErrors => Messages.Exists(m => m.ErrorLevel >= ErrorLevels.Error);

    public static ApiResponse<T> Ok(T data) => new() { Data = data };

    public static ApiResponse<T> Fail(HttpStatusCode status, string description, int code = 0) => new()
    {
        StatusCode = status,
        Messages   = [Message.Error(description, code)],
    };
}
