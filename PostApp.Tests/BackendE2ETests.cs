using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PostApp.Models;
using Xunit.Abstractions;

namespace PostApp.Tests;

public class BackendE2ETests
{
    private readonly ITestOutputHelper _outputHelper;

    public BackendE2ETests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
    
    [Fact]
    async Task SavesSingleParticipant()
    {
        const string listenUrl = "http://localhost:7777";
        using TempFolder tempFolder = new();
        
        using var host = PostApp.Program.BuildHost
        (
            new string[]
            {
                $"--Repo:DataFolderPath={tempFolder.Path}",
                $"--URLS={listenUrl}"
            },
            
            services => services.AddLogging((builder) => builder.AddXUnit(_outputHelper))
        );

        await using HostRunner hostRunner = new(host);

        // wait for host to be fully started
        try
        {
            await Task.Delay(-1, host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStarted);
        }
        catch (TaskCanceledException) { }
        
        Participant participant = new("TestFirstName", "TestLastName");

        HttpClient client = new();
        var responseMessage = await client.PostAsync($"{listenUrl}/api/save", JsonContent.Create(participant));
        responseMessage.EnsureSuccessStatusCode();

        string filePath = Directory.GetFiles(tempFolder.Path).Single();
        string fileContents = await File.ReadAllTextAsync(filePath);
        Participant received = JsonSerializer.Deserialize<Participant>(fileContents)!;
        received.Should().Be(participant);
    }
}