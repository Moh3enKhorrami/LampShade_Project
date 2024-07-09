namespace ShopManagement.Application.Contracts.Comment;

public class CommentViewModel
{
    public double Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public bool IsConfirmed { get; set; }
    public bool IsCanceled { get; set; }
    public string CommentDate { get; set; }
}