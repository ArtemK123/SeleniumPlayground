using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
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

        public IReadOnlyCollection<string> GetTasks()
            => GetTaskElements().Select(GetTaskElementText).ToList();

        public int GetActiveTasksCount()
        {
            IWebElement activeTaskCountElement = new WebDriverWait(driver, TimeSpan.FromSeconds(DefaultWaitTimeoutSeconds))
                .Until(drv => drv.FindElement(By.ClassName("todo-count")).FindElement(By.TagName("strong")));

            return int.Parse(activeTaskCountElement.Text);
        }

        public void AddTask(string title)
        {
            IWebElement newTaskInput = new WebDriverWait(driver, TimeSpan.FromSeconds(DefaultWaitTimeoutSeconds))
                .Until(drv => drv.FindElement(By.ClassName("new-todo")));

            newTaskInput.SendKeys(title);
            newTaskInput.SendKeys(Keys.Enter);
        }

        public void ModifyTask(string currentTitle, string newTitle)
        {
            IWebElement task = GetTaskElement(currentTitle);

            Actions actions = new Actions(driver);
            actions.DoubleClick(task).Perform();

            IWebElement editInput = GetCurrentEditInput();

            editInput.SendKeys(Keys.Control + "a");
            editInput.SendKeys(Keys.Delete);

            editInput.SendKeys(newTitle);
            editInput.SendKeys(Keys.Enter);
        }

        public void RemoveTask(string taskTitle)
        {
            Actions actions = new Actions(driver);
            IWebElement taskElement = GetTaskElement(taskTitle);

            actions.MoveToElement(taskElement);

            IWebElement destroySubElement = taskElement.FindElement(By.ClassName("destroy"));
            actions.MoveToElement(destroySubElement);
            actions.Click();
            actions.Perform();
        }

        public void Dispose()
        {
            driver?.Dispose();
        }

        private static string GetTaskElementText(IWebElement taskElement) => taskElement.FindElement(By.TagName("label")).Text;

        private IReadOnlyCollection<IWebElement> GetTaskElements()
        {
            IWebElement taskList = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                .Until(drv => drv.FindElement(By.ClassName("todo-list")));

            return taskList.FindElements(By.TagName("li"));
        }

        private IWebElement GetTaskElement(string taskTitle)
            => GetTaskElements().First(taskElement => GetTaskElementText(taskElement) == taskTitle);

        private IWebElement GetCurrentEditInput()
            => new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                .Until(drv => drv.FindElement(By.ClassName("edit")));
    }
}