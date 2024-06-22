using WMS.WebUI.Stores.Mocks;

namespace WMS.WebUI.ViewModels;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? ParentId { get; set; }
    public string? Parent { get; set; }
    public virtual ICollection<CategoryDto> Children { get; set; }
    public virtual ICollection<object> Products { get; set; }
    public CategoryViewModel()
    {
        Children = new List<CategoryDto>();
        Products = new List<object>();
    }
}

// read
// create
// update
