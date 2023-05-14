using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using Microsoft.Extensions.Hosting;
using PostApp.Models;

namespace PostApp.Tests;

public class BackendE2ETests
{
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
            }
        );

        await using HostRunner hostRunner = new(host);

        await Task.Delay(100);
        
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