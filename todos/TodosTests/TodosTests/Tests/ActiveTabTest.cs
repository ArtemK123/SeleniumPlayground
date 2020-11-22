using System.Collections.Generic;
using System.Linq;
using TodosTests.Page;
using Xunit;

namespace TodosTests.Tests
{
    public class ActiveTabTest
    {
        [Fact]
        internal void Test()
        {
            using TodosPage page = new TodosPage();

            string[] completedTaskTitles = { "completed1", "completed2" };
            string[] nonCompletedTaskTitles = { "active1", "active2", "active3" };

            foreach (string task in completedTaskTitles.Union(nonCompletedTaskTitles))
            {
                page.AddTask(task);
            }

            foreach (string taskToComplete in completedTaskTitles)
            {
                page.CompleteTask(taskToComplete);
            }

            page.SelectTab(TodosTab.Active);

            IReadOnlyCollection<TodoTask> shownTasks = page.GetTasks();

            Assert.True(shownTasks.All(task => !task.Completed));
            Assert.Equal(nonCompletedTaskTitles, shownTasks.Select(task => task.Title).ToArray());
        }
    }
}