using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application;

public interface IFileUpLoader
{
    string UpLoad(IFormFile filen, string path);
}