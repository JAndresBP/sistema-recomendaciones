using System.Diagnostics;
using Redis.OM;
using Redis.OM.Searching;

namespace servicio_recomendaciones.Domain.Services;

public class Worker : BackgroundService
{
    private readonly RedisCollection<Recomendation> _recomendation;
    private readonly RedisConnectionProvider _provider;
    private readonly ILogger<Worker> _logger;

    private readonly RecomendationService _recomendationService;

    public Worker(ILogger<Worker> logger, RedisConnectionProvider provider, RecomendationService recomendationService)
    {
        _logger = logger;
        _provider = provider;
        _recomendation = (RedisCollection<Recomendation>)provider.RedisCollection<Recomendation>();
        _recomendationService = recomendationService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(5000, stoppingToken);
        var sw = new Stopwatch();
        sw.Start();
        long t1 = 0, t2 = 0, t3 = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            var elapsed = sw.Elapsed;
            if( elapsed < TimeSpan.FromMinutes(1) || elapsed > TimeSpan.FromMinutes(5)){
            try{
            t1 = sw.ElapsedMilliseconds;
            var recomendations = _recomendationService.GetRecomendations();
            t2 = sw.ElapsedMilliseconds;
            foreach(var recomendation in recomendations){
                _recomendation.Update(recomendation);
            }
            t3 = sw.ElapsedMilliseconds;
            }catch(Exception e){
                _logger.LogInformation(e.Message);
            }

            _logger.LogInformation($"Worker running at: {DateTimeOffset.Now}, t2 - t1 {t2-t1}, t3 - t2 {t3-t2}");
            }
            else{
                _logger.LogInformation("Paused");
            }
            await Task.Delay(10000, stoppingToken);
        }
        sw.Stop();
    }
}
