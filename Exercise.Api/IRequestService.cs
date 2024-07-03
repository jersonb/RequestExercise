namespace Exercise.Api;

public interface IRequestService
{
    Task<int> DeleteAsync(RequestDelete request, CancellationToken stoppingToken);

    Task<IEnumerable<Response>> GetAsync(CancellationToken stoppingToken);

    Task<Response> GetAsync(Guid uuid, CancellationToken stoppingToken);

    Task<(int StatusCode, Uri? Location)> PostAsync(RequestPost request, CancellationToken stoppingToken);

    Task<(int StatusCode, Uri? Location)> PutAsync(RequestPut request, CancellationToken stoppingToken);
}