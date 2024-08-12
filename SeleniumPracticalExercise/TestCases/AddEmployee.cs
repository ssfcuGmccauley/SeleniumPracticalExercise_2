using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SeleniumPracticalExercise.Common;
using SeleniumPracticalExercise.PageObjects.Common;
using SeleniumPracticalExercise.TestCases.Common;
using OpenQA.Selenium.Support.UI;

namespace SeleniumPracticalExercise.TestCases
{
    class AddEmployee : BaseTestLocal
    {
        [Test]
        [Category("Add Employee")]
        public void AddEmployeeTest()
        {
            // Steps to automate:
            // 1. Navigate to https://opensource-demo.orangehrmlive.com/web/index.php/auth/login
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            // 2. Log in using Username: Admin, Password: admin123
            IWebElement usernameField = driver.FindElement(By.CssSelector("input[name='username']"));
            usernameField.SendKeys("Admin");
            IWebElement passwordField = driver.FindElement(By.CssSelector("input[name='password']"));
            passwordField.SendKeys("admin123");
            IWebElement loginBtn = driver.FindElement(By.CssSelector("button[type = 'submit']"));
            loginBtn.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            // 3. Click "PIM" in the left nav
            IWebElement menuItemPIM = driver.FindElement(By.CssSelector("a[href = '/web/index.php/pim/viewPimModule']"));
            menuItemPIM.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            // 4. Click "+Add" //button[. = ' Add ']
            IWebElement addBtn = driver.FindElement(By.XPath("//button[. = ' Add ']"));
            addBtn.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            // 5. Randomly generate a first name (6 characters) and last name (8 characters) and enter them into the form
            string firstName = Utils.GenerateRandomString(6);
            string lastName = Utils.GenerateRandomString(8);
            IWebElement firstNameField = driver.FindElement(By.Name("firstName"));
            firstNameField.SendKeys(firstName);
            IWebElement lastNameField = driver.FindElement(By.Name("lastName"));
            lastNameField.SendKeys(lastName);
            // 6. Get the Employee Id for use later //label[@class='oxd-label']/parent::div/following-sibling::div/child::input
            IWebElement employeeIDField = driver.FindElement(By.XPath("//label[@class='oxd-label']/parent::div/following-sibling::div/child::input"));
            string employeeID = employeeIDField.GetAttribute("value");
            // 7. Click Save button[type='submit']
            IWebElement saveBtn = driver.FindElement(By.CssSelector("button[type='submit']"));
            saveBtn.Click();
            Thread.Sleep(2000);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            // 8. Click "PIM" in the left nav
            IWebElement menuItemPIM2 = driver.FindElement(By.CssSelector("a[href = '/web/index.php/pim/viewPimModule']"));
            menuItemPIM2.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            // 8. Search for the employee you just created by Employee Id
            IWebElement searchIDField = driver.FindElement(By.XPath("//label[@class='oxd-label']/parent::div/following-sibling::div/child::input"));
            searchIDField.SendKeys(employeeID);
            IWebElement searchBtn = driver.FindElement(By.CssSelector("button[type='submit']"));
            searchBtn.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            // 9. In the employee search results, use NUnit asserts to validate that Id, First Name, and Last Name are correct
            //ID validation
            IWebElement resultID = driver.FindElement(By.XPath("//div[@class= 'oxd-table-row oxd-table-row--with-border oxd-table-row--clickable']/div[2]"));
            string resultIDStr = resultID.Text;
            Assert.That(employeeID, Is.EqualTo(resultIDStr));
            //First Name Validation
            IWebElement resultFirstName = driver.FindElement(By.XPath("//div[@class= 'oxd-table-row oxd-table-row--with-border oxd-table-row--clickable']/div[3]"));
            string resultFirstNameStr = resultFirstName.Text;
            Assert.That(firstName, Is.EqualTo(resultFirstNameStr));
            //Last Name validation
            IWebElement resultLastName = driver.FindElement(By.XPath("//div[@class= 'oxd-table-row oxd-table-row--with-border oxd-table-row--clickable']/div[4]"));
            string resultLastNameStr = resultLastName.Text;
            Assert.That(lastName, Is.EqualTo(resultLastNameStr));

            // NOTE:
            // - Use the provided WebDriver methods in BasePageLocal.cs
            // - Create page objects as needed
            // - Document all methods using XML documentation, https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/

        }
    }
}