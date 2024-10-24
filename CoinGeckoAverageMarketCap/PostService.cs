using System;
using System.Text.Json;

namespace CoinGeckoAverageMarketCap;

public static class PostService
{
    public static async Task<List<Post>> FetchPosts(string uri)
    {

        using HttpClient client = new HttpClient();

        try
        {
            // Call Gecko API
            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            
            // deserialize the response
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = JsonSerializer.Deserialize<List<Post>>(responseBody, options);
            return result;

        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);

            Console.WriteLine("The problem occured while calling API");
            return null;
        }

    }
    public static double CalculateAvgTitleLength(List<Post> posts)
    {
        if (posts == null || posts.Count == 0)
        {
            return 0.00;
        }
        double totalTitleLength = posts.Average(p => p.Title.Length);
        return totalTitleLength;

    }
}

public class Post
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; }
    public string Body { get; set; }
}
