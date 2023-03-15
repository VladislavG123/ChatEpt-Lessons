using ChatEpt.Services;
using ChatEpt.Services.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMessageService, MessageService>(); // 1 instance для всех запросов
// builder.Services.AddScoped<IMessageService, MessageService>(); // новый instance для каждого запроса
// builder.Services.AddTransient<IMessageService, MessageService>(); // новый instance при каждом обращении

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