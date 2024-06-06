![CI](https://github.com/Danielovich/RubinStatic/actions/workflows/dotnet.yml/badge.svg)

### What is this piece of software ?

- This is thought of as a local-first static blog/site generator. 
- It will generate a full static blog/site in HTML.
- It will generate HTML based on Markdown.
- HTML is rendered from the use of Razor views.
- Blog posts (essentially markdown) are not hosted within this generator.
- Blog posts are downloaded at runtime, from a github repo.
- Blog posts use markdown comments as blog post properties (slug, categories, publish date etc.).
- Generated HTML cannot be published anywhere from the generator.
- HTML is saved the execution path of the executable (your disk).

#### Markdown files acting as blog posts

[I have written some thoughts](designthoughts.md) about how I got here and why I ended up creating a new small static site generator. Thinking about really, it I hardly puts my reflective choice of building this in the category of "not invented here". I had an itch. I scratched it. 

### Now you know why this exists.

#### A Markdown file which portrays a blog post looks like this:

```
[//]: # "title: When I invented the wheel"
[//]: # "slug: i-circled-around"
[//]: # "pubDate: 14/12/1995 12:01"
[//]: # "lastModified: 14/05/2023 13:07"
[//]: # "excerpt: This is some really interesting stuff."
[//]: # "categories: engineering, wheels"
[//]: # "isPublished: true"

### Let me enlighten you!

This is some content I wrote and it's in a markdown file

And I found it easy enough...

> It will be transformed into HTML by a dependency and will look good if you drizzle a few rules of css on it.
```

(it could also look like this: https://github.com/Danielovich/markdownposts/blob/main/et-liv-i-programmering.md)


#### Some terminology

- MarkdownFile (Rubin.Markdown) - earliest artifact portraying a blog post
- MarkdownPost (Rubin.Markdown) - loosely typed MarkdownFile
- Post (Rubin.Static) - strongly typed MarkdownPost
- RenderedPage (Rubin.Static) - Strongly typed HTML representation of a Post
 
#### Build, Run and View result from a command line. (only tested this on windows)

```
git clone https://github.com/Danielovich/RubinStatic.git

cd RubinStatic

.\build.ps1

cd RubinStatic\src\Rubin.Static.Console\bin\Release\net8.0

.\Rubin.Static.Console.exe generate

cd RubinStatic\src\Rubin.Static.Console\bin\Release\net8.0\Views\Output

.\index.html
```

#### index.html, all.html etc.

The generated HTML can be structured how you wish it to be, really. The HTML is rendered by executing a Razor view, by default each view has a model. Index.cshtml looks like this.

```
@using System.Web
@using Microsoft.AspNetCore.Html
@using Rubin.Static
@using Rubin.Static.ViewModels
@using Rubin.Static.Extensions
@model IndexPageViewModel

@{
    Layout = "_Layout";
    ViewData["Title"] = "Frontpage";
}

@foreach (var post in Model.Posts)
{
    var htmlContent = new HtmlString(post.HtmlContent.Content);

    <div class="content-container">
        <div class="main-content">

            <h3 class="post-title"><a href="@post.Slug.ToUri()">@post.Title.PostTitle</a></h3>

            <p class="published">@post.PublishedDate.ToLongDateString()</p>

            @htmlContent
        </div>
    </div>
}
```

And the Layout (a shared view), which "wraps" around the given view looks like this.

```
@using Rubin.Static
@using Rubin.Static.Infrastructure
@using Rubin.Static.Extensions
@using Rubin.Static.Rendering
@{
    var categories = await SharedViewViewModel.Instance.GetCategoryPosts();
}
<html>
<head>
    <title>Blog name - @ViewData["Title"]</title>
    <link rel="stylesheet" href="Assets/styles.css" />
</head>

<body>
    <div class="menu">
        <span class="frontpage-identifier"><a href="index.html">Frontpage</a> | <a href="all.html">All posts</a></span>
    </div>

    @RenderBody()

    <div class="menu">
        <span class="frontpage-identifier"><a href="index.html">Frontpage</a> | <a href="all.html">All posts</a> | </span>
        @{
            foreach (var item in categories)
            {
                <span><a href="@item.Key.Slug.ToUri()">@item.Key.Title (@item.Value.Count())</a> | </span>
            }
        }

    </div>
</body>

</html>
```

There are a few views by default.

- index.cshtml will render a number of posts .
- all.cshtml will render all posts.
- post.cshtml will be rendered as the slug of a post, hence it will be a post.
- category.cshtml will be rendered as a category value/name and render links to posts within each category. 

You can adjust this to your liking of course.

#### Dependencies:

- .NET 8 
- Markdig
- AutoFixture
- Xunit

### Technical Documentation

It is said that very few developers actually reads code they are depended on, so based on that sentiment I will share some pointers with a brief technical documentation.

The solution is based on several projects. Only the Rubin.Static.Console has multiple dependencies to inter-solution projects. Rubin.Static.Console relies on both Rubin.Static and Rubin.Markdown.

#### The API of Rubin.Markdown is basically where we communicate with the markdown git repository and parses that markdown:

- downloading markdown files from a repo of your choice, which basically represent [blog posts](https://github.com/Danielovich/markdownposts). They utilize markdown comments as properties.
- [parsing](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown/Parsers/MarkdownPostParser.cs) those markdown files from a string to strongly typed model.
- you **MUST** [change the constants](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown/Constants.cs). Specifically the "MarkdownContentsUrl" should be added inside a appsettings.json file from where you use the API (see how the Rubin.Static.Console does it). The Github client for [downloading the markdown files](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown/GithubClient/GitHubApiService.cs) will throw an exception if not set.

You can use the API as a "stand-alone" API if you wish to utilize it from somewhere else than this solution. There is an [extenstion method you can use for this](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Markdown/Extensions/StartupExtensions.cs).


#### The API of Rubin.Static gives you the possibility to generate HTML files by utilizing the Razor syntax. 

- The API has its own Models which makes it independent from models in other projects, e.g: Rubin.Markdown.
- Views are cshtml files which can hold HTML, [Razor](https://www.w3schools.com/asp/razor_syntax.asp) and .NET code. Views can be located in Views and Views/Shared.
- Rendering a razor view outside a web application is [not exactly a non-trival challange](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Rendering/Renderer.cs), there is a internal dependency on a HostingEnvironment and I haven't found a way to discard the ServiceProvider as being used for service location.
- The [AddRazorTemplating](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Infrastructure/StartupExtensions.cs) and [AddStatic](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Infrastructure/StartupExtensions.cs) are extension methods which registers these dependencies.
- The presumable easist approach to understanding how the actual views are generated [is to follow along here](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Rendering/PageRendering.cs).
- Markdig dependency is [only used here](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Services/TransformMarkdownToHTMLService.cs).
- You can use this API from at least a console application and there is no dependencies to other internal projects.

#### Rubin.Static.Console is where the pieces are connected and a blog/site is generated. I have tried to make it as slim as possible, and so...

- there is a type implementing the IGeneratePages [called PageGenerator](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static.Console/Generators/PageGenerator.cs).
- you can [control where the static HTML pages should be saved](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static.Console/Generators/ISavePage.cs), default is [Output](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static.Console/Generators/SaveAsFile.cs) dir which is located in bin\Release\net8.0\Views\Output at runtime.
- the [SharedViewViewModel](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Rendering/SharedViewViewModel.cs) exists because of its responsibilty to the Shared View (_Layout) files. Layout views cannot have a strongly typed model so to overcome this it utilizes the SharedViewViewModel as a singleton that one can use if one wishes to output [content which should be shared across all Views that use a Layout view](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static/Views/Shared/_Layout.cshtml). 
- the [GenerateCommand is a Command](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static.Console/GenerateCommand.cs) in the sense of System.CommandLine. I have nothing against this library but the dependency is quite large, so I will keep it until I find a more suitable solution. The command is calling into a IGeneratePages type.
- posts are [converted from a MarkdownPost model to a Post model](https://github.com/Danielovich/RubinStatic/blob/main/src/Rubin.Static.Console/Mappers/PostService.cs), and I have tried to keep the sharing of the two models as far away as I found possible.
- the program is executed with the command "generate".