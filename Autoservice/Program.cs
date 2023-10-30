using Autoservice.Infrastructure.DependencyInjection;
using Autoservice.Presentation.Models;

var builder = WebApplication.CreateBuilder();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddUserSecrets<InfrastructureAssembly>();
builder.Services.AddDbConnection();

builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssemblyContaining<ApplicationAssembly>();
});

builder.AddCommandsAndQueries().AddRepositories().AddValidators();
var app = builder.Build();

app.AddCarModel().AddProviderModel().AddClientModel().AddEmployeeModel().AddPartModel();
app.UseSwagger().UseSwaggerUI();

app.Run(); 