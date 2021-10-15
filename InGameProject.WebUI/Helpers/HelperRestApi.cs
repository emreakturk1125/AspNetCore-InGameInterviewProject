using InGameProject.WebUI.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGameProject.WebUI.Helpers
{
    public class HelperRestApi<T>
    {
        public ServiceRootList<T> ConnectRestApiForList(string address, string token)
        {
            var client = new RestClient(address);
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "d8d18bd8-81f9-0203-3427-29de49d8bf1d");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", $"Bearer {token}");
            IRestResponse response = client.Execute(request);
            var resultFromResponse = JsonConvert.DeserializeObject<ServiceRootList<T>>(response.Content);

            return resultFromResponse;
        }

        public ServiceRoot<T> ConnectRestApi(string address, string token)
        {
            var client = new RestClient(address);
            var request = new RestRequest(Method.POST);
            if (token != null)
            {
                request.AddHeader("authorization", $"Bearer {token}");
            }
            IRestResponse response = client.Execute(request);
            var resultFromResponse = JsonConvert.DeserializeObject<ServiceRoot<T>>(response.Content);

            return resultFromResponse;
        }
    }
}
