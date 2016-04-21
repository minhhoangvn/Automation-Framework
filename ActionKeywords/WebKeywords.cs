﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AutomationFrameWork.Driver;
using OpenQA.Selenium.Internal;

namespace AutomationFrameWork.ActionsKeys
{
    public class WebKeywords
    {
        private static readonly WebKeywords instance = new WebKeywords();
        private static IWebDriver WebDriver = null;
        private WebKeywords()
        {
            //This method for not allow use can instance this class from outside
        }
        public static WebKeywords Instance
        {
            get
            {
                WebDriver = DriverFactory.Instance.GetWebDriver;
                return instance;
            }
        }
        /// <summary>
        /// This method is use for 
        /// navigate to URL
        /// User can use param with Uri Ex: /home/contact
        /// </summary>
        /// <param name="url"></param>
        public void Navigate(String url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }
        /// <summary>
        /// This method will naviagte to URL 
        /// It require param with exactly in URL format 
        /// Ex: https://github.com/minhhoangvn http://github.com/minhhoangvn
        /// </summary>
        /// <param name="url"></param>
        public void OpenUrl(String url)
        {
            WebDriver.Url = url;
        }
        /// <summary>
        /// This method is use for
        /// select option from dropdown list or combobox
        /// </summary>
        /// <param name="element"></param>
        /// <param name="type"></param>
        /// <param name="options"></param>
        public void Select(IWebElement element, SelectType type, string options)
        {
            SelectElement select = new SelectElement(element);
            switch (type)
            {
                case SelectType.SelectByIndex:
                    try
                    {
                        select.SelectByIndex(Int32.Parse(options));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.GetBaseException().ToString());
                        throw new ArgumentException("Please input numberic on selectOption for SelectType.SelectByIndex");
                    }
                    break;
                case SelectType.SelectByText:
                    select.SelectByText(options);
                    break;
                case SelectType.SelectByValue:
                    select.SelectByValue(options);
                    break;
                default:
                    throw new Exception("Get error in using Selected");
            }
        }
        /// <summary>
        /// This method use for 
        /// click 
        /// </summary>
        /// <param name="element"></param>
        public void Click(IWebElement element)
        {
            element.Click();
        }
        /// <summary>
        /// This method user for 
        /// enter text 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        public void SetText(IWebElement element, string text)
        {
            element.SendKeys(text);
        }
        /// <summary>
        /// This method use for 
        /// wait element ready to click 
        /// </summary>
        /// <param name="locatorValue"></param>
        /// <param name="timeOut"></param>
        public void WaitElementToBeClickable(By locatorValue, int timeOut)
        {
            WebDriverWait wait = new WebDriverWait(DriverFactory.Instance.GetWebDriver, TimeSpan.FromSeconds(timeOut));
            wait.Until(ExpectedConditions.ElementToBeClickable(locatorValue));
        }
        /// <summary>
        /// This method use for 
        /// wait element visible on DOM
        /// </summary>
        /// <param name="locatorValue"></param>
        /// <param name="timeOut"></param>
        public void WaitElementVisible(By locatorValue, int timeOut)
        {
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeOut));
            wait.Until(ExpectedConditions.ElementIsVisible(locatorValue));
        }
        /// <summary>
        /// This method use for 
        /// wait title of page contain string user want
        /// </summary>
        /// <param name="title"></param>
        /// <param name="timeOut"></param>
        public void WaitTitleContains(string title, int timeOut)
        {
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeOut));
            wait.Until(ExpectedConditions.TitleContains(title));
        }
        /// <summary>
        /// This method use for 
        /// get attribute of element in DOM
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public string GetAttribute(IWebElement element, string attribute)
        {
            return element.GetAttribute(attribute);
        }
        /// <summary>
        /// This method use for get title of page
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
        {
            return DriverFactory.Instance.GetWebDriver.Title;
        }
        /// <summary>
        /// This method use for 
        /// wait page load completed
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="time"></param>
        public void WaitForPageToLoad(int time)
        {
            TimeSpan timeout = new TimeSpan(0, 0, time);
            WebDriverWait wait = new WebDriverWait(WebDriver, timeout);
            IJavaScriptExecutor javascript = WebDriver as IJavaScriptExecutor;
            if (javascript == null)
                throw new ArgumentException("driver", "Driver must support javascript execution");
            wait.Until((d) =>
            {
                try
                {
                    return javascript.ExecuteScript("return document.readyState").Equals("complete");
                }
                catch (InvalidOperationException e)
                {
                    //Window is no longer available
                    return e.Message.ToLower().Contains("unable to get browser");
                }
                catch (WebDriverException e)
                {
                    //Browser is no longer available
                    return e.Message.ToLower().Contains("unable to connect");
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
        /// <summary>
        /// This method use for
        /// set attribute of element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        public void SetAttribute(IWebElement element, string attributeName, string value)
        {
            IWrapsDriver wrappedElement = element as IWrapsDriver;
            if (wrappedElement == null)
                throw new ArgumentException("element", "Element must wrap a web driver");

            IWebDriver driver = wrappedElement.WrappedDriver;
            IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;
            if (javascript == null)
                throw new ArgumentException("element", "Element must wrap a web driver that supports javascript execution");
            javascript.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])", element, attributeName, value);
        }
        /// <summary>
        /// This method use for 
        /// clear any text on text field
        /// </summary>
        /// <param name="element"></param>
        public void ClearText(IWebElement element)
        {
            element.Clear();
        }
        /// <summary>
        /// This method is use for
        /// Execute javascript
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public IJavaScriptExecutor JavaScript(IWebDriver driver)
        {
            return (IJavaScriptExecutor)driver;
        }
        /// <summary>
        /// This method is use for
        /// return value of css
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetCssValue(IWebElement element, string value)
        {
            return element.GetCssValue(value);
        }
        /// <summary>
        /// This method is use for
        /// return element
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IWebElement FindElement(string value)
        {
            IWebElement element=null;
            string LocatorType = value.Split(';')[0];
            string LocatorValue = value.Split(';')[1];
            switch (LocatorType.ToLower())
            {
                case "id":
                    element = WebDriver.FindElement(By.Id(LocatorValue));
                    break;
                case "name":
                    element = WebDriver.FindElement(By.Name(LocatorValue));
                    break;
                case "xpath":
                    element = WebDriver.FindElement(By.XPath(LocatorValue));
                    break;
                case "tag":
                    element = WebDriver.FindElement(By.TagName(LocatorValue));
                    break;
                case "link":
                    element = WebDriver.FindElement(By.LinkText(LocatorValue));
                    break;
                case "css":
                    element = WebDriver.FindElement(By.CssSelector(LocatorValue));
                    break;
                case "class":
                    element = WebDriver.FindElement(By.ClassName(LocatorValue));
                    break;
                default:
                    throw new ArgumentException("Support FindElement with 'id' 'name' 'xpath' 'tag' 'link' 'css' 'class'");
            }
            return element;
        }
    }
}
/// <summary>
/// This is enum for select option in keywords select
/// </summary>
public enum SelectType
{
    SelectByIndex,
    SelectByText,
    SelectByValue,
}
