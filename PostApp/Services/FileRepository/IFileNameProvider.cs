using PostApp.Models;

namespace PostApp.Services.FileRepository;

public interface IFileNameProvider
{
    string GetFileNameFor(Participant participant);
}