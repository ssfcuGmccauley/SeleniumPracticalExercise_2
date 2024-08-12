using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumPracticalExercise.PageObjects.Common
{
    public class BasePageLocal
    {
        protected IWebDriver Driver;

        public BasePageLocal(IWebDriver driver)
        {
            Driver = driver;
        }

        // Any methods that are WebDriver specific and would go into BasePage but only apply to this project go here

        /// <summary>
        /// Clears the text from an INPUT element
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <param name="timeOut">[Optional] How long to wait for the element (in seconds). The default timeOut is 10s.</param>
        public virtual void Clear(By locator, int timeOut = 10)
        {
            DateTime now = DateTime.Now;
            while (DateTime.Now < now.AddSeconds(timeOut))
            {
                try
                {
                    new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementToBeClickable(locator)).Clear();

                    return;
                }
                catch (StaleElementReferenceException)
                {
                    // do nothing, loop again
                }
            }

            throw new Exception($"Not able to clear element <{locator}> within {timeOut}s.");
        }

        /// <summary>
        /// Clicks on an element
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <param name="timeOut">[Optional] How long to wait for the element (in seconds). The default timeOut is 10s.</param>
        public virtual void Click(By locator, int timeOut = 10)
        {
            DateTime now = DateTime.Now;
            while (DateTime.Now < now.AddSeconds(timeOut))
            {
                try
                {
                    ScrollToElement(locator, timeOut);
                    new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementToBeClickable(locator)).Click();

                    return;
                }
                catch (ElementClickInterceptedException)
                {
                    // do nothing, loop again
                }
                catch (StaleElementReferenceException)
                {
                    // do nothing, loop again
                }
            }

            throw new Exception($"Not able to click element <{locator}> within {timeOut}s.");
        }

        /// <summary>
        /// Returns the value of the desired element
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The text contained in the value</returns>
        public virtual string GetValue(By locator)
        {
            int timeOut = 10;
            DateTime now = DateTime.Now;
            while (DateTime.Now < now.AddSeconds(timeOut))
            {
                try
                {
                    return FindElement(ExpectedConditions.ElementIsVisible(locator)).GetAttribute("value");
                }
                catch (StaleElementReferenceException)
                {
                    // do nothing, loop again
                }
            }

            throw new Exception($"Not able to get 'value' from element <{locator}> within {timeOut}s.");
        }

        /// <summary>
        /// Enters the provided text in the specified element.
        ///
        /// NOTE: This should only be used on non-INPUT elements. Use EditBoxSendKeysAndVerify() instead.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <param name="sendKeysVal">The string to be typed.</param>
        public virtual void SendKeys(By locator, string sendKeysVal)
        {
            FindElement(ExpectedConditions.ElementIsVisible(locator)).SendKeys(sendKeysVal);
        }

        /// <summary>
        /// Enters and verifies the provided text in the specified element.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <param name="sendKeysVal">The string to be typed.</param>
        /// <param name="timeOut">[Optional] How long to wait for the element (in seconds). The default timeOut is 10s.</param>
        /// <param name="verify">[Optional] True if the value should be verified, false otherwise</param>
        public virtual void EditBoxSendKeysAndVerify(By locator, string sendKeysVal, int timeOut = 10, bool verify = true)
        {
            DateTime now = DateTime.Now;
            while (DateTime.Now < now.AddSeconds(timeOut))
            {
                try
                {
                    Click(locator);
                    Clear(locator);
                    IWebElement e = FindElement(locator);
                    e.SendKeys(sendKeysVal);
                    WaitAfterAction();
                    if (verify)
                    {
                        Assert.AreEqual(sendKeysVal, e.GetAttribute("value"), "Verify that the correct value was entered.");
                    }

                    return;
                }
                catch (StaleElementReferenceException)
                {
                    // do nothing, loop again
                }
            }

            throw new Exception($"Not able to .SendKeys() to element <{locator}> within {timeOut}s.");
        }

        /// <summary>
        /// Finds the element with the provided locator
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The found IWebElement</returns>
        public virtual IWebElement FindElement(By locator)
        {
            return Driver.FindElement(locator);
        }

        /// <summary>
        /// Finds the element using the provided wait condition
        /// </summary>
        /// <param name="waitCondition">The wait condition can be an ExpectedCondition or a custom wait that returns an IWebElement</param>
        /// <param name="timeOut">[Optional] How long to wait for the element (in seconds). The default timeOut is 10s.</param>
        /// <returns>The found IWebElement</returns>
        public virtual IWebElement FindElement(Func<IWebDriver, IWebElement> waitCondition, int timeOut = 10)
        {
            return new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOut)).Until(waitCondition);
        }

        /// <summary>
        /// Finds all elements with the provided locator
        /// </summary>
        /// <param name="locator">The locator used to find the elements.</param>
        /// <returns>The collection of IWebElement</returns>
        public virtual IReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            return Driver.FindElements(locator);
        }

        /// <summary>
        /// Finds all elements using the provided wait condition
        /// </summary>
        /// <param name="waitCondition">The wait condition can be an ExpectedCondition or a custom wait that returns a collection of IWebElement</param>
        /// <param name="timeOut">[Optional] How long to wait for the element (in seconds). The default timeOut is 10s.</param>
        /// <returns>The collection of found IWebElements</returns>
        public virtual IReadOnlyCollection<IWebElement> FindElements(Func<IWebDriver, IReadOnlyCollection<IWebElement>> waitCondition, int timeOut = 10)
        {
            return new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOut)).Until(waitCondition);
        }

        /// <summary>
        /// Returns the text of the desired element
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The text contained in the element</returns>
        public virtual string GetText(By locator)
        {
            int timeOut = 10;
            DateTime now = DateTime.Now;
            while (DateTime.Now < now.AddSeconds(timeOut))
            {
                try
                {
                    return FindElement(ExpectedConditions.ElementIsVisible(locator)).Text;
                }
                catch (StaleElementReferenceException)
                {
                    // do nothing, loop again
                }
            }

            throw new Exception($"Not able to get .Text from element <{locator}> within {timeOut}s.");
        }

        /// <summary>
        /// Scrolls the specified element to the center of the window using Javascript.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <param name="timeOut">How long to wait for the element in seconds (optional). The default timeOut is 10s.</param>
        public virtual void ScrollToElement(By locator, int timeOut = 10)
        {
            try
            {
                Driver.ExecuteJavaScript("window.scrollTo(0, arguments[0].getBoundingClientRect().top + window.pageYOffset - (window.innerHeight / 2));", FindElement(ExpectedConditions.ElementIsVisible(locator), timeOut));
            }
            catch (Exception e)
            {
                throw new Exception("Unable to scroll to the given web element.", e);
            }
        }

        /// <summary>
        /// Waits for the specified duration of time.
        /// </summary>
        /// <param name="waitTime">[Optional] The time to wait (in milliseconds). The default time is 300ms.</param>
        public virtual void WaitAfterAction(int waitTime = 300)
        {
            try
            {
                Thread.Sleep(waitTime);
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong while sleeping.");
            }
        }
    }
}