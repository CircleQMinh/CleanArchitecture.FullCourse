using CircleCat.CleanArchitecture.FullCourse.API.Configurations;
using CircleCat.CleanArchitecture.FullCourse.API.Middlewares;
using CircleCat.CleanArchitecture.FullCourse.Application;
using CircleCat.CleanArchitecture.FullCourse.Domain;
using CircleCat.CleanArchitecture.FullCourse.Infrastructure;
using CircleCat.CleanArchitecture.FullCourse.Infrastructure.Identity;
using CircleCat.CleanArchitecture.FullCourse.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDomainLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddSwaggerGenConfiguration(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionMiddleware>();
//run migration on start
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AppCorsPolicy");
app.MapControllers();

app.Run();
