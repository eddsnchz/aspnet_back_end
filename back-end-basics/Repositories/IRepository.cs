using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BACKEND.Entities;

namespace BACKEND.Repositories
{
    public interface IRepository
    {
        void createGender(Gender gender);
        List<Gender> getAllGenders();
        Task<Gender> getById(int Id);
        Guid getGuid();
    }
}
