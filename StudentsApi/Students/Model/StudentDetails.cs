using System.Text.Json.Serialization;

namespace StudentsApi.Students.Model;

public class StudentDetails
{
    public string IndexNumber { get; init; }
    public string FName { get; init; }
    public string LName { get; init; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly Birthdate { get; init; }
    public string Email { get; init; }
    public string MothersName { get; init; }
    public string FathersName { get; init; }
    public string Studies { get; init; }
    public string Mode { get; init; }
}