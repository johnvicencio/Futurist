using Futurist.Shared.Models;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System;

namespace Futurist.Shared.Services;

public class PredictionService
{
    private readonly HttpClient _httpClient;
    private string answer = String.Empty;
    private int index = 0;

    public PredictionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetAnswerAsync(string question)
    {
        List<Prediction>? items = await _httpClient.GetFromJsonAsync<List<Prediction>>("data/predictions.json");
        if (items != null)
        {
            List<Prediction> predictions = items.GroupBy(p => p.Answer).Select(g => g.First()).ToList();
            var random = new Random();
            if (predictions != null)
            {
                index = random.Next(predictions.Count);
            }

            if (isQuestion(question))
            {
                if (predictions != null)
                {
                    answer = predictions[index].Answer;
                }
            }
            else
            {
                if (question.ToLower().Contains("fake"))
                {
                    answer = "That is harsh and this reflects on you!";
                }
                else if (question.ToLower().Contains("john"))
                {
                    answer = "Are you talking smack?";
                }
                else if (question.ToLower().Contains("who are you"))
                {
                    answer = "I'm who I am.";
                }
                else if (question.ToLower().Contains("tell me about yourself"))
                {
                    answer = "I am the Futurist!";
                }
                else if (!answer.Trim().EndsWith("?"))
                {
                    answer = "You did type something. Did you ask me a question?";
                }
                else
                {
                    List<string> huhs = new List<string> {
                        "Huh?",
                        "Ask about your future.",
                        "What do you want about the future?",
                        "I don't understand...",
                        "Is this a real question?",
                        "Ask me another question.",
                        "Let's talk about something else."
                    };

                    index = random.Next(huhs.Count);
                    var huh = huhs[index];
                    answer = huh;
                }
            }

        }
        return answer;
    }

    private bool isQuestion(string input)
    {
        bool result;
        string firstPersonPattern = @"\b(I|me|my|mine)\b";
        bool isFirstPerson = Regex.IsMatch(input, firstPersonPattern, RegexOptions.IgnoreCase);
        string questionPattern = @"^\b(who|what|when|where|why|how|which|whom|whose|will|is|can|could|should|ought)\b";
        bool isQuestion = Regex.IsMatch(input, questionPattern, RegexOptions.IgnoreCase);

        result = isFirstPerson && isQuestion ? true: false ;

        return result;
    }

}

