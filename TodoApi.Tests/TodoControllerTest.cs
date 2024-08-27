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

namespace TodoApi.Tests
{
    public class TodoControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ITestOutputHelper _output;

        public TodoControllerTest(WebApplicationFactory<Program> factory, ITestOutputHelper output)
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

            //var httpContent = new StringContent(JsonSerializer.Serialize(todopayload));
            //var response = await client.PostAsync("/api/Todo", httpContent);

            //var payload = JsonContent.Create(todopayload);

            var response = await client.PostAsJsonAsync("/api/Todo", new
            {
                name = "Test",
                isComplete = true
            });

            //response.EnsureSuccessStatusCode();
            //response.StatusCode.Should().Be(HttpStatusCode.Created);

            //var body = await response.Content.ReadAsStringAsync();

            var todo = await JsonSerializer.DeserializeAsync<Todo>(
                await response.Content.ReadAsStreamAsync(), 
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
            );

            _output.WriteLine($"todo: {todo}");
            _output.WriteLine($"Id: {todo?.Id}");
            _output.WriteLine($"Name: {todo?.Name}");
            _output.WriteLine($"IsComplete: {todo?.IsComplete}");
        }
    }
}