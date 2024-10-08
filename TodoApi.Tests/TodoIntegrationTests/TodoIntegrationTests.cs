using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.Utilities;

//using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using TodoApi.Models;
using Xunit;
using Xunit.Abstractions;

namespace TodoApi.Tests.TodoIntegrationTests
{
    public class TodoIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ITestOutputHelper _output;

        public TodoIntegrationTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;
        }

        [Fact]
        public async Task GetAll_Todo_Success()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Todo");

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            //response.Content.ToJson().ToList().Should().HaveCount(1);

            //var result = await response.Content.ReadAsStringAsync();
            //var obj = JsonConvert.DeserializeObject(result);
            //obj.Should().HaveCount(2);
        }

        [Fact]
        public async Task Post_Todo_Success()
        {
            var client = _factory.CreateClient();

            var payload = new
            {
                name = "Test",
                isComplete = true
            };

            var response = await client.PostAsJsonAsync("/api/Todo", payload);

            var todo = await JsonSerializer.DeserializeAsync<Todo>(
                await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
            );

            _output.WriteLine($"Id: {todo?.Id}");
            _output.WriteLine($"Name: {todo?.Name}");
            _output.WriteLine($"IsComplete: {todo?.IsComplete}");

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            Assert.Equal(payload.name, todo?.Name);
        }
    }
}