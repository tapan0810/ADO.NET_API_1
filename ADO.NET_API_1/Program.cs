using ADO.NET_API_1.Data;

var builder = WebApplication.CreateBuilder(args);

// -------------------- CONFIGURATION --------------------

// Add services to the container
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// CORS (for frontend like Angular/React)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// -------------------- BUILD APP --------------------

var app = builder.Build();

// -------------------- MIDDLEWARE PIPELINE --------------------

// Enable Swagger only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global Exception Handling (basic)
app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// -------------------- RUN APP --------------------

app.Run();