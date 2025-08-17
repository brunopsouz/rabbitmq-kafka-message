using Repositories.App;
using Repositories.App.Interfaces;
using Service.App;
using Service.App.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IMessageService, MessageService>();
//builder.Services.AddTransient<IMessageRepository, RabbitMessageRepository>(); //RabbitMQ Repository
builder.Services.AddTransient<IMessageRepository, KafkaMessageRepository>(); // Kafka Repository

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
