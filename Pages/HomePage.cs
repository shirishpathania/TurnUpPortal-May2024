using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnUpPortal_May2024.Pages
{
    internal class HomePage
    {
        public void homePage(IWebDriver driver)
        {
            IWebElement administrationMenu = driver.FindElement(By.XPath("/html/body/div[3]/div/div/ul/li[5]/a"));
            administrationMenu.Click();
            IWebElement timeAndMaterialsMenu = driver.FindElement(By.XPath("/html/body/div[3]/div/div/ul/li[5]/ul/li[3]/a"));
            timeAndMaterialsMenu.Click();
        }

    }
}
