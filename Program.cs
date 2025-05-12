using apigateway.service.Producer;
using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton(new ProducerConfig{
    BootstrapServers = Environment.GetEnvironmentVariable("Kafka_BootstrapServers"),
    ClientId = Environment.GetEnvironmentVariable("Kafka_Client_Id")
});

builder.Services.AddScoped<KafkaProducer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
