using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Translator.Controllers;

namespace Translator.Tests.Controllers
{
    
    
    

    class FakeHttpMessageHandler : HttpMessageHandler
    {
        public FakeHttpMessageHandler(bool hasConnectionToApi)
        {
            HasConnectionToApi = hasConnectionToApi;
        }
        private readonly bool HasConnectionToApi;

        private class ResponseMessage
        {
            public SuccessMessage success { get; set; }
            public ContentMessage contents { get; set; }
           
        }

        private class SuccessMessage
        {
            public int total { get; set; }
        }

        private class ContentMessage
        {
            public string translation { get; set; }
            public string text { get; set; }
            public string translated { get; set; }
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
           
            if (request.RequestUri.Equals("https://api.funtranslations.com/translate/leetspeak.json"))
            {
                if (!HasConnectionToApi)
                {
                    throw new TaskCanceledException();
                }

                var responseContent = new ResponseMessage
                {
                    success = new SuccessMessage { total = 1 },
                    contents = new ContentMessage
                    {
                        text = "Hello World",
                        translation = "leetspeak",
                        translated = "helL0 wOr1|)"
                    }
                };
                var response = new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new ObjectContent<ResponseMessage>(
                        responseContent,
                        new JsonMediaTypeFormatter())
                };

                return Task.FromResult(response);
            }
            else
            {
                return Task.FromResult(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Content = new StringContent("Error")
                });
            }


          
            
        }
    }

    [TestClass]
    public class LeetTranslatorTest
    {
        [TestMethod]
        public void Translate_ProperRequestWasSent_ResponseIsRecieved()
        {
            HttpMessageHandler messageHandler = new FakeHttpMessageHandler(true);
            HttpClient client = new HttpClient(messageHandler);
            client.Timeout = TimeSpan.FromSeconds(10.0);
            ITranslator translator = new LeetTranslator(client);


            Task<string> task = Task.Run(async () => await translator.Translate("Hello World"));
            task.Wait();
            string result = task.Result;


            Assert.AreEqual(result, "helL0 wOr1|)");
        }

        [TestMethod]
        public void Translate_RequestWasSentButThereIsNoConnectionToApi_ReturnsNull()
        {
            HttpMessageHandler messageHandler = new FakeHttpMessageHandler(false);
            HttpClient client = new HttpClient(messageHandler);
            client.Timeout = TimeSpan.FromSeconds(1.0);
            ITranslator translator = new LeetTranslator(client);


            Task<string> task = Task.Run(async () => await translator.Translate("Hello World"));
            task.Wait();
            string result = task.Result;


            Assert.IsNull(result);
        }


    }
}
