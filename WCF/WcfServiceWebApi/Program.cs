var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Enable Swagger UI
app.UseSwagger();

// Serve static files for the JSON definitions
app.UseStaticFiles();

// Configure Swagger UI with custom options
app.UseSwaggerUI(c =>
{
    // Add multiple services with dropdowns for JSON files
    //c.SwaggerEndpoint("serviceA.json", "Service A API");
    //c.SwaggerEndpoint("serviceB.json", "Service B API");

    // Customize the UI (optional)
    c.EnableDeepLinking();
    c.DisplayOperationId();
    c.RoutePrefix = "swagger";
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
