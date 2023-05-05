using System;
using Futurist.Shared.Models;
using System.Net.Http.Json;

namespace Futurist.Shared.Services;

public class PredictionService
{
    private readonly HttpClient _httpClient;

    public PredictionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetRandomAnswerAsync()
    {
        var predictions = await _httpClient.GetFromJsonAsync<List<Prediction>>("data/predictions.json");
        var random = new Random();
        var index = random.Next(predictions.Count);
        return predictions[index].Answer;
    }
}

