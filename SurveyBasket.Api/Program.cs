
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ApplyDependencyInjection(builder.Configuration);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
    //app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/openapi/v1.json", "v1"));
    //app.MapScalarApiReference();
}

app.UseSerilogRequestLogging();


app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
