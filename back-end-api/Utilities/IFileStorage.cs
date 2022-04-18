using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BACKEND.Utilities
{
    public interface IFileStorage
    {
        Task DeleteFile(string route, string container);
        Task<string> EditFile(string container, IFormFile file, string route);
        Task<string> SaveFile(string container, IFormFile file);
    }
}

