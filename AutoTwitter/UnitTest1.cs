using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutoEmail
{   [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AutoEmailer()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            IWebDriver driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl("http://www.twitter.com");

            // Get to login page
            IWebElement findLogin = driver.FindElement(By.CssSelector("input[autocomplete='username']"));
            findLogin.SendKeys(/* "Insert username or email here" */);

            // Type in password
            IWebElement typePass = driver.FindElement(By.CssSelector("input[type='password']"));
            typePass.SendKeys(/* "Insert password here" */);
            typePass.SendKeys(Keys.Enter);

            // Click DM button
            IWebElement slideIntoDMs = driver.FindElement(By.ClassName("dm-nav"));
            slideIntoDMs.Click();

            // Click New Message button
            IWebElement newMessage = driver.FindElement(By.XPath("//span[text()='New Message']"));
            newMessage.Click();

            // Type user + enter
            IWebElement DMVictim = driver.FindElement(By.XPath("//textarea[@placeholder='Enter a name']"));
            DMVictim.SendKeys(/* "Enter target user here" */);
            DMVictim.SendKeys(Keys.Enter);

            // Click Next
            IWebElement nextButton = driver.FindElement(By.XPath("//div[@class='DMButtonBar']/button"));
            nextButton.Click();

            // Type intended text
            string intendedDM = /* "Enter message here" */;
            IWebElement autoText = driver.FindElement(By.XPath("//div[@data-default-placeholder='Start a new message']"));
            autoText.SendKeys(intendedDM);

            // Click Send
            IWebElement sendOff = driver.FindElement(By.ClassName("DMComposer-send"));
            sendOff.Click();

            // Confirm Message sent
            var slideConfirm = driver.FindElements(By.XPath("//div[@class='DirectMessage-text']"));
            string sentDM = slideConfirm[slideConfirm.Count - 1].Text;
            Xunit.Assert.Contains(sentDM, intendedDM);
        }
    }
}
