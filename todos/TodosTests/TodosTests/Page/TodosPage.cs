using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TodosTests.Page
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

        public IReadOnlyCollection<TodoTask> GetTasks()
            => GetTaskElements().Select(GetTaskFromElement).ToList();

        public int GetActiveTasksCount()
        {
            IWebElement activeTaskCountElement = new WebDriverWait(driver, TimeSpan.FromSeconds(DefaultWaitTimeoutSeconds))
                .Until(drv => drv.FindElement(By.ClassName("todo-count")).FindElement(By.TagName("strong")));

            return int.Parse(activeTaskCountElement.Text);
        }

        public void AddTask(string taskTitle)
        {
            IWebElement newTaskInput = new WebDriverWait(driver, TimeSpan.FromSeconds(DefaultWaitTimeoutSeconds))
                .Until(drv => drv.FindElement(By.ClassName("new-todo")));

            newTaskInput.SendKeys(taskTitle);
            newTaskInput.SendKeys(Keys.Enter);
        }

        public void ModifyTask(string currentTaskTitle, string newTitle)
        {
            IWebElement taskElement = GetTaskElement(currentTaskTitle);

            Actions actions = new Actions(driver);
            actions.DoubleClick(taskElement).Perform();

            IWebElement editInput = GetCurrentEditInput();

            editInput.SendKeys(Keys.Control + "a");
            editInput.SendKeys(Keys.Delete);

            editInput.SendKeys(newTitle);
            editInput.SendKeys(Keys.Enter);
        }

        public void RemoveTask(string taskText)
        {
            Actions actions = new Actions(driver);
            IWebElement taskElement = GetTaskElement(taskText);

            actions.MoveToElement(taskElement);

            IWebElement destroySubElement = taskElement.FindElement(By.ClassName("destroy"));
            actions.MoveToElement(destroySubElement);
            actions.Click();
            actions.Perform();
        }

        public void CompleteTask(string taskText)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            driver?.Dispose();
        }

        private static TodoTask GetTaskFromElement(IWebElement taskElement)
            => new TodoTask(GetTaskElementText(taskElement), GetCompleteState(taskElement));

        private static string GetTaskElementText(IWebElement taskElement)
            => taskElement.FindElement(By.TagName("label")).Text;

        private static bool GetCompleteState(IWebElement taskElement)
            => taskElement.FindElements(By.ClassName("ng-touched")).Any();

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