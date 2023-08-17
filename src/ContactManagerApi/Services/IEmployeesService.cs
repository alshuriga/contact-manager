using ContactManagerTest.Models;

namespace ContactManagerTest.Services
{
    public interface IEmployeesService
    {
        Task<Employee[]> ListAll();
        Task<Employee?> GetById(int id);
        Task Add(Employee employee);
        Task AddMultiple(IEnumerable<Employee> employees);
        Task Update(Employee employee);
        Task DeleteById(int id);
    }
}
