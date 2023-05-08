using Futurist.Shared.Models;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Futurist.Shared.Services;

public class PredictionService
{
    private readonly HttpClient _httpClient;
    private string result = "";
    private int index = 0;
    //private string key = Environment.GetEnvironmentVariable("CHATGPT_API"); //working on it
    //const string url = "https://api.openai.com/v1/chat/completions";

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


            bool triggers = Regex.IsMatch(question.Trim(), @"^\b(who|what|when|where|why|how|which|whom|whose|will|is|can|could|should|ought)\b", RegexOptions.IgnoreCase) && (question.Trim().EndsWith("?")) ? true : false;
            if (question.ToLower().Contains("fake"))
            {
                result = "That is harsh and this reflects on you!";
            }
            else if (question.ToLower().Contains("john"))
            {
                result = "Are you talking smack?";
            }
            else if (question.ToLower().Contains("who are you"))
            {
                result = "I'm who I am.";
            }
            else if (question.ToLower().Contains("tell me about yourself"))
            {
                result = "I am the Futurist!";
            }
            else if (triggers)
            {
                if (predictions != null)
                {
                    result = predictions[index].Answer;
                }

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
                    result = answer;
                }
                else
                {
                    result = "Are you missing a question mark?";
                }

            }


           
        }
        return result;
    }

    //public async Task<string> GetChatGptResponseAsync(string prompt)
    //{
    //    // Initialise the chat by describing the assistant,
    //    // and providing the assistants first question to the user
    //    var messages = new List<dynamic>
    //    {
    //        new {role = "system",
    //            content = "You are a universal entity, an astrologer, or someone who has prophesies who read the future of a person. I'm a person who will type a question. After I typed my question please generate a generic answer."},
    //        new {role = "user",
    //            content = "How can I help you?"}
    //    };
    //    var userMessage = prompt;
    //    messages.Add(new { role = "user", content = userMessage });

    //    // Create the request for the API sending the
    //    // latest collection of chat messages
    //    var request = new
    //    {
    //        messages,
    //        model = "gpt-3.5-turbo",
    //        max_tokens = 300,
    //    };


    //    // Send the request and capture the response
    //    var httpClient = new HttpClient();
    //    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {key}");
    //    var requestJson = JsonConvert.SerializeObject(request);
    //    var requestContent = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json");
    //    var httpResponseMessage = await httpClient.PostAsync(url, requestContent);
    //    var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
    //    var responseObject = JsonConvert.DeserializeAnonymousType(jsonString, new
    //    {
    //        choices = new[] { new { message = new { role = string.Empty, content = string.Empty } } },
    //        error = new { message = string.Empty }
    //    });


    //    if (!string.IsNullOrEmpty(responseObject?.error?.message))  // Check for errors
    //    {
    //        return "Error";
    //    }
    //    else  // Add the message object to the message collection
    //    {
    //        var messageObject = responseObject?.choices[0].message;
    //        messages.Add(messageObject);
    //        return messageObject.content;
    //    }
    //}

}

