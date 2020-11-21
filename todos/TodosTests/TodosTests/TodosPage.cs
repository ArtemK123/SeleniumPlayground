using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TodosTests
{
    internal class TodosPage : IDisposable
    {
        private const int DefaultWaitTimeoutSeconds = 3;

        private readonly IWebDriver driver;

        public TodosPage()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://todomvc.com/examples/angularjs/#/");
        }

        public void AddTask(string title)
        {
            IWebElement newTaskInput = new WebDriverWait(driver, TimeSpan.FromSeconds(DefaultWaitTimeoutSeconds))
                .Until(drv => drv.FindElement(By.ClassName("new-todo")));

            newTaskInput.SendKeys(title);
            newTaskInput.SendKeys(Keys.Enter);
        }

        public IReadOnlyCollection<string> GetTasks()
        {
            IWebElement taskList = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                .Until(drv => drv.FindElement(By.ClassName("todo-list")));

            IReadOnlyCollection<IWebElement> tasks = taskList.FindElements(By.TagName("li"));

            return tasks.Select(GetTaskTextFromLiTag).ToList();
        }

        public int GetActiveTasksCount()
        {
            IWebElement activeTaskCountElement = new WebDriverWait(driver, TimeSpan.FromSeconds(DefaultWaitTimeoutSeconds))
                .Until(drv => drv.FindElement(By.ClassName("todo-count")).FindElement(By.TagName("strong")));

            return int.Parse(activeTaskCountElement.Text);
        }

        public void Dispose()
        {
            driver?.Dispose();
        }

        private static string GetTaskTextFromLiTag(IWebElement liTag) => liTag.FindElement(By.TagName("label")).Text;
    }
}