using PostApp.Models;

namespace PostApp.Services;

public interface IRepository
{
    Task Save(Participant participant);
}