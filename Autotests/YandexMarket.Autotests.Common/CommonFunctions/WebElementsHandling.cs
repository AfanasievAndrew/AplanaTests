namespace YandexMarket.Autotests.Common.CommonFunctions
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using SeleniumExtras.WaitHelpers;

    public class WebElementsHandling
    {
        public IWebElement GetWebElementByCssSelector(IWebDriver driver, string cssSelector, TimeSpan timeOut)
        {
            IWebElement element = null;

            try
            {
                WebDriverWait driverWait = new WebDriverWait(driver, timeOut);

                element = driverWait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
                        By.CssSelector(cssSelector)));
            }
            catch(Exception ex)
            {
                throw new Exception($"Cannot get IWebElement, exception: {ex.ToString()}");
            }

            return element;
        }

        public IWebElement GetWebElementByXPath(IWebDriver driver, string xPath, TimeSpan timeOut)
        {
            IWebElement element = null;

            try
            {
                WebDriverWait driverWait = new WebDriverWait(driver, timeOut);

                element = driverWait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
                        By.XPath(xPath)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot get IWebElement, exception: {ex.ToString()}");
            }

            return element;
        }

        public IWebElement GetWebElementByLinkText(IWebDriver driver, string linkText, TimeSpan timeOut)
        {
            IWebElement element = null;

            try
            {
                WebDriverWait driverWait = new WebDriverWait(driver, timeOut);

                element = driverWait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
                        By.LinkText(linkText)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot get IWebElement, exception: {ex.ToString()}");
            }

            return element;
        }

        public IWebElement GetWebElementById(IWebDriver driver, string id, TimeSpan timeOut)
        {
            IWebElement element = null;

            try
            {
                WebDriverWait driverWait = new WebDriverWait(driver, timeOut);

                element = driverWait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
                        By.Id(id)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot get IWebElement, exception: {ex.ToString()}");
            }

            return element;
        }
    }
}
