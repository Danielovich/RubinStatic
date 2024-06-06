namespace Rubin.Static.Tests;

/// <summary>
/// A helping type for testing post categories
/// var categories = PostCategoriesCustomization.RandomPickedCategories("Technology,Health,Science,Education,Entertainment");
/// might return "Entertainment,Technology,Education"
/// </summary>

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

        // random count of elements
        int count = rnd.Next(1, shuffled.Count + 1);

        // Take 'count' elements 
        return shuffled.Take(count).ToList();
    }
}
