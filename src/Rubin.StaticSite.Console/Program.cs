var services = new ServiceCollection();
services.Setup();

using var serviceProvider = services.BuildServiceProvider();

// entry to run app
var rootCommand = new RootCommand("Static Blog Generation");

var commands = serviceProvider.GetServices<Command>();
commands.ToList().ForEach(command => rootCommand.AddCommand(command));

await rootCommand.InvokeAsync(args);