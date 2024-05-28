using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnUpPortal_May2024.Pages
{
    internal class TMPage
    {
        public void createTMRecord(IWebDriver driver)
        {
            // Actions
            IWebElement createTMRecord = driver.FindElement(By.XPath("//*[@id=\"container\"]/p/a"));
            IWebElement typeCodeDropdown = driver.FindElement(By.XPath("//*[@id=\"TimeMaterialEditForm\"]/div/div[1]/div/span[1]/span/span[2]/span"));
            IWebElement typeCodeTime = driver.FindElement(By.XPath("//*[@id=\"TypeCode_listbox\"]/li[2]"));
            IWebElement code = driver.FindElement(By.Id("Code"));
            IWebElement description = driver.FindElement(By.Id("Description"));
            IWebElement price = driver.FindElement(By.XPath("//*[@id=\"TimeMaterialEditForm\"]/div/div[4]/div/span[1]/span/input[1]"));
            IWebElement priceTextBox = driver.FindElement(By.Id("Price"));
            IWebElement saveButton = driver.FindElement(By.Id("SaveButton"));

            createTMRecord.Click();
            typeCodeDropdown.Click();
            typeCodeTime.Click();
            code.SendKeys("MAY2024");
            description.SendKeys("Test Analyst");
            price.Click();
            priceTextBox.SendKeys("100");
            saveButton.Click();

            // Assertion
            // Thread.Sleep(3000);
            // Explicit Wait -> Go To Last Page button
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
                int tmRecordsPerPageCount = 10;

                for (int j = 1; j <= 10; j++)
                {
                    String codeText = driver.FindElement(By.XPath("//tr[" + j + "]/td[1]")).Text;
                    String typeCodeText = driver.FindElement(By.XPath("//tr[" + j + "]/td[2]")).Text;
                    String descriptionText = driver.FindElement(By.XPath("//tr[" + j + "]/td[3]")).Text;
                    String priceText = driver.FindElement(By.XPath("//tr[" + j + "]/td[4]")).Text;

                    if (codeText == "MAY2024")
                    {
                        Assert.That(codeText == "MAY2024", "Code value does not match");
                        Assert.That(typeCodeText == "T", "TypeCode value does not match");
                        Assert.That(descriptionText == "Test Analyst", "Description value does not match");
                        Assert.That(priceText.Contains("100"), "Price value does not match");
                        i = tmRecordPageCount;
                        break;
                    }
                }
                // Click on next page
                driver.FindElement(By.XPath("//span[@class='k-icon k-i-arrow-e']")).Click();
            }
        }

        public void editTMRecord(IWebDriver driver)
        {
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
        public void deleteTMRecord(IWebDriver driver)
        {

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
