using SchoolLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Tilf�jer controller services til dependency injection containeren.
builder.Services.AddControllers();

// Tilf�jer support til API endpoints og Swagger dokumentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Konfigurerer CORS (Cross-Origin Resource Sharing) politik.
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

// Tilf�jer en singleton service for TeacherRepository.
builder.Services.AddSingleton<TeacherRepository>(new TeacherRepository());

var app = builder.Build();

// Aktiverer Swagger middleware til at generere og vise API dokumentation.
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
// Konfigurerer middleware til at h�ndtere autorisation.
app.UseAuthorization();

// Aktiverer CORS politikken defineret tidligere.
app.UseCors("AllowAll");

// Mapper controller routes til endpoints.
app.MapControllers();

// K�rer applikationen.
app.Run();
