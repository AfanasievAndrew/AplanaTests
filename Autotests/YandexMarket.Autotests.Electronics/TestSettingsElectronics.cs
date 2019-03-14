namespace YandexMarket.Autotests.Electronics
{
    using YandexMarket.Autotests.Common;

    public sealed class TestSettingsElectronics : TestSettings
    {
        public string YandexURL { get; } = "https://yandex.ru/";

        public string YandexMarketCssSelector { get; } = "[data-id='market']";

        public string SectionsMenuCssSelector { get; } = ".n-w-tabs__horizontal-tabs-container a";

        public string NeedSection { get; } = "Электроника";

        //public string NeedSubsection { get; } = "Телевизоры";

        public string ElementsCountButtonCssSelector { get; } = ".n-pager button";

        //public int ElementsCount { get; } = 48; 

        public string PriceFromId { get; } = "glpricefrom";

        //public int PriceFromValue { get; } = 20000;

        public string ElementListxPath { get; } = "//div[contains(@class,'n-snippet-list')]/div[contains(@class,'n-snippet-card2')]";

        public string TitleLinkElementsCssSelector { get; } = ".n-snippet-card2__title a";

        public string SearchId { get; } = "header-search";

        public string SearchResultHeaderCssSelector { get; } = ".n-product-summary__headline h1";

        public string GetCheckBoxxPathByName( string name ) => $"//input[@type='checkbox'][contains(@name,'{name}')]/../div";
    }
}


