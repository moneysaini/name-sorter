using Microsoft.Extensions.DependencyInjection;
using name_sorter;
using name_sorter.Services.Implementations;
using name_sorter.Services.Interfaces;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: name-sorter <file-path>");
            return;
        }

        // Configure DI
        var serviceProvider = ConfigureServices();

        // Resolve dependencies
        var app = serviceProvider.GetRequiredService<App>();

        // Run the application
        app.Run(args[0]);
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Register services with DI
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<INameSorter, LastNameSorter>();
        services.AddSingleton<App>();

        return services.BuildServiceProvider();
    }
}