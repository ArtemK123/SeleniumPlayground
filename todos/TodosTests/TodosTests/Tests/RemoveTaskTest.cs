using TodosTests.Page;
using Xunit;

namespace TodosTests.Tests
{
    public class RemoveTaskTest
    {
        [Fact]
        internal void Test()
        {
            using TodosPage page = new TodosPage();
            string taskTitle = "task";

            page.AddTask("testing task");
            page.AddTask(taskTitle);
            int tasksCountOnPageBeforeAct = page.GetTasks().Count;

            page.RemoveTask(taskTitle);

            Assert.DoesNotContain(page.GetTasks(), shownTask => shownTask.Title == taskTitle);
            Assert.Equal(tasksCountOnPageBeforeAct - 1, page.GetActiveTasksCount());
        }
    }
}