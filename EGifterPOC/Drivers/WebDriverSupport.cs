using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace EGifterPOC.Drivers
{
    [Binding]
    public class WebDriverSupport
    {
        private readonly IObjectContainer _objectContainer;
        private readonly Configuration _configuration;

        public WebDriverSupport(IObjectContainer objectContainer, Configuration configuration)
        {
            _objectContainer = objectContainer;
            _configuration = configuration;
        }

        [BeforeScenario]
        public void InitializeWebDriver()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.BinaryLocation = _configuration.ChromeExecutablePath;
            chromeOptions.AddArgument("no-sandbox");
            chromeOptions.AddArgument("start-maximized");

            var webDriver = new ChromeDriver(
                ChromeDriverService.CreateDefaultService(_configuration.ChromeDriverExecutablePath),
                chromeOptions);
            _objectContainer.RegisterInstanceAs<RemoteWebDriver>(webDriver);
        }

        [AfterScenario]
        public void DisposeWebDriver()
        {
            var webDriver = _objectContainer.Resolve<RemoteWebDriver>();
            webDriver.Quit();
        }
    }
}
