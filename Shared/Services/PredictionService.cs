using System;
using Futurist.Shared.Models;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

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
        bool triggers = Regex.IsMatch(question.Trim(), @"^\b(who|what|when|where|why|how|which|whom|whose|will)\b", RegexOptions.IgnoreCase) && (question.Trim().EndsWith("?")) ? true : false;
        if (question.ToLower().Contains("fake"))
        {
            return "That is harsh and this reflects on you!";
        }
        else if (question.ToLower().Contains("john"))
        {
            return "Are you talking smack?";
        }
        else if (triggers)
        {
          
            return predictions[index].Answer;
        }
        else
        {
            List<string> answers = new List<string> {
                "Huh?",
                "I don't understand...",
                "Is this a real question?",
                "Ask me another question.",
                "Let's talk about something else."
            };
            index = random.Next(answers.Count);
            string answer = answers[index];
            if (answer.Trim().EndsWith("?"))
            {
                return answer;
            }
            else
            {
                return "Are you missing a question mark?";
            }
            
        }
    }

}

