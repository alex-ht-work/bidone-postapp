using System.IO.Abstractions;
using System.Text.Json;
using Microsoft.Extensions.Options;
using PostApp.Models;

namespace PostApp.Services.FileRepository;

public class FileRepository: IRepository
{
    private readonly IFileNameProvider _fileNameProvider;
    private readonly IFileSystem _fileSystem;
    private readonly IOptions<FileRepositoryConfig> _config;
    private readonly ILogger<FileRepository> _logger;

    public FileRepository(IFileNameProvider fileNameProvider, IFileSystem fileSystem, IOptions<FileRepositoryConfig> config, ILoggerFactory loggerFactory)
    {
        _fileNameProvider = fileNameProvider;
        _fileSystem = fileSystem;  // file system abstracted for testability
        _config = config;
        _logger = loggerFactory.CreateLogger<FileRepository>();
    }
    
    public async Task Save(Participant participant)
    {
        _logger.LogInformation("Saving participant: {Participant}", participant);

        string directoryPath = _config.Value.DataFolderPath;

        _fileSystem.Directory.CreateDirectory(directoryPath);
        
        await _fileSystem.File.WriteAllTextAsync
        (
            Path.Combine(directoryPath, _fileNameProvider.GetFileNameFor(participant)),
            JsonSerializer.Serialize(participant)
        );
    }
}