namespace YandexMarket.Autotests.Common
{
    using System;
    using System.IO;

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
        }

        public string ChromeDriverDir { get; } = @"D:\Drivers\";
    }
}
