using System.Collections.Generic;
using System.Linq;
using TodosTests.Page;
using Xunit;

namespace TodosTests.Tests
{
    public class CompletedTabTest
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

            page.SelectTab(TodosTab.Completed);

            IReadOnlyCollection<TodoTask> shownTasks = page.GetTasks();

            Assert.True(shownTasks.All(task => task.Completed));
            Assert.Equal(completedTaskTitles, shownTasks.Select(task => task.Title).ToArray());
        }
    }
}