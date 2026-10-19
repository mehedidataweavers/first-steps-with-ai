using System.ClientModel;
using Microsoft.Extensions.AI;
using OpenAI;

// Groq's free hosted API (OpenAI-compatible). Get a key at https://console.groq.com/keys
string key = Environment.GetEnvironmentVariable("GROQ_API_KEY")
    ?? throw new InvalidOperationException("Set the GROQ_API_KEY environment variable.");

IChatClient client = new OpenAIClient(
        new ApiKeyCredential(key),
        new OpenAIClientOptions { Endpoint = new Uri("https://api.groq.com/openai/v1") })
    .GetChatClient("openai/gpt-oss-20b") // Apache 2.0
    .AsIChatClient();

string text = File.ReadAllText("benefits.md");
string prompt = $"""
    Summarize the following text in 20 words or less:
    {text}
    """;

ChatResponse response = await client.GetResponseAsync(prompt);
Console.WriteLine(response);
