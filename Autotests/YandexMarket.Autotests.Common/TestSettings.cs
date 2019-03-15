namespace YandexMarket.Autotests.Common
{
    using System;
    using System.IO;
    using System.Configuration;

    public class TestSettings
    {
        public TestSettings()
        {
            var chromeDrivePath = Path.Combine(ChromeDriverDir, "chromedriver.exe");

            if (!Directory.Exists(ChromeDriverDir))
            {
                throw new Exception($"Can not find {ChromeDriverDir}");
            }
            else if(!File.Exists(chromeDrivePath))
            {
                throw new Exception($"Can not find {chromeDrivePath}");
            }

            Scenario = ConfigurationManager.AppSettings["Scenario"];
        }

        public string Scenario { get; }

        public string ChromeDriverDir { get; } = @"D:\Drivers\";
    }
}
