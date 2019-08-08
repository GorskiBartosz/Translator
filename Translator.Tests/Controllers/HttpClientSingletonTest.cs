using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Translator.Controllers;

namespace Translator.Tests.Controllers
{
    [TestClass]
    public class HttpClientSingletonTest
    {

        [TestMethod]
        public void Instance_ProgramTakesTwoInstances_ThoseAreTheSameInstance()
        {
            HttpClientSingleton client1 = HttpClientSingleton.Instance;
            HttpClientSingleton client2 = HttpClientSingleton.Instance;
            Assert.AreSame(client1, client2);
        }
    }
}
