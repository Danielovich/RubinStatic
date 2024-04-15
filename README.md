### Rubin Static Site Generator

#### Dependencies:

- .NET 8
- Markdig
- AutoFixture
- Xunit

Very very few developers actually reads code, so instead I will share some pointers with a brief technical documentation.

### Technical Documentation

The solution is based on several projects but where only the Rubin.StaticSite.Console has mulitple dependencies to inter-solution projects. Rubin.StaticSite.Console relies on both Rubin.Static and Rubin.Markdown.

#### The API of Rubin.Markdown is basically where we communicate with  external infrastructure and parses formats:

- downloading markdown files from a repo of your choice, which basically represent [blog posts](https://github.com/Danielovich/markdownposts).
- [parsing](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown/Parsers/MarkdownPostParser.cs) those markdown files from a string to strongly typed model.
- you **MUST** [adjust the constants](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown/Constants.cs). Specifically the "MarkdownContentsUrl" should be added inside a appsettings.json file from where you use the API (see how the Rubin.StaticSite.Console does it). The Github client for [downloading the markdown files](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown/GithubClient/GitHubApiService.cs) will throw if not set.

You can use the API as a stand-alone API if you wish to utilize it from somewhere else than this solution. There is an [extenstion method you can use for this](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown/Extensions/StartupExtensions.cs).


#### The API of Rubin.Static gives you the possibility to generate static HTML files by utilizing the Razor syntax. The API...

- has its own Models which makes it independent from models in other projects, e.g: Rubin.Markdown.
- Views are cshtml files which can implement HTML, Razor and .NET code. Views can be located in Views and Views/Shared.
- Rendering a razor view outside a web application is [not exactly a non-trival challange](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Infrastructure/Renderer.cs), there is a internal dependency on a HostingEnvironment and I haven't found a way to discard the ServiceProvider as being used for service location. 
- The [AddRazorTemplating](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Infrastructure/StartupExtensions.cs) is an extension method which registers these dependencies.
- The presumable easist approach to understanding how the actual views are generated [is to follow along here](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Services/GenerateStaticService.cs).
- Markdig dependency is [only used here](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Services/TransformMarkdownToHTMLService.cs).
- If you are wondering what the fuzz about the Category-code is all about, consider it helpers for outputting e.g.: CategoryName(PostCount), e.g: "Sailing(19)".
- You can use this API from at least a console application and there is no dependencies to other internal projects.

#### Rubin.Markdown.Console is where it's all connected. I have tried to make it as slim as possible, and so...

- there is a wrapper around the IGenerateStatic [called PageGenerator](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown.Console/Generators/PageGenerator.cs).
- you can [control where the static HTML pages should be saved](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown.Console/Generators/FileSaver.cs), default is /Output dir which is stored in the obj/bin at runtim.
- the [SetupLayoutHelper is actually placed inside Rubin.Static](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Infrastructure/LayoutHelper.cs) because of its responsibilty to the _Layout view files. Layout views cannot have a strongly typed model so to overcome this I have made a singleton that one can use if one wishes to output [content which should be shared across all Views that use a Layout view](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Views/Shared/_Layout.cshtml). 
- the [GenerateCommand is a Command](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown.Console/GenerateCommand.cs) in the sense of System.CommandLine. I have nothing against this library but the dependency is quite large, so I will keep  it until I find a more suitable solution. The command acts as the executor of the PageGenator.
- posts are [converted from a MarkdownPost model to a Post model](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown.Console/Mappers/PostService.cs), and I have tried to keep the sharing of the two models as far away as I found possible.
- the program is executed with the command "generate".