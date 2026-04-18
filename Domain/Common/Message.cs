using System.Text.Json.Serialization;

namespace Domain.Common;

public record Message
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ErrorLevels ErrorLevel { get; set; }
    public int Code { get; set; }
    public string? Description { get; set; }

    public static Message Info(string description, int code = 0)    => Build(ErrorLevels.Info,    description, code);
    public static Message Warning(string description, int code = 0) => Build(ErrorLevels.Warning, description, code);
    public static Message Error(string description, int code = 0)   => Build(ErrorLevels.Error,   description, code);
    public static Message Fatal(string description, int code = 0)   => Build(ErrorLevels.Fatal,   description, code);

    private static Message Build(ErrorLevels level, string description, int code) => new()
    {
        ErrorLevel  = level,
        Code        = code,
        Description = description,
    };
}
