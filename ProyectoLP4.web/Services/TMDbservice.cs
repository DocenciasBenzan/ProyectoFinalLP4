using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ProyectoLP4.web.Models;
using Newtonsoft.Json;

/// <summary>
/// Servicio para conectar con la API y obtener sus datos.
/// </summary>
public class TMDbService : ITMDbservice
{

	/// <summary>
	/// Se encarga del gestionamiento de las consultas generales a la API.
	/// </summary>
	private readonly HttpClient _httpClient;
	private const string ApiKey = "f080810985003cec1aeef1e10d5ce41b";
	private const string BaseUrl = "https://api.themoviedb.org/3/";

	public TMDbService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}
	/// <summary>
	/// Función para buscar peliculas usando la API.
	/// </summary>
	/// <param name="query"></param>
	/// Hace referencia al valor introducido por el usuario para la busqueda
	/// <returns></returns>
	public async Task<List<Movie>> SearchTitlesAsync(string query)
	{
		try
		{
			Console.WriteLine("Entrando a SearchTitlesAsync");
			if (string.IsNullOrWhiteSpace(query))
			{
				Console.WriteLine("El query está vacío o nulo.");
				return new List<Movie>();
			}

			var movieTask = _httpClient.GetAsync($"{BaseUrl}search/movie?api_key={ApiKey}&query={Uri.EscapeDataString(query)}");
			var tvTask = _httpClient.GetAsync($"{BaseUrl}search/tv?api_key={ApiKey}&query={Uri.EscapeDataString(query)}");

			await Task.WhenAll(movieTask, tvTask);

			Console.WriteLine("Llamadas a la API completadas.");

			var movieResults = await ParseResponseAsync(movieTask.Result);
			var tvResults = await ParseResponseAsync(tvTask.Result);

			Console.WriteLine($"Películas encontradas: {movieResults.Count}");
			Console.WriteLine($"Series encontradas: {tvResults.Count}");

			return movieResults.Concat(tvResults).ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Ocurrió un error: {ex.Message}");
			return new List<Movie>();
		}


		#region Old and new code (Not using)
		//var movieResults = movieTask.IsSuccessStatusCode
		//? JsonSerializer.Deserialize<TMDbSearchResult>(await movieTask.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?.Results
		//: new List<Movie>();

		//var tvResults = tvTask.IsSuccessStatusCode
		//? JsonSerializer.Deserialize<TMDbSearchResult>(await tvTask.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?.Results
		//: new List<Movie>();

		//return movieResults.Concat(tvResults).ToList();

		//OLD CODE

		//var response = await _httpClient.GetAsync($"{BaseUrl}search/movie?api_key={ApiKey}&query={Uri.EscapeDataString(query)}");
		//response.EnsureSuccessStatusCode();

		//var json = await response.Content.ReadAsStringAsync();
		//var result = JsonSerializer.Deserialize<TMDbSearchResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		//return result?.Results ?? new List<Movie>();
		#endregion
	}

	/// <summary>
	/// Función para utilizar los datos brinda la API a la hora de buscar.
	/// </summary>
	/// <param name="response"></param>
	/// <returns>Representa la variable utilizada para recibir la respuestas en tipo string.</returns>
	private async Task<List<Movie>> ParseResponseAsync(HttpResponseMessage response)
	{
		if (!response.IsSuccessStatusCode)
		{
			Console.WriteLine($"Error en la respuesta: {response.StatusCode}");
			return new List<Movie>();
		}

		var json = await response.Content.ReadAsStringAsync();
		
		var r = JsonConvert.DeserializeObject<TMDbSearchResult>(json);
		var movies = r.Results.Select(x => new Movie()
		{
			Title = x.title,
			Overview = x.overview,
			TMDbId = x.id,
			Poster_path = x.poster_path,
			Release_date = x.release_date,
			Vote_average = x?.vote_average?.ToString() ?? "0.00",
			Name = x?.Name ?? "",
			First_air_date = x?.First_air_date ?? ""

		}).ToList();
		return movies;
	}

	/// <summary>
	/// Función para obtener las peliculas y series en tendencia.
	/// </summary>
	/// <param name="mediaType"></param>
	/// <param name="timeWindow"></param>
	/// <returns>Una representa el tipo de titulo (pelicula o serie) y el otro el rango de tiempo</returns>
	public async Task<List<Movie>> GetTrendingAsync(string mediaType, string timeWindow = "week")
	{
		try
		{
			var response = await _httpClient.GetAsync($"{BaseUrl}trending/{mediaType}/{timeWindow}?api_key={ApiKey}");
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine($"Error al obtener tendencias: {response.StatusCode}");
				return new List<Movie>();
			}

			var json = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<TMDbSearchResult>(json);
			return result.Results.Select(x => new Movie
			{
				Title = x.title,
				Overview = x.overview,
				TMDbId = x.id,
				Poster_path = x.poster_path,
				Release_date = x.release_date,
				Name = x.Name,
				First_air_date = x.First_air_date,
				Vote_average = x?.vote_average?.ToString() ?? "0.00"
			}).ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error al obtener tendencias: {ex.Message}");
			return new List<Movie>();
		}
	}
}


public class TMDbSearchResult
{
	public List<ResultMovie> Results { get; set; }
	public int page { get; set; }
	public int total_pages { get; set; }
	public int total_results { get; set; }
}
public class ResultMovie
{
	public bool adult { get; set; }
	public string backdrop_path { get; set; }
	public List<int> genre_ids { get; set; }
	public int id { get; set; }
	public string original_language { get; set; }
	public string original_title { get; set; }
	public string overview { get; set; }
	public double popularity { get; set; }
	public string poster_path { get; set; }
	public string release_date { get; set; }
	public string title { get; set; }
	public bool video { get; set; }
	//Cambiar por string or decimal
	public object? vote_average { get; set; }
	public int vote_count { get; set; }
	public string? Name { get; set; }
	public string? First_air_date { get; set; }
}
