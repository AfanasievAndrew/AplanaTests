namespace YandexMarket.Autotests.Electronics
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using YandexMarket.Autotests.Common;
    using YandexMarket.Autotests.Electronics.Properties;

    public sealed class TestSettingsElectronics : TestSettings
    {
        public TestSettingsElectronics()
        {
            ScenarioSettings = LoadScenarioSettings();
        }

        public string YandexURL { get; } = "https://yandex.ru/";

        public string YandexMarketCssSelector { get; } = "[data-id='market']";

        public string SectionsMenuCssSelector { get; } = ".n-w-tabs__horizontal-tabs-container a";

        public string NeedSection { get; } = "Электроника";

        public string ElementsCountButtonCssSelector { get; } = ".n-pager button";

        public string PriceFromId { get; } = "glpricefrom";

        public string ElementListxPath { get; } = "//div[contains(@class,'n-snippet-list')]/child::div[not (contains(@class,'n-entrypoint-carousel'))]";

        public string TitleLinkElementsxPath { get; } = "//div[contains(@class,'n-snippet-list')]/child::*//div[contains(@class,'title')]/a"; //".n-snippet-card2__title a";

        public string SearchId { get; } = "header-search";

        public string SearchResultHeaderCssSelector { get; } = ".n-product-summary__headline h1";

        public ScenarioSettings ScenarioSettings { get; }

        public string GetCheckBoxxPathByName( string name ) => $"//input[@type='checkbox'][contains(@name,'{name}')]/../div";

        private ScenarioSettings LoadScenarioSettings()
        {
            ScenarioSettings scenarioSettings = Activator.CreateInstance<ScenarioSettings>();

            var jsonString = string.Empty;

            switch (Scenario)
            {
                case "Headphones":
                    {
                        jsonString = Resources.Headphones;
                        break;
                    }
                case "TV":
                    {
                        jsonString = Resources.TV;
                        break;
                    }
            }

            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(scenarioSettings.GetType());

                scenarioSettings = (ScenarioSettings)serializer.ReadObject(ms);
            }

            return scenarioSettings;
        }
    }
}


