using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Controllers
{
    ///   <summary>
    ///   Interface for different translators.
    ///   </summary>
    public interface ITranslator
    {
        ///<summary>Async method that translates input string into other language.</summary>
        ///<param name="input" type="string">Input to be translated.</param>
        ///<returns type="Task<string>">Translated string in task.</returns>
        Task<string> Translate(string input);
    }
}
