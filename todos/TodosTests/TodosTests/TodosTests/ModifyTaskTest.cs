using System.Linq;
using Xunit;

namespace TodosTests.TodosTests
{
    public class ModifyTaskTest
    {
        [Fact]
        internal void Test()
        {
            using TodosPage page = new TodosPage();
            string taskTitle = "task";
            string modifiedTaskTitle = "modified";

            page.AddTask(taskTitle);
            page.ModifyTask(taskTitle, modifiedTaskTitle);

            string actual = page.GetTasks().First();
            Assert.Equal(modifiedTaskTitle, actual);
        }
    }
}