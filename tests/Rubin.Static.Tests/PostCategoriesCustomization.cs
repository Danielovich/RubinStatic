using Rubin.Static.Models;

namespace Rubin.Static.Tests;

internal static class PostCategoriesCustomization
{
    static Random rnd = new Random();
    public static IEnumerable<Category> RandomPickedCategories(string commaDelimitedCategories)
    {
        // Shuffle the list
        List<Category> shuffled = new List<Category>();
        commaDelimitedCategories.Split(',')
            .ToList()
            .ForEach(f => shuffled.Add(new Category() { Title = f }));

        shuffled.OrderBy(x => rnd.Next()).ToList();

        // Decide on a random count of elements to take (at least 1, up to the list's count)
        int count = rnd.Next(1, shuffled.Count + 1);

        // Take the first 'count' elements from the shuffled list
        return shuffled.Take(count).ToList();
    }
}
