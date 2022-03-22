using Todo.Common;

namespace Todo.Business.Entities.Models
{
    public class TodoItem: IIdentifiableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
