using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TodosTests.TodosTests
{
    public class AddTaskTest
    {
        [Fact]
        internal void Test()
        {
            using TodosPage page = new TodosPage();
            string newTaskTitle = "testTask";

            page.AddTask(newTaskTitle);

            IReadOnlyCollection<string> tasks = page.GetTasks();
            int activeItemsCount = page.GetActiveTasksCount();
            Assert.Equal(newTaskTitle, tasks.First());
            Assert.Equal(1, activeItemsCount);
        }
    }
}