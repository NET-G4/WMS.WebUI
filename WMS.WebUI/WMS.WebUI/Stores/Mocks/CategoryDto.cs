namespace WMS.WebUI.Stores.Mocks
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public string? Parent { get; set; }
        public virtual ICollection<CategoryDto> Children { get; set; }
        public CategoryDto()
        {
            Children = new List<CategoryDto>();
        }
    }
}
