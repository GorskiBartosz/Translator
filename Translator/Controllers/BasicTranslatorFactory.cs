using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translator.Controllers
{
    ///<summary>
    ///Basic Translator factory made as singleton.
    ///</summary>
    public class BasicTranslatorFactory : ITranslatorFactory
    {
        /// <summary>
        /// Read-only instance of BasicTranslatorFactory which is used for retrieving BasicTranslatorFactories instead of constructing them.
        /// </summary>
        public static readonly ITranslatorFactory Instance = new BasicTranslatorFactory();

        /// <summary>
        /// Private constructor because of the singleton pattern.
        /// </summary>
        private BasicTranslatorFactory(){}

        /// <summary>
        /// Main method which returns different translators according to given translatorName.
        /// </summary>
        /// <param name="translatorName">String which indicates requested translator.</param>
        /// <returns>Requested Translator object if translatorName is indicates any properly.</returns>
        /// <exception cref="ArgumentOutOfRangeException">when translatorName is not indicating any translator.</exception>
        public ITranslator MakeTranslator(string translatorName)
        {
            switch (translatorName)
            {
                case "leetspeak":
                    return new LeetTranslator(HttpClientSingleton.Instance);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}