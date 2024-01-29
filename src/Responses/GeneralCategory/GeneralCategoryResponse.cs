namespace server.Responses.GeneralCategory;

public class GeneralCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public GeneralCategoryResponse(Models.GeneralCategory generalCategory)
    {
        Id = generalCategory.Id;
        Name = generalCategory.Name;
        CreatedDate = generalCategory.CreatedDate;
        UpdatedDate = generalCategory.UpdatedDate;
    }
}