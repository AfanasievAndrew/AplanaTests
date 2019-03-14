namespace YandexMarket.Autotests.Electronics
{
    using System.Collections.Generic;

    class ScenarioSettings
    {
        public string NeedSubsection { get; } = "Телевизоры";

        public int ElementsCount { get; } = 48;

        public int PriceFromValue { get; } = 20000;

        IEnumerable<string> Manufacturer { get; } = new []{ "LG", "Samsung" };
    }
}
