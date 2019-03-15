namespace YandexMarket.Autotests.Electronics
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using YandexMarket.Autotests.Common;

    //TODO: Реализовать зависимость тест кейсов (с помощью флагов)
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
            // Переходим в яндекс маркет
            Driver.Navigate().GoToUrl(TestSettings.YandexURL);

            var market = WebElementsHandling.GetWebElementByCssSelector(
                Driver,
                TestSettings.YandexMarketCssSelector,
                TimeSpan.FromSeconds(30));

            market.Click();
        }

        [SetUp]
        public void SetUp()
        {
            // Ожидание всмлывающего окна запроса места расположения 
            if (setRegion)
            {
                return;
            }

            try
            {
                var popupElement = WebElementsHandling.GetWebElementByCssSelector(
                    Driver,
                    ".popup2",
                    TimeSpan.FromSeconds(5));

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
            // Переходим по маркету в нужный раздел
            var links = Driver.FindElements(By.CssSelector(TestSettings.SectionsMenuCssSelector));

            var electronics = links.Single(l => l.Text.Equals(TestSettings.NeedSection));

            TryToOpenSection(electronics, 3);
        }

        [Test]
        public void Test_002_SetFilters()
        {
            // Не получилось выбрать "Показывать по 12", оставил некоторые попытки
            try
            {
                Driver.FindElement(By.CssSelector(TestSettings.ElementsCountButtonCssSelector)).Click();

                /*
                Actions.SendKeys(Keys.Up);
                Actions.SendKeys(Keys.Enter);
                */

                var buttons = Driver.FindElement(By.XPath($"//span[text()='Показывать по {TestSettings.ScenarioSettings.ElementsCount}']/parent::*"));
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

            // Меняем стоимость от
            var priceFrom = WebElementsHandling.GetWebElementById(Driver, TestSettings.PriceFromId, TimeSpan.FromSeconds(10));

            priceFrom.SendKeys(TestSettings.ScenarioSettings.PriceFromValue + Keys.Enter);

            // Устанавливаем нужные чекбоксы в true
            foreach (var manufacturer in TestSettings.ScenarioSettings.Manufacturers)
            {
                var checkbox = WebElementsHandling.GetWebElementByXPath(Driver, TestSettings.GetCheckBoxxPathByName(manufacturer), TimeSpan.FromSeconds(10));
                checkbox.Click();
            }

            // Успеваем получить элементы до того как чекбокс применился
            Thread.Sleep(TimeSpan.FromSeconds(2));

            // Проверка колличества элементов
            var currentCount = GetElementsCount(3, TimeSpan.FromSeconds(2));

            Assert.That(currentCount == TestSettings.ScenarioSettings.ElementsCount, $"Error: expected elements count {TestSettings.ScenarioSettings.ElementsCount}, but now {currentCount}");
        }

        [Test]
        public void Test_003_SearchFirstElement()
        {
            // Вытаскиваем первый элемент и ищем его
            var element = Driver.FindElements(By.XPath(TestSettings.TitleLinkElementsxPath)).First();
            var text = element.Text;

            var search = Driver.FindElement(By.Id(TestSettings.SearchId));
            search.SendKeys(text + Keys.Enter);

            IWebElement result;

            // Хотел объеденить кейсы, но в итоге в конце расхождение :(
            try
            {
                result = WebElementsHandling.GetWebElementByCssSelector(
                    Driver,
                    TestSettings.SearchResultHeaderCssSelector,
                    TimeSpan.FromSeconds(30));
            }
            catch
            {
                result = Driver.FindElements(By.XPath(TestSettings.TitleLinkElementsxPath)).First();
            }

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
                        TestSettings.ScenarioSettings.NeedSubsection,
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

        private int GetElementsCount(int retryCount, TimeSpan delay)
        {
            int count = 0;

            for (int i = 1; i <= retryCount; i++)
            {
                var elements = Driver.FindElements(By.XPath(TestSettings.ElementListxPath));

                count = elements.Count;

                if (elements.Count > TestSettings.ScenarioSettings.ElementsCount)
                {
                    Thread.Sleep(delay);
                }
                else
                {
                    break;
                }
            }

            return count;
        }

    }
}
