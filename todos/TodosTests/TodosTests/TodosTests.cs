using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace TodosTests
{
    public class HelloTodosTest : IDisposable
    {
        private readonly TodosPage page;

        public HelloTodosTest()
        {
            page = new TodosPage();
        }

        [Fact]
        internal void AddTaskWithPageObjectTest()
        {
            string newTaskTitle = "testTask";

            page.AddTask(newTaskTitle);

            IReadOnlyCollection<string> tasks = page.GetTasks();
            int activeItemsCount = page.GetActiveTasksCount();
            Assert.Equal(newTaskTitle, tasks.First());
            Assert.Equal(1, activeItemsCount);
        }

        [Fact]
        internal void ModifyTaskTest()
        {
            using IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://todomvc.com/examples/angularjs/#/");

            IWebElement newTaskInput = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                .Until(drv => drv.FindElement(By.ClassName("new-todo")));

            string modifiedTaskTitle = "modified";

            newTaskInput.SendKeys("task1");
            newTaskInput.SendKeys(Keys.Enter);

            IWebElement taskList = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                .Until(drv => drv.FindElement(By.ClassName("todo-list")));

            IReadOnlyCollection<IWebElement> tasks = taskList.FindElements(By.TagName("li"));

            IWebElement firstTask = tasks.First();

            IWebElement firstTaskTitle = firstTask.FindElement(By.TagName("label"));

            Actions actions = new Actions(driver);
            firstTask.Click();
            firstTask.Click();
            actions.DoubleClick(firstTask).Perform();

            IWebElement input = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                .Until(drv => drv.FindElement(By.ClassName("edit")));

            input.SendKeys(Keys.Control + "a");
            input.SendKeys(Keys.Delete);

            input.SendKeys(modifiedTaskTitle);
            input.SendKeys(Keys.Enter);

            Assert.Equal(modifiedTaskTitle, firstTaskTitle.Text);
        }

        public void Dispose()
        {
            page?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}