using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Controllers
{
    ///<summary>Interface for factories that create different Translators.</summary>
    public interface ITranslatorFactory
    {
        ///<summary>Creates ITranslator object.</summary>
        ///<param name="trasnlatorName" type="string">Name of desired translator type.</param>
        ///<returns type="ITranslator">Concreate translator object hidden behind ITranslator interface.</returns>
        ITranslator MakeTranslator(string translatorName);
    }
}
