namespace server.Responses.UserCategory;

public class UserCategoryResponse
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public UserCategoryResponse(Models.UserCategory userCategory)
    {
        Id = userCategory.Id;
        Name = userCategory.Name;
        UserId = userCategory.UserId;
        CreatedDate = userCategory.CreatedDate;
        UpdatedDate = userCategory.UpdatedDate;
    }
}