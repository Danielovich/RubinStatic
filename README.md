### Rubin Static HTML Generator from Github repo and markdown files

![](https://raw.githubusercontent.com/Danielovich/RubinStatic/main/doc/assets/basicdesc.jpg)

#### Markdown files acting as blog posts

[I have written some thoughts](designthoughts.md) about how I got here and why I ended up creating a new small static site generator. Thinking about it I hardly put my reflective choice of building this in the category of "not invented here". I had an itch. I scratched it.

#### Some terminology

- MarkdownFile (Rubin.Markdown) - earliest artifact to portray a blog post
- MarkdownPost (Rubin.Markdown) - loosely typed MarkdownFile
- Post (Rubin.Static) - strongly typed MarkdownPost
- RenderedPage (Rubin.Static) - Strongly typed HTML representation of a Post


#### If you know what you're doing!

Open the .sln with VS 20*. Set Rubin.Static.Console as start project, hit F5 and wait until the console is done working its thing. Open up "Rubin.Static.Console\bin\Debug\net8.0\Views\Output" and view Index.html in your browser.

Now you can try and change the appsettings.json file in the same project, and point to your own github repo where you host your own markdown files acting as blog posts.

#### Dependencies:

- .NET 8 
- Markdig
- AutoFixture
- Xunit

It is said that very few developers actually reads code they are depended on, so based on that sentiment I will share some pointers with a brief technical documentation.

### Technical Documentation

The solution is based on several projects. Only the Rubin.Static.Console has multiple dependencies to inter-solution projects. Rubin.Static.Console relies on both Rubin.Static and Rubin.Markdown.

#### The API of Rubin.Markdown is basically where we communicate with external "infrastructure" and parses formats:

- downloading markdown files from a repo of your choice, which basically represent [blog posts](https://github.com/Danielovich/markdownposts). They utilize markdown comments as properties.
- [parsing](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown/Parsers/MarkdownPostParser.cs) those markdown files from a string to strongly typed model.
- you **MUST** [adjust the constants](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown/Constants.cs). Specifically the "MarkdownContentsUrl" should be added inside a appsettings.json file from where you use the API (see how the Rubin.Static.Console does it). The Github client for [downloading the markdown files](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown/GithubClient/GitHubApiService.cs) will throw an exception if not set.

You can use the API as a "stand-alone" API if you wish to utilize it from somewhere else than this solution. There is an [extenstion method you can use for this](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown/Extensions/StartupExtensions.cs).


#### The API of Rubin.Static gives you the possibility to generate static HTML files by utilizing the Razor syntax. 

- The API has its own Models which makes it independent from models in other projects, e.g: Rubin.Markdown.
- Views are cshtml files which can hold HTML, [Razor](https://www.w3schools.com/asp/razor_syntax.asp) and .NET code. Views can be located in Views and Views/Shared.
- Rendering a razor view outside a web application is [not exactly a non-trival challange](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Rendering/Renderer.cs), there is a internal dependency on a HostingEnvironment and I haven't found a way to discard the ServiceProvider as being used for service location.
- The [AddRazorTemplating](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Infrastructure/StartupExtensions.cs) and [AddStatic](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Infrastructure/StartupExtensions.cs) are extension methods which registers these dependencies.
- The presumable easist approach to understanding how the actual views are generated [is to follow along here](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Rendering/PageRendering.cs).
- Markdig dependency is [only used here](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Services/TransformMarkdownToHTMLService.cs).
- You can use this API from at least a console application and there is no dependencies to other internal projects.

#### Rubin.Static.Console is where it's all connected. I have tried to make it as slim as possible, and so...

- there is a type implementing the IGeneratePages [called PageGenerator](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static.Console/Generators/PageGenerator.cs).
- you can [control where the static HTML pages should be saved](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static.Console/Generators/ISavePage.cs), default is [/Output](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static.Console/Generators/SaveAsFile.cs) dir which is stored in the obj/bin at runtime.
- the [SharedViewViewModel](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Rendering/SharedViewViewModel.cs) exists because of its responsibilty to the Shared View (_Layout) files. Layout views cannot have a strongly typed model so to overcome this it utilizes the SharedViewViewModel as a singleton that one can use if one wishes to output [content which should be shared across all Views that use a Layout view](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Views/Shared/_Layout.cshtml). 
- the [GenerateCommand is a Command](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static.Console/GenerateCommand.cs) in the sense of System.CommandLine. I have nothing against this library but the dependency is quite large, so I will keep it until I find a more suitable solution. The command is calling into a IGeneratePages type.
- posts are [converted from a MarkdownPost model to a Post model](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static.Console/Mappers/PostService.cs), and I have tried to keep the sharing of the two models as far away as I found possible.
- the program is executed with the command "generate".