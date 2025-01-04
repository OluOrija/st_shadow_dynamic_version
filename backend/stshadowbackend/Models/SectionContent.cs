namespace stshadowbackend.Models
{
    public class SectionContent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; } // Nullable
        public int Order { get; set; }
    }

}
