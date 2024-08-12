using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumPracticalExercise.TestCases.Common
{
    public abstract class BaseTest
    {
        public ThreadLocal<IWebDriver> Driver = new();
        public string Url = string.Empty;

        [OneTimeSetUp]
        public virtual void OneTimeSetup()
        {
            Url = "about:blank";
        }

        [SetUp]
        public virtual void Setup()
        {
            Driver.Value = new ChromeDriver();
            Driver.Value.Manage().Window.Maximize();
            Driver.Value.Url = Url;
        }

        [TearDown]
        public virtual void TearDown()
        {
            Driver.Value?.Quit();
        }
    }
}