using JobFinder.Server.Helpers;
using JobFinder.Server.Parsers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IParserFactory, ParserFactory>();

builder.Services.AddScoped<WebHelper>();

builder.Services.AddScoped<IParser, FakeParser>();

builder.Services.AddScoped<IParser, JobinjaParser>();
builder.Services.AddScoped<IParser, QueraParser>();
builder.Services.AddScoped<IParser, JobvisionParser>();

builder.Services.AddHttpClient("Default", opt =>
{
    opt.Timeout = TimeSpan.FromSeconds(5);
    opt.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64; rv:128.0) Gecko/20100101 Firefox/128.0");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(conf =>
{
    conf.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
