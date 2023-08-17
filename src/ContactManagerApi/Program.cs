using ContactManagerTest;
using ContactManagerTest.Models;
using ContactManagerTest.Infrastructure;
using ContactManagerTest.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<EmployeesContext>(opts => {
    if (builder.Configuration.GetValue<bool>("UseInMemoryDb"))
        opts.UseInMemoryDatabase("employeesDb");
    else
        opts.UseSqlServer(builder.Configuration.GetConnectionString("EmployeesDb"));
    });
builder.Services.AddSingleton<ICsvDeserializerService<Employee>, CSVDeserializerService>();
builder.Services.AddScoped<IEmployeesService, EFEmployeeService>();
builder.Services.AddScoped<IValidator<Employee>, EmployeeValidator>();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations());
var app = builder.Build();

//exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();
app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseCors(c =>
    {
        c.AllowAnyOrigin();
        c.AllowAnyHeader();
        c.AllowAnyMethod();
    });
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
    app.UseHttpsRedirection(); 
}




app.Run();
