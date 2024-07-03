using System.Text.Json;

namespace Exercise.Api;

public class Worker(IRequestService service, ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        try
        {
            var requestPost = new RequestPost
            {
                Uuid = Guid.NewGuid()
            };

            var (StatusCodePost, LocationPost) = await service.PostAsync(requestPost, stoppingToken);
            logger.LogInformation("Post HttpStatus: {@Status}, Location: {@Location}", StatusCodePost, LocationPost);

            var response = await service.GetAsync(requestPost.Uuid, stoppingToken);
            logger.LogInformation("Get Response: {@Response}", JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));

            var requestPut = new RequestPut
            {
                Uuid = response.Uuid,
                Id = response.Id + 1,
            };

            var (StatusCodePut, LocationPut) = await service.PutAsync(requestPut, stoppingToken);
            logger.LogInformation("Put HttpStatus: {@Status}, Location: {@Location}", StatusCodePut, LocationPut);

            var requestDelete = new RequestDelete
            {
                Uuid = response.Uuid
            };

            var list = await service.GetAsync(stoppingToken);

            logger.LogInformation("Get List Response: {@Response}", JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true }));

            var statusDelete = await service.DeleteAsync(requestDelete, stoppingToken);
            logger.LogInformation("Delete HttpStatus: {@Status}", statusDelete);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error:");
        }
    }
}