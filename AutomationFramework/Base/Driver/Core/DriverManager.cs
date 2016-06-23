﻿using AutomationFrameWork.Exceptions;
using OpenQA.Selenium;
using System.Threading;
using System;
using System.Reflection;
using System.Linq;

namespace AutomationFrameWork.Driver
{
    public static class DriverManager<DriverType>
    {
        private static ThreadLocal<object> _driverStored = new ThreadLocal<object>();
        /// <summary>
        /// This method return driver base on generic TypeofDriver
        /// Ex: DriverManger<IWebDriver> will return IWebDriver
        /// DriverManager<PhantomJSDriver> will return PhantomJSDriver
        /// </summary>
        public static DriverType Driver
        {
            get
            { return (DriverType)DriverStored; }
        }
        /// <summary>
        /// This is use for stored driver 
        /// when running in paralell in single machine
        /// </summary>
        private static object DriverStored
        {
            get
            {
                if (_driverStored == null || _driverStored.Value == null)
                    throw new StepErrorException("Please call method 'StartDriver' before can get Driver");
                return _driverStored.Value;
            }
            set
            {
                _driverStored.Value = value;
            }
        }
        /// <summary>
        /// This method is use for instance driver
        /// </summary>
        /// <param name="factoryType"></param>
        /// <param name="type"></param>
        /// <param name="driverServices"></param>
        /// <param name="desiredCapabilities"></param>
        /// <param name="commandTimeOut"></param>
        /// <param name="pageLoadTimeout"></param>
        /// <param name="scriptTimeout"></param>
        /// <param name="isMaximize"></param>
        public static void StartDriver(FactoryType factoryType, Browser type, object driverServices = null, object desiredCapabilities = null, int commandTimeOut = 60, int pageLoadTimeout = 60, int scriptTimeout = 60, bool isMaximize = false)
        {
            Type foundClass = Assembly.GetExecutingAssembly().GetTypes()
                         .Where(item => item.Namespace == Constants.FACTORY_NAME_SPACE && item.Name.Equals(factoryType.ToString(), StringComparison.OrdinalIgnoreCase))
                         .FirstOrDefault();

            if (foundClass != null)
            {
                Object[] args = { type, driverServices, desiredCapabilities, commandTimeOut, pageLoadTimeout, scriptTimeout, isMaximize };
                object instance = Activator.CreateInstance(foundClass, args);
                Type classType = instance.GetType();
                MethodInfo method = classType.GetMethod("GetDriver", BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance);
                DriverStored = method.Invoke(instance, null);
            }
            else
                throw new NotImplementedException("Factory for " + factoryType + " is not implemented");
        }
        /// <summary>
        /// This method is use for close and destroy driver
        /// </summary>
        public static void CloseDriver()
        {
            IWebDriver driver = (IWebDriver)DriverStored;
            driver.Quit();
            driver.Dispose();
            if (DriverStored != null)
                DriverStored = null;
        }
    }
}
