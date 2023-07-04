using HR_Management.Application;
using HR_Management.Infrastructure;
using HR_Management.Persistence;
using HR_Management.Persistence.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Config Database

builder.Services.AddDbContext<CompanyManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OfficeAutomationSystemConnectionString")));

#endregion
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>

{
    options.AddPolicy("CorsPolicy", b =>
        b.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
    );
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
