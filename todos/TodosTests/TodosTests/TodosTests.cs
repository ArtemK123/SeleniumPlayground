using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TodosTests
{
    public class HelloTodosTest : IDisposable
    {
        private readonly TodosPage page;

        public HelloTodosTest()
        {
            page = new TodosPage();
        }

        [Fact]
        internal void AddTaskTest()
        {
            string newTaskTitle = "testTask";

            page.AddTask(newTaskTitle);

            IReadOnlyCollection<string> tasks = page.GetTasks();
            int activeItemsCount = page.GetActiveTasksCount();
            Assert.Equal(newTaskTitle, tasks.First());
            Assert.Equal(1, activeItemsCount);
        }

        [Fact]
        internal void ModifyTaskTest()
        {
            string taskTitle = "task";
            string modifiedTaskTitle = "modified";

            page.AddTask(taskTitle);
            page.ModifyTask(taskTitle, modifiedTaskTitle);

            string actual = page.GetTasks().First();
            Assert.Equal(modifiedTaskTitle, actual);
        }

        [Fact]
        internal void RemoveTaskTest()
        {
            string task = "task";

            page.AddTask("testing task");
            page.AddTask(task);
            int tasksCountOnPageBeforeAct = page.GetTasks().Count;

            page.RemoveTask(task);

            Assert.DoesNotContain(page.GetTasks(), shownTask => shownTask == task);
            Assert.Equal(tasksCountOnPageBeforeAct - 1, page.GetActiveTasksCount());
        }

        public void Dispose()
        {
            page?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}