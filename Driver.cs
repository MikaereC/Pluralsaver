﻿using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Pluralsaver
{
    public class Driver
    {
        public static IWebDriver Instance { get; set; }

        public static void Initialize()
        {
            Instance = new ChromeDriver();
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        }

        public static void Close()
        {
            Console.WriteLine("Shutting down the browser...");
            Instance.Close();
            Instance.Dispose();
        }

        public static void WaitUntilVisible(By by)
        {
            var wait = new WebDriverWait(Instance, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(by));
        }

        public static void WaitUntilHidden(By by)
        {
            var wait = new WebDriverWait(Instance, TimeSpan.FromSeconds(10));
            wait.Until(driver => !Instance.FindElement(by).Displayed);
        }

        public static void WaitSeconds(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }
    }
}
