using System;

namespace WpfMvvmSkeleton.Models
{
    // A Model is a plain data class.
    // No UI references, no ViewModel references — just data and shape.
    // Replace / extend this with your real domain objects.
    public class SampleItem
    {
        public int      Id          { get; set; }
        public string   Name        { get; set; }
        public string   Description { get; set; }
        public DateTime CreatedAt   { get; set; }
        public bool     IsActive    { get; set; }
    }
}
