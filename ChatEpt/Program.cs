using ChatEpt;
using ChatEpt.Services;
using ChatEpt.Services.Abstract;
using ChatEpt.Services.Ai;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<StartupAiService>();
builder.Services.AddScoped<WhoIsAiService>();
builder.Services.AddScoped<RandomMessageAiService>();

builder.Services.AddSingleton<ProfanityFilter.ProfanityFilter>(); 
builder.Services.AddSingleton<IBadWordChecker, BadWordChecker>(); 
builder.Services.AddSingleton<IAiMessageRouter, AiMessageRouter>(); 

// Add HttpClients
builder.Services.AddHttpClient<StartupAiService>();
builder.Services.AddHttpClient<WhoIsAiService>();
builder.Services.AddHttpClient<RandomMessageAiService>();

builder.Services.AddDbContext<ApplicationContext>(x => 
    x.UseInMemoryDatabase(builder.Configuration.GetConnectionString("InMemory")!));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();