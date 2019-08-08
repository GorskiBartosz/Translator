using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Translator.Models;
using System.Diagnostics;

namespace Translator.Controllers
{
    public class HomeController : Controller
    {
        
        /// <summary>
        /// Action that responds to translations with JSON encoded strings.
        /// </summary>
        /// <param name="source">Text to be translated</param>
        /// <param name="translationType">Requested translation type. For now only leetspeak is supported.</param>
        /// <returns>
        /// JSON encoded respond with two fields:
        /// 1. success which is true if translation was successful, false otherwise
        /// 2. content which is translated string if translation was successful, null otherwise
        /// </returns>
        [Route("Home/Translate/{translationType}/{source}")]
        public ActionResult Translate(string source, string translationType)
        {
            try
            {
                ITranslatorFactory translatorFactory = BasicTranslatorFactory.Instance;
                ITranslator translator = translatorFactory.MakeTranslator(translationType);
                Task<string> task = Task.Run(async () => await translator.Translate(source));
                task.Wait();
                string result = task.Result;
               
                if (result != null)
                {
                    SaveToDatabase(source, translationType, result);
                    var output = new { success = true, content = result };
                    return Json(output, JsonRequestBehavior.AllowGet);
                }
                else throw new HttpRequestException();
            }
            catch (Exception)
            {
                var output = new { success = false, content = "" };
                return Json(output, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Saves record to database using EntityFramework
        /// </summary>
        /// <param name="source">Source word</param>
        /// <param name="translationType">Translation type used for processing</param>
        /// <param name="result">Output of translation</param>
        private void SaveToDatabase(string source, string translationType, string result)
        {
            var dbContext = new TranslatorDbContext();
            var translation = new Translation
            {
                Date = DateTime.Now,
                Source = source,
                TranslatedText = result,
                TranslationType = translationType
            };
            dbContext.Translations.Add(translation);
            dbContext.SaveChanges();
            dbContext.Dispose();
        }

        /// <summary>
        /// Basic Action for index page.
        /// </summary>
        /// <returns>Index Page View</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Basic Action for About page.
        /// </summary>
        /// <returns>About Page View</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Translator.";

            return View();
        }

        /// <summary>
        /// Basic Action for Contact page.
        /// </summary>
        /// <returns>Contact Page View</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }
    }
}