using PostApp.Models;

namespace PostApp.Services.FileRepository;

public class FileNameProvider: IFileNameProvider
{
    public string GetFileNameFor(Participant participant)
    {
        return $"{Guid.NewGuid()}.json";
    }
}