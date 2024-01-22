namespace server.Responses.Category;

public class CategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public CategoryResponse(Models.Category category)
    {
        Id = category.Id;
        Name = category.Name;
        CreatedDate = category.CreatedDate;
        UpdatedDate = category.UpdatedDate;
    }
}