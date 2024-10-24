// build a .NET console application that fetches data from a the JSONPOST API and calculates the average 
// lenght of the posts' titles 

using CoinGeckoAverageMarketCap;

string apiUrl = "https://jsonplaceholder.typicode.com/posts";

try
{
    var posts = await PostService.FetchPosts(apiUrl);
    if (posts != null)
    {
        double totalTitleLength = PostService.CalculateAvgTitleLength(posts);
        Console.WriteLine($"Average length of all posts' titles is {totalTitleLength}");
    }
    else
    {
        Console.WriteLine("Failed to process or fetch the data");
    }
}
catch (Exception)
{

    Console.WriteLine("The problem occured while calling API.");
}


