namespace server.Dtos.UserCategory;

public class CreateUserCategoryDto
{
    public required int UserId { get; set; }
    public required string Name { get; set; }
}