using ContactManagerTest.Models;

namespace ContactManagerTest.Services;

public class CSVDeserializerService : ICsvDeserializerService<Employee>
{
    public List<Employee> Deserialize(string csv)
    {
        var lines = csv.Trim().Split('\n').Select(v => v.Trim()).ToArray();
       // var propertiesLine = String.Join(',', typeof(Employee).GetProperties().Select(p => p.Name));
       // if (lines.FirstOrDefault() != propertiesLine) throw new ArgumentException("Invalid input data");
        List<Employee> entries = new List<Employee>();
        var valueNames = lines[0].Split(',').Select(v => v.Trim()).ToArray();

        for (int i = 1; i < lines.Length; i++)
        {
            if (String.IsNullOrWhiteSpace(lines[i])) continue;
            var values = lines[i].Split(',').Select(v => v.Trim()).ToArray();
            var entry = new Employee();
            entry.Married = bool.Parse(values[valueNames.TakeWhile(v => v != "Married").Count()]);
            entry.DateOfBirth = DateTime.Parse(values[valueNames.TakeWhile(v => v != "DateOfBirth").Count()]);
            entry.Name = values[valueNames.TakeWhile(v => v != "Name").Count()];
            entry.Phone = values[valueNames.TakeWhile(v => v != "Phone").Count()];
            entry.Salary = decimal.Parse(values[valueNames.TakeWhile(v => v != "Salary").Count()]);
            entries.Add(entry);
        }
        return entries;
    }
}
