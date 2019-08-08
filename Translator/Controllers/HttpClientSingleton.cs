using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Translator.Controllers
{
    /// <summary>
    /// Simple extension for HttpClient that makes this class implementing singleton pattern.
    /// </summary>
    public class HttpClientSingleton : HttpClient
    {
        /// <summary>
        /// Private constructor because of the singleton pattern.
        /// </summary>
        private HttpClientSingleton(){}

        /// <summary>
        /// Readonly instance of HttpClientSingleton which is used for retriveing HttpClientSingletons instead of constructing them.
        /// </summary>
        public static readonly HttpClientSingleton Instance = new HttpClientSingleton
        {
            Timeout = TimeSpan.FromSeconds(10.0)
        };

    }
}