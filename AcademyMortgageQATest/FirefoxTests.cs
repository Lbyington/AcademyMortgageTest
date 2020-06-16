using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace AcademyMortgageQATest
{
    class FirefoxTests
    {

        private IWebDriver driver;
        public string homeURL;
        WebDriverWait wait;
        string text;

        [SetUp]
        public void SetUpTest()
        {
            homeURL = "https://academymortgage.com/";
            driver = new FirefoxDriver("C:\\Windows\\System32");
            wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(15));

        }

        [Test(Description = "Check Homepage")]
        public void validatePageElements()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(homeURL);
            wait.Until(driver =>
                driver.FindElement(By.XPath("//span[contains(.,'News')]")).Displayed);
            driver.FindElement(By.XPath("//span[contains(.,'News')]")).Click();
            wait.Until(driver =>
                driver.FindElement(By.XPath("//span[contains(.,'All News')]")).Displayed);
            driver.FindElement(By.XPath("//span[contains(.,'All News')]")).Click();
            text = driver.FindElement(By.XPath("//*[contains(text(),'James Mac Pherson')]")).Text;
            Assert.IsTrue(text.Contains("James Mac Pherson"));
        }

        [Test(Description = "Test finding an agent")]
        public void searchForAgent()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(homeURL);
            wait.Until(driver =>
                driver.FindElement(By.XPath("//span[contains(.,'Find a Loan Officer')]")).Displayed);
            driver.FindElement(By.XPath("//span[contains(.,'Find a Loan Officer')]")).Click();
            driver.FindElement(By.XPath("//input[@id='Zip']")).SendKeys("84005");
            IWebElement radius = driver.FindElement(By.XPath("//select[@id='Radius']"));
            SelectElement select = new SelectElement(radius);
            select.SelectByValue("25");
            driver.FindElement(By.XPath("//div[@id='Main_C007_Col00']/div/div/div/div[3]/form/button")).Click();
            text = driver.FindElement(By.XPath("//*[contains(text(),'Matt Keyes')]")).Text;
            Assert.IsTrue(text.Contains("Matt Keyes"));
            driver.FindElement(By.XPath("//*[contains(text(),'Matt Keyes')]"));
            List<IWebElement> elements = driver.FindElements(By.ClassName("team-member")).ToList();
            bool exist = false;
            foreach (IWebElement element in elements)
            {
                if (element.FindElement(By.ClassName("name")).Text.Contains("Matt Keyes") &&
                        element.FindElement(By.ClassName("nmls-desktop")).Text.Contains("398768"))
                {
                    exist = true;
                    break;
                }
            }
            Assert.IsTrue(exist);
        }

        [TearDown]
        public void TearDownTest()
        {
            driver.Close();
        }

    }
}
