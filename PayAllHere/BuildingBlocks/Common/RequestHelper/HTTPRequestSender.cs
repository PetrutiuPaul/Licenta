using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestHelper
{
    public static class HTTPRequestSender
    {
        /// <summary>
        /// Makes a GET call to an url
        /// </summary>
        /// <param name="url"></param>
        /// <returns>
        /// Returns json response string if call is successful, throws error json response as exception message otherwise
        ///</returns>
        public static async Task<string> GetAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(responseString);
                }
                return responseString;
            }
        }

        /// <summary>
        /// Makes a post call with a json body to an url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="payload"></param>
        /// <param name="applyBlackListFilter">blacklist filter will be called if this is set to true</param>
        /// <returns>
        /// Returns json response string if call is successful, throws error json response as exception message otherwise
        ///</returns>
        public static async Task<string> PostAsync(string url, object payload)
        {
            using (var client = new HttpClient())
            {
                var jsonInString = JsonConvert.SerializeObject(payload);

                var response = await client.PostAsync(url, new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(responseString);
                }

                return responseString;
            }
        }

        public static async Task<string> DeleteAsync(string url, object payload)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync(url);

                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(responseString);
                }

                return responseString;
            }
        }
    }
}
