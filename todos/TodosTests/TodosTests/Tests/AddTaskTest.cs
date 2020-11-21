using System.Collections.Generic;
using System.Linq;
using TodosTests.Page;
using Xunit;

namespace TodosTests.Tests
{
    public class AddTaskTest
    {
        [Fact]
        internal void Test()
        {
            using TodosPage page = new TodosPage();
            string newTaskTitle = "testTask";

            page.AddTask(newTaskTitle);

            IReadOnlyCollection<TodoTask> tasks = page.GetTasks();
            int activeItemsCount = page.GetActiveTasksCount();
            Assert.Equal(newTaskTitle, tasks.First().Title);
            Assert.Equal(1, activeItemsCount);
        }
    }
}