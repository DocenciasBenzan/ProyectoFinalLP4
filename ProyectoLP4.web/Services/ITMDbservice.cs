using ProyectoLP4.web.Models;

public interface ITMDbservice
{
    Task<List<Movie>> SearchTitlesAsync(string query);
    Task<List<Movie>> GetTrendingAsync(string mediaType, string timeWindow = "week");
}