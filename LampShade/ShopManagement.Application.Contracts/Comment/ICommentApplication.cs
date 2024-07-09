using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Comment;

public interface ICommentApplication
{
    List<CommentViewModel> Search(CommentSearchModel searchModel);
    OperationResult Add(AddComment command);
    OperationResult Confirm(long id);
    OperationResult Cancel(long id);
}