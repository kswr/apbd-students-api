using StudentsApi.Students.Model;

namespace StudentsApi.Students;

public static class StudentsRepository
{
    private static readonly string CsvDirectory = $"{Directory.GetCurrentDirectory()}/students.csv";

    public static HashSet<StudentDetails> GetAll()
    {
        return StudentCsvAdapter.Read(CsvDirectory);
    }
}

public static class StudentCsvAdapter
{
    public static HashSet<StudentDetails> Read(string sourceFile)
    {
        var studentDetails = new HashSet<StudentDetails>();
        var lines = File.ReadAllLines(sourceFile);
        foreach (var line in lines)
        {
            try
            {
                var details = FromCsv(line);
                studentDetails.Add(details);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        return studentDetails;
    }

    private static StudentDetails FromCsv(string record)
    {
        var fields = record.Split(",");
        return new StudentDetails
        {
            FName = fields[0],
            LName = fields[1],
            IndexNumber = fields[2],
            Birthdate = DateOnly.Parse(fields[3]),
            Studies = fields[4],
            Mode = fields[5],
            Email = fields[6],
            FathersName = fields[7],
            MothersName = fields[8],
        };
    }
}