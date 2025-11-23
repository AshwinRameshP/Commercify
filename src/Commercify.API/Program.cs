using Commercify.Api.Modules;
using Commercify.API.Extensions;
using Commercify.API.Modules;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddEndpointsApiExplorer()
    .AddOpenApi()
    .AddCustomConfiguration(builder.Configuration)
    .AddDatabase()
    .AddSwagger()
    .AddDependencyInjection()
    .AddCors(policy =>
    {
        policy.AddPolicy("OpenCorsPolicy", builder =>
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    });

var app = builder.Build();

//using var scope = app.Services.CreateScope();
//var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//dbContext.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //scalar added
    app.MapScalarApiReference(options =>
    {
        // Added for Host server to be visible in the UI
        options.Servers = Array.Empty<ScalarServer>();
    });
    //swagger ui support
    //app.UseSwagger();
    //app.UseSwaggerUI(options =>
    //{
    //    options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI v1");
    //});
    app.UseSwaggerWithUI(app.Environment);
    //Redoc suppport
    app.UseReDoc(options =>
    {
        options.SpecUrl = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();

CategoryModule.MapEndpoints(app);
ProductModule.MapEndpoints(app);

app.Run();