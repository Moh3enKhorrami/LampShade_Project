using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CommentManagement.Application;
using ShopManagement.Application;

namespace CommentManagement.Configuration;

public class CommentManagementBoostrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
       
        services.AddTransient<ICommentApplication, CommentApplication>();
        services.AddTransient<ICommentRepository, CommentRepository>();
        
        services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));
        
    }
}
