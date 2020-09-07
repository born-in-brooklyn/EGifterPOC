using System;
using System.IO;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace EGifterPOC.Drivers
{
    [Binding]
    public class Configuration
    {
        private Uri _homePageUri;
        public Uri HomePageUri
        {
            get
            {
                if (_homePageUri == null)
                {
                    var fromEnvironment = Environment.GetEnvironmentVariable("HOME_PAGE_URI");
                    if (string.IsNullOrWhiteSpace(fromEnvironment) || !Uri.TryCreate(fromEnvironment, UriKind.Absolute, out _homePageUri))
                    {
                        _homePageUri = new Uri("https://stage-v3.egifter.com/");
                    }
                }

                return _homePageUri;
            }
        }

        private Uri _corporateUri;
        public Uri CorporateUri
        {
            get
            {
                if (_corporateUri == null)
                {
                    var fromEnvironment = Environment.GetEnvironmentVariable("CORPORATE_URI");
                    if (string.IsNullOrWhiteSpace(fromEnvironment) || !Uri.TryCreate(fromEnvironment, UriKind.Absolute, out _corporateUri))
                    {
                        _corporateUri = new Uri("https://corporate.egifter.com/");
                    }
                }

                return _corporateUri;
            }
        }

        private TimeSpan? _defaultWaitForElement;
        public TimeSpan DefaultWaitForAssert
        {
            get
            {
                if (_defaultWaitForElement == null)
                {
                    _defaultWaitForElement = TimeSpan.FromSeconds(10);

                    var fromEnvironment = Environment.GetEnvironmentVariable("WAIT_FOR_ASSERT_SECONDS");
                    if (!string.IsNullOrWhiteSpace(fromEnvironment) && int.TryParse(fromEnvironment, out int defaultWaitForAssertSeconds))
                    {
                        _defaultWaitForElement = TimeSpan.FromSeconds(defaultWaitForAssertSeconds);
                    }
                }

                return _defaultWaitForElement.Value;
            }
        }

        private TimeSpan? _defaultPauseBetweenActAndAssert;
        public TimeSpan DefaultPauseBetweenActAndAssert
        {
            get
            {
                if (_defaultPauseBetweenActAndAssert == null)
                {
                    _defaultPauseBetweenActAndAssert = TimeSpan.FromMilliseconds(800);

                    var fromEnvironment = Environment.GetEnvironmentVariable("PAUSE_BETWEEN_ACT_ASSERT_MILLISECONDS");
                    if (!string.IsNullOrWhiteSpace(fromEnvironment) && int.TryParse(fromEnvironment, out int output))
                    {
                        _defaultPauseBetweenActAndAssert = TimeSpan.FromMilliseconds(output);
                    }
                }

                return _defaultPauseBetweenActAndAssert.Value;
            }
        }

        private string _chromeExecutablePath;
        public string ChromeExecutablePath
        {
            get
            {
                if (_chromeExecutablePath == null)
                {
                    var fromEnvironment = Environment.GetEnvironmentVariable("CHROME_EXECUTABLE_PATH");
                    if (string.IsNullOrWhiteSpace(fromEnvironment))
                    {
                        _chromeExecutablePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Browsers\Google\Chrome\Application\chrome.exe");
                    }
                }

                return _chromeExecutablePath;
            }
        }

        private string _chromeDriverExecutablePath;
        public string ChromeDriverExecutablePath
        {
            get
            {
                if (_chromeDriverExecutablePath == null)
                {
                    var fromEnvironment = Environment.GetEnvironmentVariable("CHROMEDRIVER_EXECUTABLE_PATH");
                    if (string.IsNullOrWhiteSpace(fromEnvironment))
                    {
                        _chromeDriverExecutablePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Selenium\ChromeDriver");
                    }
                }

                return _chromeDriverExecutablePath;
            }
        }
    }
}
