using static SmartGym.Components.Display.ClassTable;

namespace SmartGym.Components.UIClasses.Classes
{
    public class ClassVM
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public DateTime schedule { get; set; }
        public int capacity { get; set; }
        public int trainerId { get; set; }
        public TempUserVM? trainer { get; set; }
        public int? categoryId { get; set; }
        public List<TempUserVM>? attending { get; set; }
    }
}