using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

namespace Rubin.Static.Rendering;

public class Renderer : IRenderer
{
    private readonly IServiceProvider _serviceProvider;

    public Renderer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    /// <summary>
    /// <seealso cref="IRenderer"/>
    /// </summary>
    public async Task<string> RenderViewToPageAsync<TModel>(string viewPath, TModel model)
    {
        var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

        // will be the carrier for the rendered view which is essentially an HTML page
        using (var htmlWriter = new StringWriter())
        {
            var viewEngine = _serviceProvider.GetRequiredService<IRazorViewEngine>();
            var viewResult = viewEngine.FindView(actionContext, viewPath, false);

            if (viewResult.View == null)
            {
                throw new ArgumentNullException($"{viewPath} does not match any available view");
            }

            var viewDictionary = new ViewDataDictionary<TModel>(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary())
            { Model = model };

            var viewContext = new ViewContext(
                actionContext,
                viewResult.View,
                viewDictionary,
                new TempDataDictionary(actionContext.HttpContext, _serviceProvider.GetRequiredService<ITempDataProvider>()),
                htmlWriter,
                new HtmlHelperOptions());

            await viewResult.View.RenderAsync(viewContext);
            return htmlWriter.ToString();
        }
    }
}
