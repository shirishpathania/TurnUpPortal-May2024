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
using TurnUpPortal_May2024.Pages;
using TurnUpPortal_May2024.Utils;

namespace TurnUpPortal_May2024.Tests
{
    internal class TM_Tests : CommonDriver
    {
             
        LoginPage loginPageObj = new LoginPage();
        HomePage homePageObj = new HomePage();
        TMPage tmPageObj = new TMPage();

        [SetUp]
        public void setUp()
        {
            // Prerequisites
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://horse.industryconnect.io/Account/Login?ReturnUrl=%2f");
            // loginPage();
            loginPageObj.loginPage(driver);
            // homePage();
            homePageObj.homePage(driver);
        }

        [TearDown]
        public void tearDown()
        {
            driver.Quit();
        }

        [Test, Order(1)]
        public void createTMRecordTest()
        {
            tmPageObj.createTMRecord(driver);
            
        }

        [Test, Order(2)]
        public void editTMRecordTest() {
            tmPageObj.editTMRecord(driver);
        }

        [Test, Order(3)]
        public void deleteTMRecordTest() {
            tmPageObj.deleteTMRecord(driver);
        }
    }
}
