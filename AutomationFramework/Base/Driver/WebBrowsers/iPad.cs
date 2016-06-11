﻿using AutomationFrameWork.Driver.Interface;
using OpenQA.Selenium.Chrome;
using System;
using OpenQA.Selenium;
namespace AutomationFrameWork.Driver
{
    class iPad : IDrivers<IWebDriver, ChromeDriverService, ChromeOptions>
    {

        public ChromeOptions DesiredCapabilities
        {
            get
            {
                ChromeOptions options = new ChromeOptions();
                options.LeaveBrowserRunning = true;
                options.AddArgument("--user-agent=Mozilla/5.0 (iPad; CPU OS 9_1 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/9.0 Mobile/10A5355d Safari/8536.25");
                return options;
            }
        }
        public ChromeDriverService DriverServices
        {
            get
            {
                ChromeDriverService serivces = ChromeDriverService.CreateDefaultService();
                serivces.EnableVerboseLogging = false;
                serivces.HideCommandPromptWindow = true;
                serivces.SuppressInitialDiagnosticInformation = false;
                return serivces;
            }
        }

        public IWebDriver Drivers(ChromeDriverService driverServices = null, ChromeOptions desiredCapabilities = null, int commandTimeOut = 60)
        {
            driverServices = driverServices ?? DriverServices;
            desiredCapabilities = desiredCapabilities ?? DesiredCapabilities;
            var options = desiredCapabilities;
            options.AddArgument("--user-agent=Mozilla/5.0 (iPad; CPU OS 9_1 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/9.0 Mobile/10A5355d Safari/8536.25");
            return new ChromeDriver(driverServices, options, TimeSpan.FromSeconds(commandTimeOut));
        }
    }
}
