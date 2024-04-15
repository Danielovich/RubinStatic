using Microsoft.Extensions.DependencyInjection;
using Rubin.Markdown.Console.Extensions;
using System.CommandLine;


var services = new ServiceCollection();
services.Setup();

using var serviceProvider = services.BuildServiceProvider();

// entry to run app
var rootCommand = new RootCommand("Static Site Generation");

var commands = serviceProvider.GetServices<Command>();
commands.ToList().ForEach(command => rootCommand.AddCommand(command));

await rootCommand.InvokeAsync(args);