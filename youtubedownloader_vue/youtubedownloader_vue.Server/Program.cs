using CleanArchitecture.Infrastructure;
using Duende.IdentityServer.Hosting;
using youtubedownloader_vue.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration); 
builder.Services.AddWebUiServices();
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseOpenApi();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(settings =>
    {
       
    });
}

app.UseCors(policy => 
    policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("/index.html");

app.Run();
