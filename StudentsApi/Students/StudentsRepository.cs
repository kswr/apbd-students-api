using System.Text;
using StudentsApi.Students.Model;

namespace StudentsApi.Students;

public static class StudentsRepository
{
    public static HashSet<StudentDetails> GetAll()
    {
        return StudentCsvAdapter.Read();
    }

    public static StudentDetails? Get(string indexNumber)
    {
        try
        {
            return StudentCsvAdapter.Read().First(x => x.IndexNumber.Equals(indexNumber));
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public static StudentDetails? Update(StudentDetails student)
    {
        try
        {
            AssertDetails(student);
            var existingStudent = StudentCsvAdapter.Read()
                .First(x => x.IndexNumber.Equals(student.IndexNumber));
            if (existingStudent is null) return null;
            var students = StudentCsvAdapter.Read();
            students.Remove(existingStudent);
            students.Add(student);
            StudentCsvAdapter.Write(students);
            return student;
        }
        catch (DetailsAssertionException)
        {
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public static StudentDetails? Add(StudentDetails student)
    {
        try
        {
            AssertDetails(student);
            var students = StudentCsvAdapter.Read();
            if (!students.Add(student)) return null;
            StudentCsvAdapter.Write(students);
            return student;
        }
        catch (DetailsAssertionException)
        {
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static void AssertDetails(StudentDetails student)
    {
        var details = new List<string>
        {
            student.IndexNumber, student.FName, student.LName, student.Email, student.MothersName, student.FathersName,
            student.Studies, student.Mode
        };
        if (details.Any(string.IsNullOrEmpty))
        {
            throw new DetailsAssertionException();
        }

        if (!student.IndexNumber.StartsWith("s")) throw new DetailsAssertionException();
    }
}

internal class DetailsAssertionException : Exception
{
}

public static class StudentCsvAdapter
{
    private static readonly string CsvDirectory = $"{Directory.GetCurrentDirectory()}/students.csv";

    public static HashSet<StudentDetails> Read()
    {
        var studentDetails = new HashSet<StudentDetails>();
        var lines = File.ReadAllLines(CsvDirectory);
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

    public static void Write(IEnumerable<StudentDetails> students)
    {
        var lines = students.Select(ToCsv).ToList();
        File.WriteAllLines(CsvDirectory, lines);
    }

    private static string ToCsv(StudentDetails student)
    {
        return new StringBuilder().AppendJoin(",",
            student.FName,
            student.LName,
            student.IndexNumber,
            student.Birthdate.ToString(),
            student.Studies,
            student.Mode,
            student.Email,
            student.FathersName,
            student.MothersName).ToString();
    }
}