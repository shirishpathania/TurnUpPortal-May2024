using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnUpPortal_May2024.Utils;

namespace TurnUpPortal_May2024.Pages
{
    internal class LoginPage
    {
        // WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));

        public void loginPage(IWebDriver driver)
        {
            // Exception Handling
            try
            {
                // driver.FindElement(By.Id("UserName")).SendKeys("hari");
                IWebElement userName = driver.FindElement(By.Id("UserName"));

                // driver.FindElement(By.Name("Password")).SendKeys("123123");
                IWebElement password = driver.FindElement(By.Name("Password"));

                // driver.FindElement(By.XPath("//*[@id=\"loginForm\"]/form/div[3]/input[1]")).Click();
                IWebElement loginButton = driver.FindElement(By.XPath("//*[@id=\"loginForm\"]/form/div[3]/input[1]"));

                WaitHelper.WaitToBeClickable(driver, "Id", "UserName", 10);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("UserName")));
                userName.SendKeys("hari");
                WaitHelper.WaitToBeVisible(driver, "Name", "Password", 20);
                password.SendKeys("123123");
                loginButton.Click();
            }
            catch (Exception ex)
            {
                Assert.Fail("TurnUp Portal did not load successfully");

            }
        }
    }
}
