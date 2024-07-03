using Exercise.Api;
using Exercise.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddHttpClient<IRequestService, HttpClientService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5093");
});

services.AddSingleton<FakeCache>();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddHostedService<Worker>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.AddExerciseEnpoints()
   .UseHttpsRedirection();

await app.RunAsync();