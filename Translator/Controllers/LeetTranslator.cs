using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Translator.Models;

namespace Translator.Controllers
{
    ///<summary>LeetSpeak translator which uses API - https://api.funtranslations.com for translation.</summary>
    public class LeetTranslator : ITranslator
    {
        /// <summary>
        /// HttpClient which is used for sending API calls.
        /// </summary>
        private readonly HttpClient Client;
        /// <summary>
        /// Url of the leetspeak API
        /// </summary>
        private readonly string Url = "https://api.funtranslations.com/translate/leetspeak.json";

        /// <summary>
        /// Constructs LeetTranslator with given HttpClient.
        /// </summary>
        /// <param name="client">HttpClient to be set as Client property</param>
        public LeetTranslator(HttpClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Async method that translates given input to LeetSpeak.
        /// </summary>
        /// <param name="input">Input to be translated</param>
        /// <returns>LeetSpeak translation</returns>
        public async Task<string> Translate(string input)
        {
            try
            {
                var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("text", input),
                });
                HttpResponseMessage response = await Client.PostAsync(Url, requestContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                response.Dispose();
                dynamic responseObject = JObject.Parse(responseBody);
                return responseObject.contents.translated;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}