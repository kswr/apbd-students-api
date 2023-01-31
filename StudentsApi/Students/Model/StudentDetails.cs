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

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        var other = (StudentDetails) obj;
        return IndexNumber.Equals(other.IndexNumber);
    }

    public override int GetHashCode()
    {
        return IndexNumber.GetHashCode() * 17;
    }
}