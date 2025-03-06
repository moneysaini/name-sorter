using name_sorter.Services.Interfaces;

namespace name_sorter.Services.Implementations
{
    public class FileService : IFileService
    {
        public List<string> ReadNames(string filePath)
        {
            return File.ReadAllLines(filePath).ToList();
        }

        public void WriteNames(string filePath, List<string> names)
        {
            File.WriteAllLines(filePath, names);
        }
    }
}
