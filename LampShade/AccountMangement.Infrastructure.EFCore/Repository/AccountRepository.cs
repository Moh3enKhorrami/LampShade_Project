using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountMangement.Infrastructure.EFCore.Repository;

public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
{
    private readonly AccountContext _context;
    public AccountRepository( AccountContext context) : base(context)
    {
        _context = context;
    }

    public EditAccount GetDetails(long id)
    {
        return _context.Accounts.Select(x => new EditAccount()
        {
            Id = x.Id,
            FullName = x.FullName,
            Mobile = x.Mobile,
            RoleId = x.RoleId,
            UserName = x.UserName
        }).FirstOrDefault(x => x.Id == id);
    }

    public ChangePassword GetChange(long id)
    {
        return _context.Accounts.Select(x => new ChangePassword()
        {
            Id = x.Id
        }).FirstOrDefault(x => x.Id == id);
    }


    public List<AccountViewModel> Search(AccountSearchModel searchModel)
    {
        var query = _context.Accounts
            .Include(x => x.Role)
            .Select(x => new AccountViewModel()
        {
            Id = x.Id,
            FullName = x.FullName,
            Mobile = x.Mobile,
            ProfilePhoto = x.ProfilePhoto,
            Role = x.Role.Name,
            RoleId = x.RoleId,
            UserName = x.UserName,
            CreationDate = x.CreationDate.ToString()
        });
        if (!string.IsNullOrWhiteSpace(searchModel.Fullname))
            query = query.Where(x => x.FullName.Contains(searchModel.Fullname));

        if (!string.IsNullOrWhiteSpace(searchModel.Username))
            query = query.Where(x => x.UserName.Contains(searchModel.Username));

        if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
            query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));
        
        if (searchModel.RoleId > 0)
            query = query.Where(x => x.RoleId == searchModel.RoleId);

        return query.OrderByDescending(x => x.Id).ToList();
    }

    public Account GetBy(string username)
    {
        return _context.Accounts.FirstOrDefault(x => x.UserName == username);
    }
}