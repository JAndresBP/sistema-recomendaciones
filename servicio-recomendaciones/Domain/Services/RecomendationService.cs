using servicio_recomendaciones.Domain;

public class RecomendationService 
{
    private readonly HttpClient _httpClient;
    

    public RecomendationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    internal IReadOnlyList<Recomendation> GetRecomendations()
    {
        List<Recomendation> recomendations = new List<Recomendation>();
        var rnd = new Random();
        for(int i = 0; i< 1000; i++){
            for(int j = 0; j < 4; j++){
                recomendations.Add(new Recomendation{
                    Key = $"{i}-{j}",
                    UserId = i,
                    RecomendationType = j,
                    CreatedOn = DateTime.UtcNow.Ticks,
                    ProductIds = GetProductList()
                });
            }
        }
        return recomendations;
    }

    private IReadOnlyList<int> GetProductList(){
        var rnd = new Random();
        var size = 10;
        var list = new List<int>();
        for(int i = 0; i < size; i++){
            list.Add(rnd.Next(500));
        }
        return list;
    }
}