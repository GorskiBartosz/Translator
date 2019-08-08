using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translator.Models
{
    /// <summary>
    /// Class that represents one record in translations table in the database.
    /// </summary>
    public class Translation
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string TranslationType{ get; set; }
        public string TranslatedText { get; set; }
        public DateTime Date { get; set; }
    }
}