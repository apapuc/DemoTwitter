﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DemoTwitter.Tests.UserAutomationTests
{
    [TestClass]
    public class UserAutomationTests
    {
        private IWebDriver driver;
        private IWebElement element;
        private string AppUrl = "http://localhost:91/DemoTwitter";
        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        public void LogIn()
        {
            driver.Navigate().GoToUrl(AppUrl + "/Home/Login");
            element = driver.FindElement(By.Id("email"));
            element.SendKeys("apapuc30@yahoo.com");
            element = driver.FindElement(By.Id("password"));
            element.SendKeys("alex");
            element = driver.FindElement(By.Id("login-submit"));
            element.Click();
        }

        [TestMethod]
        public void Accessing_twitter_first_page()
        {
            //Arrange

            string actual;
            string expected = AppUrl + "/Home/Login";

            //Act
            driver.Navigate().GoToUrl(AppUrl + "/Home/Login");
            actual = driver.Url;

            //Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Login_into_twitter_with_an_existing_account()
        {
            //Arrange 
            string actual;
            string expected = AppUrl + "/User/Index";

            //Act
            LogIn();
            actual = driver.Url;

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Login_into_twitter_with_an_unexisting_account()
        {
            //Arrange 
            string actual;
            string expected = "http://localhost:91/DemoTwitter/";

            //Act
            driver.Navigate().GoToUrl(AppUrl);
            element = driver.FindElement(By.Id("email"));
            element.SendKeys("apappucusc@gmail.com");
            element = driver.FindElement(By.Id("password"));
            element.SendKeys("nopasword");
            element = driver.FindElement(By.Id("login-submit"));
            element.Click();
            actual = driver.Url;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Register_Into_Twitter_and_log_in_with_registered_account_Delete_created_account()
        {
            //Arrange
            string actual;
            string expected1 = AppUrl + "/User/Index";
            string expected2 = AppUrl + "/";

            //Act
            driver.Navigate().GoToUrl(AppUrl + "/Home/Register");
            element = driver.FindElement(By.Id("username"));
            element.SendKeys("eugen");
            element = driver.FindElement(By.Id("email"));
            element.SendKeys("eugen@mail.com");
            element = driver.FindElement(By.Id("password"));
            element.SendKeys("eugen");
            element = driver.FindElement(By.Id("lastname"));
            element.SendKeys("Eugen");
            element = driver.FindElement(By.Id("firstname"));
            element.SendKeys("Papuc");
            element = driver.FindElement(By.Id("avatar"));
            element.SendKeys("C:/Users/apapuc/Desktop/pic.jpg");
            element = driver.FindElement(By.Id("login-submit"));
            element.Click();
            element = driver.FindElement(By.Id("login-form-link"));
            element.Click();
            element = driver.FindElement(By.Id("email"));
            element.SendKeys("eugen@mail.com");
            element = driver.FindElement(By.Id("password"));
            element.SendKeys("eugen");
            element = driver.FindElement(By.Id("login-submit"));
            element.Click();
            actual = driver.Url;
            Assert.AreEqual(expected1, actual);
            element = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul[2]/li[1]/a"));
            element.Click();
            element = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul[2]/li[1]/ul/li[2]/a"));
            element.Click();
            actual = driver.Url;

            //Assert
            Assert.AreEqual(expected2, actual);
        }
        [TestMethod]
        public void Going_to_feed_page()
        {
            //Arrange
            string actual;
            string expected = AppUrl + "/Tweet/FollowedUsersFeed";

            //Act
            LogIn();
            element = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul[1]/li[3]/a"));
            element.Click();
            actual = driver.Url;

            //Assect
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Going_to_All_users_page()
        {
            //Arrange
            string actual;
            string expected = AppUrl + "/User/All";

            //Act

            LogIn();
            element = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul[1]/li[2]/a"));
            element.Click();
            actual = driver.Url;

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Checking_follow_functionality()
        {
            //Arrange
            string actual;
            string expected;

            //Act
            LogIn();
            element = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul[1]/li[3]/a"));
            element.Click();
            element = driver.FindElement(By.XPath("/html/body/div/div[3]/ol/li/div[2]"));
            actual = element.Text;
            element = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul[1]/li[2]/a"));
            element.Click();
            element = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[3]/td[4]/a"));
            element.Click();
            element = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul[1]/li[3]/a"));
            element.Click();
            element = driver.FindElement(By.XPath("/html/body/div/div[3]/ol/li[1]/div[2]"));
            expected = element.Text;

            //Assert
            Assert.AreNotEqual(expected, actual);
        }


        [TestMethod]
        public void Logout_from_twitter()
        {
            //Arrange

            string actual;
            string expected = "http://localhost:91/DemoTwitter/";

            //Act

            LogIn();
            element = driver.FindElement(By.XPath("/html/body/nav/div/div[2]/ul[2]/li[2]/a"));
            element.Click();
            actual = driver.Url;

            //Assert

            Assert.AreEqual(expected, actual);
        }
        [TestCleanup]
        public void CleanUp()
        {
            if (driver != null)
            {
                driver.Close();
                driver.Dispose();
            }
        }
    }
}
