using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace TodosTests
{
    public class HelloTodosTest
    {
        [Fact]
        internal void AddItemTest()
        {
            using IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://todomvc.com/examples/angularjs/#/");

            string newTaskTitle = "testTask";

            IWebElement newTaskInput = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                .Until(drv => drv.FindElement(By.ClassName("new-todo")));

            newTaskInput.SendKeys(newTaskTitle);
            newTaskInput.SendKeys(Keys.Enter);

            IWebElement taskList = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                .Until(drv => drv.FindElement(By.ClassName("todo-list")));

            IReadOnlyCollection<IWebElement> tasks = taskList.FindElements(By.TagName("li"));

            IWebElement firstTask = tasks.First();

            string firstTaskTitle = firstTask.FindElement(By.TagName("label")).Text;

            string activeItemsCount = driver.FindElement(By.ClassName("todo-count")).FindElement(By.TagName("strong")).Text;

            Assert.Equal(newTaskTitle, firstTaskTitle);
            Assert.Equal(1, int.Parse(activeItemsCount));
        }
    }
}