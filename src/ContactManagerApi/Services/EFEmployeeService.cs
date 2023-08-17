using ContactManagerTest.Models;
using ContactManagerTest.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ContactManagerTest.Services
{
    public class EFEmployeeService : IEmployeesService
    {
        private readonly EmployeesContext _context;
        private readonly IValidator<Employee> _validator;

        public EFEmployeeService(EmployeesContext context, IValidator<Employee> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task Add(Employee employee)
        {
            await _validator.ValidateAndThrowAsync(employee);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task AddMultiple(IEnumerable<Employee> employees)
        {
            foreach (var e in employees) await _validator.ValidateAndThrowAsync(e);
            await _context.AddRangeAsync(employees);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var entity = await _context.Employees.SingleOrDefaultAsync(e => e.EmployeeID == id);
            if (entity != null) _context.Remove(entity);
            await _context.SaveChangesAsync();  
        }

        public async Task<Employee?> GetById(int id)
        {
            var empl = await _context.Employees.AsNoTracking().SingleOrDefaultAsync(e => e.EmployeeID == id);
            return empl;
        }

        public async Task<Employee[]> ListAll()
        {
            return await _context.Employees.AsNoTracking().ToArrayAsync();
        }

        public async Task Update(Employee employee)
        {
            await _validator.ValidateAndThrowAsync(employee);
            _context.Employees.Update(employee);
            await  _context.SaveChangesAsync();
        }
    }
}
