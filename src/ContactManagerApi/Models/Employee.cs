namespace ContactManagerTest.Models;

public class Employee
{
    public int EmployeeID { get; set; }
    public string Name { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public bool Married { get; set; }
    public string Phone { get; set; } = null!;
    public decimal Salary { get; set; }
}
