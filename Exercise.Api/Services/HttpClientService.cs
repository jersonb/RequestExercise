namespace Exercise.Api.Services;

public class HttpClientService(HttpClient client, ILogger<HttpClientService> logger) : IRequestService
{
    public async Task<IEnumerable<Response>> GetAsync(CancellationToken stoppingToken)
    {
        var response = await client.GetFromJsonAsync<List<Response>>($"exercise", stoppingToken);

        logger.LogInformation("Get Response : {@Response}", response.Count);

        return response;
    }

    public async Task<Response> GetAsync(Guid uuid, CancellationToken stoppingToken)
    {
        var response = await client.GetFromJsonAsync<Response>($"exercise/{uuid}", stoppingToken)
            ?? throw new Exception();

        logger.LogInformation("Get Response : {@Response}", response.Id);

        return response;
    }

    public async Task<(int StatusCode, Uri? Location)> PostAsync(RequestPost request, CancellationToken stoppingToken)
    {
        var post = await client.PostAsJsonAsync("exercise", request, stoppingToken);

        logger.LogInformation("Post {@Status} Guid to request: {@Guid}", post.StatusCode, request.Uuid);
        return ((int)post.StatusCode, post.Headers.Location);
    }

    public async Task<(int StatusCode, Uri? Location)> PutAsync(RequestPut request, CancellationToken stoppingToken)
    {
        var put = await client.PutAsJsonAsync($"exercise/{request.Uuid}", request, stoppingToken);

        logger.LogInformation("Put {@Status} Guid to request: {@Guid}", put.StatusCode, request.Uuid);
        return ((int)put.StatusCode, put.Headers.Location);
    }

    public async Task<int> DeleteAsync(RequestDelete request, CancellationToken stoppingToken)
    {
        var put = await client.DeleteAsync($"exercise/{request.Uuid}", stoppingToken);

        logger.LogInformation("Delete {@Status} Guid to request: {@Guid}", put.StatusCode, request.Uuid);
        return (int)put.StatusCode;
    }
}