using Xunit;

namespace TodosTests.TodosTests
{
    public class RemoveTaskTest
    {
        [Fact]
        internal void Test()
        {
            using TodosPage page = new TodosPage();
            string task = "task";

            page.AddTask("testing task");
            page.AddTask(task);
            int tasksCountOnPageBeforeAct = page.GetTasks().Count;

            page.RemoveTask(task);

            Assert.DoesNotContain(page.GetTasks(), shownTask => shownTask == task);
            Assert.Equal(tasksCountOnPageBeforeAct - 1, page.GetActiveTasksCount());
        }
    }
}