using Microsoft.Extensions.DependencyInjection;
using Moq;
using name_sorter;
using name_sorter.Services.Interfaces;

public class AppTests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly Mock<INameSorter> _nameSorterMock;
    private readonly App _app;

    public AppTests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _nameSorterMock = new Mock<INameSorter>();

        var services = new ServiceCollection()
            .AddSingleton(_fileServiceMock.Object)
            .AddSingleton(_nameSorterMock.Object)
            .AddSingleton<App>()
            .BuildServiceProvider();

        _app = services.GetRequiredService<App>();
    }

    [Fact]
    public void Run_ShouldReadSortAndWriteNames()
    {
        // Arrange
        string inputFilePath = "input.txt";
        string outputFilePath = "sorted-names-list.txt";

        var unsortedNames = new List<string>
        {
            "Janet Parsons",
            "Vaughn Lewis",
            "Adonis Julius Archer"
        };

        var sortedNames = new List<string>
        {
            "Adonis Julius Archer",
            "Vaughn Lewis",
            "Janet Parsons"
        };

        _fileServiceMock.Setup(fs => fs.ReadNames(inputFilePath)).Returns(unsortedNames);
        _nameSorterMock.Setup(ns => ns.SortNames(unsortedNames)).Returns(sortedNames);
        _fileServiceMock.Setup(fs => fs.WriteNames(outputFilePath, sortedNames));

        // Act
        _app.Run(inputFilePath);

        // Assert
        _fileServiceMock.Verify(fs => fs.ReadNames(inputFilePath), Times.Once);
        _nameSorterMock.Verify(ns => ns.SortNames(unsortedNames), Times.Once);
        _fileServiceMock.Verify(fs => fs.WriteNames(outputFilePath, sortedNames), Times.Once);
    }

    [Fact]
    public void Run_ShouldHandleFileReadError()
    {
        // Arrange
        string inputFilePath = "invalid.txt";
        _fileServiceMock.Setup(fs => fs.ReadNames(inputFilePath)).Throws(new Exception("File not found"));

        // Act & Assert
        var ex = Record.Exception(() => _app.Run(inputFilePath));

        Assert.NotNull(ex);
        Assert.Contains("File not found", ex.Message);
    }
}
