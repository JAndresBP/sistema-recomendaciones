using Redis.OM.Modeling;

namespace servicio_recomendaciones.Domain;

[Document(StorageType = StorageType.Json, Prefixes = new []{"recomendations"})]
public class Recomendation
{
    [RedisIdField]
    [Indexed]
    public string? Key {get;set;}
    [Indexed]
    public int UserId {get;set;}
    [Indexed]
    public int RecomendationType {get;set;} 
    public IReadOnlyList<int>? ProductIds {get;set;}
    public long CreatedOn {get;set;}
}
