using RmqLib;
using RmqLib.@interface;
using task_master_api.Exchanges;
using task_master_api.Queues;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IQueueConnection>(_ => new QueueConnectionService("amqp://guest:guest@localhost:5672"));
builder.Services.AddScoped<IQueuePublisher<TaskQueue, DefaultExchange>>(s => (QueuePublisherService<TaskQueue, DefaultExchange>)(new(s.GetRequiredService<IQueueConnection>(), new TaskQueue(), new DefaultExchange())));
builder.Services.AddScoped<IQueuePublisher<ReportsQueue, DefaultExchange>>(s => (QueuePublisherService<ReportsQueue, DefaultExchange>)(new(s.GetRequiredService<IQueueConnection>(), new ReportsQueue(), new DefaultExchange())));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
