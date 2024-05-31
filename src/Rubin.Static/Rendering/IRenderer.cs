namespace Rubin.Static.Rendering;

public interface IRenderer
{
    /// <summary>
    /// Looks for a (MVC/Razor) view and renders the content based on the given Model
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="viewPath">Path to the View</param>
    /// <param name="model">A ViewModel</param>
    /// <returns>Rendered HTML content as a string</returns>
    /// <exception cref="ArgumentNullException">If view is not found</exception>
    Task<string> RenderViewToPageAsync<TModel>(string viewPath, TModel model);
}
