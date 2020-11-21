using System.Linq;
using TodosTests.Page;
using Xunit;

namespace TodosTests.Tests
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

            string actual = page.GetTasks().First().Title;
            Assert.Equal(modifiedTaskTitle, actual);
        }
    }
}