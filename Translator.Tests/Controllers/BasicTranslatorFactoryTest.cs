using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Translator.Controllers;

namespace Translator.Tests.Controllers
{
    [TestClass]
    public class BasicTranslatorFactoryTest
    {
        [TestMethod]
        public void Instance_ProgramTakesTwoInstances_ThoseAreTheSameInstance()
        {
            ITranslatorFactory factory1 = BasicTranslatorFactory.Instance;
            ITranslatorFactory factory2 = BasicTranslatorFactory.Instance;
            Assert.AreSame(factory1, factory2);
        }

        [TestMethod]
        public void MakeTranslator_ProgramCreatesLeetSpeakTranslator_TranslatorIsCreated()
        {
            ITranslatorFactory factory = BasicTranslatorFactory.Instance;
            ITranslator translator = factory.MakeTranslator("leetspeak");
            Assert.IsTrue(translator is LeetTranslator);
        }

        [TestMethod]
        public void MakeTranslator_ProgramTriesToCreateUnexistingTranslator_ArgumentOutOfRangeExceptionIsThrown()
        {
            ITranslatorFactory factory = BasicTranslatorFactory.Instance;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                ITranslator translator = factory.MakeTranslator("unexisting_translator");
            });
        }



    }
}
