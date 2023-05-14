namespace PostApp.Tests;

public class TempFolder: IDisposable
{
    public string Path { get; }
    
    public TempFolder()
    {
        Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(Path);
    }
    
    public void Dispose()
    {
        Directory.Delete(Path, true);
    }
}