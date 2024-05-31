using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit;

namespace AmusedTestProject
{
    public class AmusedTest
    {
        private const string BaseUrl = "https://api.restful-api.dev";
        private RestClient client;

        public AmusedTest()
        {
            client = new RestClient(BaseUrl);
        }

        [Fact]
        public async Task GetListOfAllObjects()
        {
            var request = new RestRequest("/objects", Method.Get);
            var response = await client.ExecuteAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.False(string.IsNullOrEmpty(response.Content));

        }

        [Fact]
        public async Task AddObject()
        {
            var request = new RestRequest("/objects", Method.Post);
            request.AddJsonBody(new { name = "Amused Phone 2024 Pro", data = new { year = "2024", price = "380000.00", CPU = "snapdragon 835", storage = "512 GB" } });

            var response = await client.ExecuteAsync(request);


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.False(string.IsNullOrEmpty(response.Content));

        }


        [Fact]
        public async Task GetSingleObject()
        {
            var createRequest = new RestRequest("/objects", Method.Post);
            createRequest.AddJsonBody(new { name = "Amused Phone 2024 Pro Max", data = new { year = "2024", price = "480000.00", CPU = "snapdragon 836", storage = "1 TB" } });
            var createResponse = await client.ExecuteAsync(createRequest);

            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var createdObjectId = JObject.Parse(createResponse.Content)["id"].ToString();

            var request = new RestRequest($"/objects/{createdObjectId}", Method.Get);
            var response = await client.ExecuteAsync(request);

            //response.StatusCode.Should().Be(HttpStatusCode.OK);
            //response.Content.Should().Contain("Amused Phone 2024 Pro Max");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.False(string.IsNullOrEmpty(response.Content));
            Assert.Contains("Amused Phone 2024 Pro Max", response.Content);
        }


        [Fact]
        public async Task UpdateObject()
        {
            var createRequest = new RestRequest("/objects", Method.Post);
            createRequest.AddJsonBody(new { name = "Amused Phone 2024 Pro", data = new { year = "2024", price = "380000.00", CPU = "snapdragon 835", storage = "512 GB" } });
            var createResponse = await client.ExecuteAsync(createRequest);

            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var createdObjectId = JObject.Parse(createResponse.Content)["id"].ToString();

            var updateRequest = new RestRequest($"/objects/{createdObjectId}", Method.Put);
            updateRequest.AddJsonBody(new { name = "Amused Phone 2024 Ultra Max", data = new { year = "2024", price = "400000.00", CPU = "snapdragon 837", storage = "1 TB" } });
            var updateResponse = await client.ExecuteAsync(updateRequest);

            //updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            // updateResponse.Content.Should().Contain("Amused Phone 2024 Ultra Max");

            Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
            Assert.False(string.IsNullOrEmpty(updateResponse.Content));
            Assert.Contains("Amused Phone 2024 Ultra Max", updateResponse.Content);

        }

        [Fact]
        public async Task DeleteObject()
        {
            var createRequest = new RestRequest("/objects", Method.Post);
            createRequest.AddJsonBody(new { name = "Amused Phone 2024 Pro", data = new { year = "2024", price = "380000.00", CPU = "snapdragon 835", storage = "512 GB" } });
            var createResponse = await client.ExecuteAsync(createRequest);

            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var createdObjectId = JObject.Parse(createResponse.Content)["id"].ToString();

            var deleteRequest = new RestRequest($"/objects/{createdObjectId}", Method.Delete);
            var deleteResponse = await client.ExecuteAsync(deleteRequest);

            //deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);

        }


    }
}
