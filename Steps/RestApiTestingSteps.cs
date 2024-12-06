using RestSharp;
using Newtonsoft.Json;
using System.Net;
using TechTalk.SpecFlow;
using Newtonsoft.Json.Linq;

namespace SpecFlowRestApi.Steps
{
    [Binding]
    public class RestApiTestingSteps
    {
        private RestClient? client;
        private RestRequest? request;
        private RestResponse? response;
        private string? authToken;


        [Given(@"Connection to the API and obtain authentication token")]
        public void GivenConnectToApiAndObtainToken()
        {
            client = new RestClient("https://restful-booker.herokuapp.com/");

            var authRequest = new RestRequest("auth", Method.Post);
            authRequest.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });

            var authResponse = client?.Execute(authRequest);

            var tokenData = JsonConvert.DeserializeObject<dynamic>(authResponse?.Content);
            authToken = tokenData?.token;
        }

        [Given(@"Connection to the API")]
        public void GivenConnectToApi()
        {
            client = new RestClient("https://restful-booker.herokuapp.com/");
        }
        [Given(@"Creation of GET request to get the list of all booking")]
        public void GivenCreateGetAllRequest()
        {
            request = new RestRequest("booking", Method.Get);

            response = client?.Execute(request);

            Assert.That(response?.IsSuccessful, Is.True);
        }

        [Given(@"Creation of GET request to fetch booking (.*) details")]
        public void GivenCreateGetRequest(int index)
        {
            JArray bookingList = JArray.Parse(response?.Content);
            int BookingId = (int)bookingList[index]["bookingid"];

            request = new RestRequest($"booking/{BookingId}", Method.Get);
            request.AddHeader("Accept", "application/json");
        }

        [Given(@"Creation of POST request to create a booking")]
        public void GivenCreatePostRequest()
        {
            request = new RestRequest("booking", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request?.AddHeader("Accept", "application/json");
            request?.AddJsonBody(new
            {
                firstname = "Jim",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2019-01-01"
                },
                additionalneeds = "Breakfast"
            });
        }

        [Given(@"Creation of PUT request to update (.*) booking")]
        public void GivenCreatePutRequest(int index)
        {
            JArray bookingList = JArray.Parse(response?.Content);
            int BookingId = (int)bookingList[index]["bookingid"];

            request = new RestRequest($"booking/{BookingId}", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Cookie", $"token={authToken}");
            request?.AddJsonBody(new
            {
                firstname = "Jim",
                lastname = "Upddaated",
                totalprice = 150,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2022-01-01",
                    checkout = "2022-01-10"
                },
                additionalneeds = "Breakfast"
            });
        }



        [Given(@"Creation of PATCH request to update (.*) booking")]
        public void GivenCreatePatchRequest(int index)
        {
            JArray bookingList = JArray.Parse(response?.Content);
            int BookingId = (int)bookingList[index]["bookingid"];

            request = new RestRequest($"booking/{BookingId}", Method.Patch);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Cookie", $"token={authToken}");
            request?.AddJsonBody(new
            {
                firstname = "Jim",
                lastname = "Upddaated"
            });
        }

        [Given(@"Creation of DELETE request to delete (.*) booking")]
        public void GivenCreateDeleteRequestWithToken(int index)
        {
            JArray bookingList = JArray.Parse(response?.Content);
            int BookingId = (int)bookingList[index]["bookingid"];

            request = new RestRequest($"booking/{BookingId}", Method.Delete);
            request.AddHeader("Content-Type", "application/json");
            request?.AddHeader("Cookie", $"token={authToken}");
        }

        [When(@"Sending the request")]
        public void WhenSendRequest()
        {
            response = client?.Execute(request);
        }

        [Then(@"Verify that the request is successful")]
        public void ThenResponseIsSuccess()
        {
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"Verify that the booking is created successfully")]
        public void ThenBookingIsCreated()
        {
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"Verify that the booking is updated successfully")]
        public void ThenBookingIsUpdated()
        {
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"Verify that the booking is successfully deleted")]
        public void ThenRecordIsDeleted()
        {
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
    }
}
