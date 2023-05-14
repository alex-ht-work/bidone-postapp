using System.IO.Abstractions;
using Microsoft.Extensions.Options;
using PostApp.Models;

namespace PostApp.Services.FileRepository;

public class FileRepository: IRepository
{
    private readonly IFileSystem _fileSystem;
    private readonly IOptions<FileRepositoryConfig> _config;
    private readonly ILogger<FileRepository> _logger;

    public FileRepository(IFileSystem fileSystem, IOptions<FileRepositoryConfig> config, ILoggerFactory loggerFactory)
    {
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