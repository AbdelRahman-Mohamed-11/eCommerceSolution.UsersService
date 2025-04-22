using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Infrastructure;
using eCommerce.Infrastructure.DbContext;
using eCommerce.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration)
    .AddCore(builder.Configuration)
    .AddUseCases(builder.Configuration);


builder.Services.AddControllers();


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddScoped<DapperDbContext>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
});


var app = builder.Build();

app.UseExceptionHandler();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();
