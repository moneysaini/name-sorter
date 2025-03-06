using name_sorter.Services.Interfaces;

namespace name_sorter
{
    public class App
    {
        private readonly IFileService _fileService;
        private readonly INameSorter _nameSorter;

        public App(IFileService fileService, INameSorter nameSorter)
        {
            _fileService = fileService;
            _nameSorter = nameSorter;
        }

        public void Run(string inputFilePath)
        {
            string outputFilePath = "sorted-names-list.txt";

            try
            {
                List<string> names = _fileService.ReadNames(inputFilePath);
                List<string> sortedNames = _nameSorter.SortNames(names);

                foreach (var name in sortedNames)
                {
                    Console.WriteLine(name);
                }

                _fileService.WriteNames(outputFilePath, sortedNames);
                Console.WriteLine($"Sorted names saved to {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}

