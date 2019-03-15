namespace YandexMarket.Autotests.Electronics
{
    using System.Collections.Generic;

    public sealed class ScenarioSettings
    {
        public string NeedSubsection { get; set; }

        public int ElementsCount { get; set; }

        public int PriceFromValue { get; set; }

        public IEnumerable<string> Manufacturers { get; set; }
    }
}
