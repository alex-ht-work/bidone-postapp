using System.IO.Abstractions;
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
        _fileSystem = fileSystem;
        _config = config;
        _logger = loggerFactory.CreateLogger<FileRepository>();
    }
    
    public Task Save(Participant participant)
    {
        _logger.LogInformation("Saving participant: {Participant}", participant);

        throw new NotImplementedException();
    }
}