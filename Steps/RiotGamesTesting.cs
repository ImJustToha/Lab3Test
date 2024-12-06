using RestSharp;
using Newtonsoft.Json;
using System.Net;
using TechTalk.SpecFlow;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace SpecFlowRestApi.Steps
{
    [Binding]
    public class RiotGamesTesting
    {
        private const string region = "europe";
        private const string platform = "eun1";
        private const string api_key = "RGAPI-aafc0616-7046-4ee5-aa3f-99faac777073";

        private RestClient? client;
        private RestRequest? request;
        private RestResponse? response;

        [Given(@"Creation of GET request to get (.*) data with (.*) tag")]
        public void CreationOfGETRequestToGetUserDataWithTag(string userName, string userTag)
        {
            client = new RestClient($"https://{region}.api.riotgames.com/");//cnnection

            request = new RestRequest($"/riot/account/v1/accounts/by-riot-id/{userName}/{userTag}", Method.Get);

            request.AddHeader("X-Riot-Token", $"{api_key}");

        }

        [Given(@"Creation of GET request to get summoner information")]
        public void CreationOfGETRequestToGetUserSummonerInformation()
        {

            response = client?.Execute(request);
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var objects = JObject.Parse(response?.Content);
            string puuid = (string)objects["puuid"];

            client = new RestClient($"https://{platform}.api.riotgames.com/");

            request = new RestRequest($"lol/summoner/v4/summoners/by-puuid/{puuid}", Method.Get);
            request.AddHeader("X-Riot-Token", $"{api_key}");

        }

        [Given(@"Creation of GET request to receive the list of free champions")]
        public void CreationOfGETRequestToReceiveTheListOfFreeChampions()
        {


            client = new RestClient($"https://{platform}.api.riotgames.com/");

            request = new RestRequest($"/lol/platform/v3/champion-rotations", Method.Get);

            request.AddHeader("X-Riot-Token", $"{api_key}");
        }

        [Given(@"Creation of GET request to get all champion mastery entries")]
        public void CreationOfGETRequestToGetAllChamponMasteryEntries()
        {
            response = client?.Execute(request);
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var objects = JObject.Parse(response?.Content);
            string puuid = (string)objects["puuid"];

            client = new RestClient($"https://{platform}.api.riotgames.com/");

            request = new RestRequest($"/lol/champion-mastery/v4/champion-masteries/by-puuid/{puuid}", Method.Get);

            request.AddHeader("X-Riot-Token", $"{api_key}");
        }
        [Given(@"Creation a GET request to get a champion (.*) mastery")]
        public void CreationAGETRequestToGetAChampionMastery(int ChampID)
        {
            response = client?.Execute(request);
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var objects = JObject.Parse(response?.Content);
            string puuid = (string)objects["puuid"];

            client = new RestClient($"https://{platform}.api.riotgames.com/");

            request = new RestRequest($"/lol/champion-mastery/v4/champion-masteries/by-puuid/{puuid}/by-champion/{ChampID}", Method.Get);

            request.AddHeader("X-Riot-Token", $"{api_key}");
        }
        [When(@"Sending the request to the riot API")]
        public void SendingTheRequestToTheRiotAPI()
        {
            response = client?.Execute(request);
        }

        [Then(@"Verify that the riot request is successful")]
        public void VarifyThatTheRequestIsSuccesful()
        {
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
