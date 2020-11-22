using System.Collections.Generic;
using System.Linq;
using TodosTests.Page;
using Xunit;

namespace TodosTests.Tests
{
    public class CompleteAllCompletedTasksTest
    {
        [Fact]
        internal void Test()
        {
            using TodosPage page = new TodosPage();

            string[] taskTitles = { "task1", "task2", "task3" };

            foreach (string task in taskTitles)
            {
                page.AddTask(task);
                page.CompleteTask(task);
            }

            page.CompleteAll();

            IReadOnlyCollection<TodoTask> shownTasks = page.GetTasks();

            Assert.True(shownTasks.All(task => !task.Completed));
            Assert.Equal(taskTitles, shownTasks.Select(task => task.Title).ToArray());
        }
    }
}