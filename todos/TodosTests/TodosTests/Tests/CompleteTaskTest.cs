using System.Linq;
using TodosTests.Page;
using Xunit;

namespace TodosTests.Tests
{
    public class CompleteTaskTest
    {
        [Fact]
        internal void Test()
        {
            using TodosPage page = new TodosPage();
            string taskTitle = "testing task";

            page.AddTask("mock task");
            page.AddTask(taskTitle);
            int tasksCountOnPageBeforeAct = page.GetTasks().Count;

            page.CompleteTask(taskTitle);

            TodoTask completedTask = page.GetTasks().First(todoTask => todoTask.Completed);

            Assert.Equal(taskTitle, completedTask.Title);
            Assert.Equal(tasksCountOnPageBeforeAct - 1, page.GetActiveTasksCount());
        }
    }
}