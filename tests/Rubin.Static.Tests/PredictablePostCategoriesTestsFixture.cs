namespace Rubin.Static.Tests;

/// <summary>
/// This type is for making testing Post Categories easier.
/// Categories can be "set" on a Post and here we set a random number of categories 
/// </summary>
public class PredictablePostCategoriesTestsFixture
{
    public Fixture Fixture { get; private set; }

    public PredictablePostCategoriesTestsFixture()
    {
        Fixture = new Fixture();

        // customize fixture since we need each post to have a predictable number and names of categories 
        Fixture.Customize<IEnumerable<Category>>(p => p.FromFactory(() =>
        {
            return PostCategoriesCustomization.RandomPickedCategories("working, personal,   code, food, professional");
        }));
        
        Fixture.Customize<Post>(c => c.FromFactory(() => new Post(
            Fixture.Create<Title>(),
            Fixture.Create<HtmlContent>(),
            Fixture.Create<IEnumerable<Category>>(), //this will use the Category customization
            Fixture.Create<Slug>(),
            Fixture.Create<DateTime>(),
            true
        )));
    }
}
