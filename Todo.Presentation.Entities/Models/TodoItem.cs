using Todo.Common.Interface;

namespace Todo.Presentation.Entities
{
    public class TodoItem : IIdentifiableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}