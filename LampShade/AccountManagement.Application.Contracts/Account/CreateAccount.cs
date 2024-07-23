using System.ComponentModel.DataAnnotations;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Account;

public class CreateAccount
{
    [Required]
    public string FullName { get; set; }
    
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Mobile { get; set; }
    
    
    public long RoleId { get; set; }
    
    public IFormFile ProfilePhoto { get; set; }
    public List<RoleViewModel> Roles { get; set; }
}