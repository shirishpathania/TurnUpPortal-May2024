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

namespace TurnUpPortal_May2024.Tests
{
    internal class TM_Tests
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void setUp()
        {
            // Prerequisites
            driver = new ChromeDriver();
            // Explicit Wait
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // Implicit Wait
            // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://horse.industryconnect.io/Account/Login?ReturnUrl=%2f");
            loginPage();
            homePage();
        }

        public void loginPage()
        {
            driver.FindElement(By.Id("UserName")).SendKeys("hari");
            driver.FindElement(By.Name("Password")).SendKeys("123123");
            driver.FindElement(By.XPath("//*[@id=\"loginForm\"]/form/div[3]/input[1]")).Click();
        }

        public void homePage()
        {
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/ul/li[5]/a")).Click();
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/ul/li[5]/ul/li[3]/a")).Click();
        }

        [TearDown]
        public void tearDown()
        {
            driver.Quit();
        }

        [Test, Order(1)]
        public void createTMRecordTest()
        {
            // Actions
            driver.FindElement(By.XPath("//*[@id=\"container\"]/p/a")).Click();
            driver.FindElement(By.XPath("//*[@id=\"TimeMaterialEditForm\"]/div/div[1]/div/span[1]/span/span[2]/span")).Click();
            driver.FindElement(By.XPath("//*[@id=\"TypeCode_listbox\"]/li[2]")).Click();
            driver.FindElement(By.Id("Code")).SendKeys("MAY2024");
            driver.FindElement(By.Id("Description")).SendKeys("Test Analyst");
            driver.FindElement(By.XPath("//*[@id=\"TimeMaterialEditForm\"]/div/div[4]/div/span[1]/span/input[1]")).Click();
            driver.FindElement(By.Id("Price")).SendKeys("100");
            driver.FindElement(By.Id("SaveButton")).Click();

            // Assertion
            // Thread.Sleep(3000);
            // Explicit Wait
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"tmsGrid\"]/div[4]/a[4]/span")));
            driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[4]/a[4]/span")).Click();

            // Get Time and Material Record page count
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"tmsGrid\"]/div[4]/ul/li[3]/span")));
            int tmRecordPageCount = Int32.Parse(driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[4]/ul/li[3]/span")).Text);
            
            // Go Back to first Page
            driver.FindElement(By.XPath("//span[@class='k-icon k-i-seek-w']")).Click();
            Thread.Sleep(3000);

            for (int i = 1; i <= tmRecordPageCount; i++)
            {
                // Get Time and Material Records per page count
                int tmRecordsPerPageCount = Int32.Parse(driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[4]/span[1]/span/span/span[1]")).Text);
                
                for (int j = 1; j <= tmRecordsPerPageCount; j++)
                {
                    String code = driver.FindElement(By.XPath("//tr["+j+"]/td[1]")).Text;
                    String typeCode = driver.FindElement(By.XPath("//tr["+j+"]/td[2]")).Text;
                    String description = driver.FindElement(By.XPath("//tr[" + j + "]/td[3]")).Text;
                    String price = driver.FindElement(By.XPath("//tr[" + j + "]/td[4]")).Text;

                    if (code == "MAY2024")
                    {
                        Assert.That(code == "MAY2024", "Code value does not match");
                        Assert.That(typeCode == "T", "TypeCode value does not match");
                        Assert.That(description == "Test Analyst", "Description value does not match");
                        Assert.That(price.Contains("100"), "Price value does not match");
                        i = tmRecordPageCount;
                        break;
                    }
                }
                // Click on next page
                driver.FindElement(By.XPath("//span[@class='k-icon k-i-arrow-e']")).Click();
            }
        }

        [Test, Order(2)]
        public void editTMRecordTest() {
            driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[4]/a[4]/span")).Click();
            driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[3]/table/tbody/tr[3]/td[5]/a[1]")).Click();
            driver.FindElement(By.Id("Code")).SendKeys("JUNE2024");
            driver.FindElement(By.Id("Description")).SendKeys("Business Analyst");
            driver.FindElement(By.XPath("//*[@id=\"TimeMaterialEditForm\"]/div/div[4]/div/span[1]/span/input[1]")).Click();
            driver.FindElement(By.Id("Price")).SendKeys("200");
            driver.FindElement(By.Id("SaveButton")).Click();

            // Assertion
            // Thread.Sleep(3000);
            driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[4]/a[4]/span")).Click();
            String code = driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[3]/table/tbody/tr[3]/td[1]")).Text;
            String typeCode = driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[3]/table/tbody/tr[3]/td[2]")).Text;
            String description = driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[3]/table/tbody/tr[3]/td[3]")).Text;
            String price = driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[3]/table/tbody/tr[3]/td[4]")).Text;

            Assert.That(code == "JUNE2024", "Code value does not match");
            Assert.That(typeCode == "T", "TypeCode value does not match");
            Assert.That(description == "Business Analyst", "Description value does not match");
            Assert.That(price.Contains("200"), "Price value does not match");
        }

        [Test, Order(3)]
        public void deleteTMRecordTest() {
            driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[4]/a[4]/span")).Click();
            driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[3]/table/tbody/tr[4]/td[5]/a[2]")).Click();
            driver.SwitchTo().Alert().Accept();

            // Assertion
            // Thread.Sleep(3000);
            driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[4]/a[4]/span")).Click();
            String code = driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[3]/table/tbody/tr[3]/td[1]")).Text;
            String typeCode = driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[3]/table/tbody/tr[3]/td[2]")).Text;
            String description = driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[3]/table/tbody/tr[3]/td[3]")).Text;
            String price = driver.FindElement(By.XPath("//*[@id=\"tmsGrid\"]/div[3]/table/tbody/tr[3]/td[4]")).Text;

            Assert.That(code != "JUNE2024", "Code value does not match");
            Assert.That(typeCode != "T", "TypeCode value does not match");
            Assert.That(description != "Business Analyst", "Description value does not match");
            Assert.That(!price.Contains("200"), "Price value does not match");
        }
    }
}
