namespace Rubin.Static.Infrastructure;

public interface IRenderer
{
    Task<string> RenderViewToStringAsync<TModel>(string viewPath, TModel model);
}
