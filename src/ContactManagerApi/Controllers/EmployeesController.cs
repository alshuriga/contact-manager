using ContactManagerTest.Models;
using ContactManagerTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;

namespace ContactManagerTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly ICsvDeserializerService<Employee> _deserializerService;
        private readonly IEmployeesService _employeesService;

        public EmployeesController(ICsvDeserializerService<Employee> deserializerService, IEmployeesService employeesService)
        {
            _deserializerService = deserializerService;
            _employeesService = employeesService;
        }

        [SwaggerOperation(Summary = "Add employees from a CSV file to the database")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<IActionResult> UploadEmployeesData([FromForm] IFormFile file)
        {
            if (file.ContentType != "text/csv") throw new ApplicationException("Invalid type of file");
            var result = new StringBuilder();
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                while (stream.Peek() > 0)
                {
                    result.AppendLine(stream.ReadLine());
                }
            }
            var entries =  _deserializerService.Deserialize(result.ToString());
            await _employeesService.AddMultiple(entries);

            return NoContent();
        }

        [SwaggerOperation(Summary = "List all employees from the database.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> ListAllEmployees()
        {
            var employees = await _employeesService.ListAll();
            return Ok(employees);
        }

        [SwaggerOperation(Summary = "Update single employee record.")]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
           await _employeesService.Update(employee);
           return NoContent();
        }

        [SwaggerOperation(Summary = "Delete single by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeesService.DeleteById(id);
            return NoContent();
        }


    }
}
