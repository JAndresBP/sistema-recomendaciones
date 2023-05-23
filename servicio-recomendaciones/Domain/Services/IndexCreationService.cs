

using Redis.OM;

namespace servicio_recomendaciones.Domain.Services;

public class IndexCreationService : IHostedService
{
    private readonly RedisConnectionProvider _provider;
    public IndexCreationService(RedisConnectionProvider provider)
    {
        _provider = provider;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _provider.Connection.CreateIndexAsync(typeof(Recomendation));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}