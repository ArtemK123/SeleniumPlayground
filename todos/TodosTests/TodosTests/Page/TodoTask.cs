namespace TodosTests.Page
{
    public class TodoTask
    {
        public TodoTask(string title, bool completed = false)
        {
            Title = title;
            Completed = completed;
        }

        public bool Completed { get; }

        public string Title { get; }
    }
}