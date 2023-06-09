using ChatEpt;
using ChatEpt.Services;
using ChatEpt.Services.Abstract;
using ChatEpt.Services.Ai;
using ChatEpt.Services.Ai.Abstract;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAiMessageRouter, AiMessageRouter>();
builder.Services.AddSingleton<ProfanityFilter.ProfanityFilter>(); 
builder.Services.AddSingleton<IBadWordChecker, BadWordChecker>();

builder.Services.AddScoped<StartupAiService>();
builder.Services.AddScoped<IWhoIsAiService, WhoIsAiService>();
builder.Services.AddScoped<RandomAiService>();

// Add HttpClients
builder.Services.AddHttpClient<AiMessageRouter>();

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