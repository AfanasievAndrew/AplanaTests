namespace YandexMarket.Autotests.Electronics
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using YandexMarket.Autotests.Common;

    [TestFixture]
    public class Electronics : TestFixture<TestSettingsElectronics>
    {
        private bool setRegion = false;

        public Electronics()
        {
            Driver.Manage().Window.Maximize();

            Actions = new Actions(Driver);
        }

        Actions Actions { get; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Driver.Navigate().GoToUrl(TestSettings.YandexURL);

            var market = WebElementsHandling.GetWebElementByCssSelector(
                Driver,
                TestSettings.YandexMarketCssSelector,
                TimeSpan.FromMinutes(1));

            market.Click();
        }

        [SetUp]
        public void SetUp()
        {
            if (setRegion)
            {
                return;
            }

            try
            {
                var popupElement = WebElementsHandling.GetWebElementByCssSelector(
                    Driver,
                    ".popup2",
                    TimeSpan.FromSeconds(2));

                var buttons = Driver.FindElements(By.CssSelector(".popup2 .button2"));

                buttons.Single(b => b.Text.Contains("Да")).Click();

                setRegion = true;
            }
            catch (Exception ex)
            {
                //Logger.Warning();
            }
        }

        [Test]
        public void Test_001_GoToSection()
        {
            var links = Driver.FindElements(By.CssSelector(TestSettings.SectionsMenuCssSelector));

            var electronics = links.Single(l => l.Text.Equals(TestSettings.NeedSection));

            TryToOpenSection(electronics, 3);
        }

        [Test]
        public void Test_002_SetFilters()
        {
            try
            {
                Driver.FindElement(By.CssSelector(TestSettings.ElementsCountButtonCssSelector)).Click();

                /*
                Actions.SendKeys(Keys.Up);
                Actions.SendKeys(Keys.Enter);
                */

                var buttons = Driver.FindElement(By.XPath($"//span[text()='Показывать по {TestSettings.ElementsCount}']/parent::*"));
                buttons.Click();

                /*
                Actions.MoveToElement(buttons).Build().Perform();
                Actions.Click();
                */

                /*
                 var hiddenWebElement = WebElementsHandling.GetWebElementByXPath(Driver, "//select[@class='select__control']/option[@value='12']", TimeSpan.FromMinutes(1));
                 hiddenWebElement.SendKeys(Keys.Enter);
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click()", hiddenWebElement);*/

                /*
                Driver.FindElement(By.CssSelector(".n-pager button")).Click();
                Driver.FindElement(By.XPath("//div[contains(@class,n-pager)]/span[contains(@class,'select')]/button/span[1]")).Click();*/
            }
            catch
            {
                //Logger.Warning();
            }

            var priceFrom = WebElementsHandling.GetWebElementById(Driver, TestSettings.PriceFromId, TimeSpan.FromMinutes(1));

            priceFrom.SendKeys(TestSettings.PriceFromValue + Keys.Enter);

            var checkboxLG = WebElementsHandling.GetWebElementByXPath(Driver, TestSettings.GetCheckBoxxPathByName("LG"), TimeSpan.FromSeconds(10));
            checkboxLG.Click();

            var checkboxSumsung = WebElementsHandling.GetWebElementByXPath(Driver, TestSettings.GetCheckBoxxPathByName("Samsung"), TimeSpan.FromSeconds(20));
            checkboxSumsung.Click();

            var elements = Driver.FindElements(By.XPath(TestSettings.ElementListxPath));
            
            Assert.That(elements.Count == TestSettings.ElementsCount, $"Error: expected elements count {TestSettings.ElementsCount}, but now {elements.Count}");
        }

        [Test]
        public void Test_003_SearchFirstElement()
        {
            var element = Driver.FindElements(By.CssSelector(TestSettings.TitleLinkElementsCssSelector)).First();
            var text = element.Text;

            var search = Driver.FindElement(By.Id(TestSettings.SearchId));
            search.SendKeys(text + Keys.Enter);

            var result = WebElementsHandling.GetWebElementByCssSelector(
                Driver,
                TestSettings.SearchResultHeaderCssSelector,
                TimeSpan.FromMinutes(1));

            Assert.That(result.Text.Equals(text), $"Wrong text in result search title: expected '{text}', but now '{result.Text}'");
        }

        private void TryToOpenSection( IWebElement electronics, int retryCount )
        {
            for (int i = 1; i <= retryCount; i++)
            {
                try
                {
                    Actions.MoveToElement(electronics).Build().Perform();

                    WebElementsHandling.GetWebElementByLinkText(
                        Driver,
                        TestSettings.NeedSubsection,
                        TimeSpan.FromSeconds(10)).Click();

                    break;
                }
                catch
                {
                    if (i == retryCount)
                    {
                        throw;
                    }
                    else
                    {
                        //Logger.Warning();
                    }
                }
            }
        }
    }
}
