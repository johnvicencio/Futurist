using System;
using Futurist.Shared.Models;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace Futurist.Shared.Services;

public class PredictionService
{
    private readonly HttpClient _httpClient;

    public PredictionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetAnswerAsync(string question)
    {
        var predictions = await _httpClient.GetFromJsonAsync<List<Prediction>>("data/predictions.json");
        var random = new Random();
        var index = random.Next(predictions.Count);
        var firstWord = question.Split(' ')[0].ToLower();
        if (question.ToLower().Contains("fake"))
        {
            return "That is harsh and this reflects on you!";
        }
        else if (question.ToLower().Contains("john"))
        {
            return "Are you talking smack?";
        }
        else if (new[] { "who", "what", "when", "where", "why", "how", "which", "whom", "whose", "will", "am", "should", "ought", "can", "could"}.Contains(firstWord) && (question.ToLower().Contains("i") || question.ToLower().Contains("me")))
        {
          
            return predictions[index].Answer;
        }
        else
        {
            return $"You said {firstWord.ToUpper()} and did not ask about yourself? Ask me a question about yourself...";
        }
    }

}

