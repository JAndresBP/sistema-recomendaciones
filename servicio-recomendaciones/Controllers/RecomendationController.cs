using Microsoft.AspNetCore.Mvc;
using Redis.OM;
using Redis.OM.Searching;
using servicio_recomendaciones.Domain;

namespace servicio_recomendaciones.Controllers;

[ApiController]
[Route("[controller]")]
public class RecomendationController : ControllerBase
{
    private readonly RedisCollection<Recomendation> _recomendation;
    private readonly RedisConnectionProvider _provider;
    private readonly ILogger<RecomendationController> _logger;

    public RecomendationController(ILogger<RecomendationController> logger, RedisConnectionProvider provider)
    {
        _logger = logger;
        _provider = provider;
        _recomendation = (RedisCollection<Recomendation>)provider.RedisCollection<Recomendation>();
    }

    [HttpGet]
    public Task<Recomendation> Get(int userId, int recomendationType)
    {
        return _recomendation.FirstAsync(item => item.UserId == userId && item.RecomendationType == recomendationType);
    }
}
