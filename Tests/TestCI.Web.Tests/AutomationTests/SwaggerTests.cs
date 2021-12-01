using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Sanakan.Web.Tests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestCI.Web.Tests.AutomationTests
{
#if DEBUG
    [TestClass]
#endif
    public class SwaggerTests
    {
        private static HttpClient _client;
        private static ChromeDriver _driver;
        private static SHA1Managed _sha;
        private static readonly uint[] _lookup32 = CreateLookup32();

        static SwaggerTests()
        {
            _sha = new SHA1Managed();
        }

        private static uint[] CreateLookup32()
        {
            var result = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                string s = i.ToString("X2");
                result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
            }
            return result;
        }

        private static string ByteArrayToHexViaLookup32(byte[] bytes)
        {
            var lookup32 = _lookup32;
            var result = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                var val = lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[2 * i + 1] = (char)(val >> 16);
            }
            return new string(result);
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var factory = new TestWebApplicationFactory();
            _client = factory.CreateClient();
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            chromeOptions.AddArguments("start-maximized");
            _driver = new ChromeDriver(chromeOptions);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _client.Dispose();
            _driver.Quit();
        }

        [TestMethod]
        public async Task Should_Display_Swagger_Page()
        {
            _driver.Url = _client.BaseAddress.ToString();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            wait.Until(dr => ((IJavaScriptExecutor)dr).ExecuteScript("return document.readyState").Equals("complete"));
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            var fileName = @"screenshot.jpg";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Jpeg);
            string actualHash = await GetFileHashAsync(fileName);
            string expectedHash = await GetFileHashAsync("AutomationTests/expectedScreenshot.jpg");
            actualHash.Should().Be(expectedHash);
        }

        private static async Task<string> GetFileHashAsync(string filename)
        {
            var stream = File.OpenRead(filename);
            var hashedBytes = await _sha.ComputeHashAsync(stream);
            return ByteArrayToHexViaLookup32(hashedBytes);
            
        }
    }
}
