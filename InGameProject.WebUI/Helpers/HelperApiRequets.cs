using InGameProject.WebUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InGameProject.WebUI.Helpers
{
    public  class HelperApiRequets<T>
    {
        public async Task<ServiceRootList<T>> ApiRequest(string url)
        {
            HelperAPI _api = new HelperAPI();
            HttpResponseMessage res;
            var resultFromApi = "";
            HttpClient client = _api.Initial();

            res = await client.GetAsync("api/products/getall");
            resultFromApi = res.Content.ReadAsStringAsync().Result;
            ServiceRootList<T> rootForProduct = JsonConvert.DeserializeObject<ServiceRootList<T>>(resultFromApi);

            return rootForProduct;
        }
    }
}
