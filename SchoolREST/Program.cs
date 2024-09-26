using SchoolLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                              });
});

builder.Services.AddSingleton<TeacherRepository>(new TeacherRepository());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
