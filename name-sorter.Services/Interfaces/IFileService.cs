namespace name_sorter.Services.Interfaces
{
    public interface IFileService
    {
        List<string> ReadNames(string filePath);
        void WriteNames(string filePath, List<string> names);
    }
}
