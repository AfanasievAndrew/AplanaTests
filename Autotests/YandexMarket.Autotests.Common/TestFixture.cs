// TODO: Logging
// TODO: Add other drivers
namespace YandexMarket.Autotests.Common
{
    using System;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using YandexMarket.Autotests.Common.CommonFunctions;

    [TestFixture]
    public class TestFixture<T> where T : TestSettings, new()
    {
        public IWebDriver Driver { get; }

        public T TestSettings = new T();

        public WebElementsHandling WebElementsHandling = new WebElementsHandling();

        public TestFixture()
        {
            Driver = new ChromeDriver(TestSettings.ChromeDriverDir);
        }

       [OneTimeTearDown]
       public void OneTimeTearDown()
       {
            Driver.Close();
       }
    }
}
