using System.IO.Abstractions.TestingHelpers;
using System.Text.Json;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using PostApp.Models;
using PostApp.Services.FileRepository;

namespace PostApp.Tests;

public class FileRepositoryTests
{
    [Fact]
    public async Task SavesSingleParticipant()
    {
        const string fileName = "Test file name.json";
        const string dataPath = "/var/my-test-path/data";
        Participant participant = new("TestFirstName", "TestLastName");
        var mockFilenameProvider = new Mock<IFileNameProvider>();
        mockFilenameProvider.Setup(x => x.GetFileNameFor(It.Is<Participant>(p => p == participant)))
                            .Returns(fileName);
        var mockOptions = new Mock<IOptions<FileRepositoryConfig>>();
        mockOptions.Setup(x => x.Value).Returns(new FileRepositoryConfig(){DataFolderPath="/var/my-test-path/data"});

        var mockFileSystem = new MockFileSystem();
        
        var testee = new FileRepository(mockFilenameProvider.Object, mockFileSystem, mockOptions.Object, new NullLoggerFactory());

        await testee.Save(participant);

        string fileContents = await mockFileSystem.File.ReadAllTextAsync(Path.Combine(dataPath, fileName));
        Participant received = JsonSerializer.Deserialize<Participant>(fileContents)!;
        received.Should().Be(participant);
    }
}