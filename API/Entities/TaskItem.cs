namespace API.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Day { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}
