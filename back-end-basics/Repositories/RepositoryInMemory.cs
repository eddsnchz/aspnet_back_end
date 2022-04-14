using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BACKEND.Entities;

namespace BACKEND.Repositories
{
    public class RepositoryInMemory: IRepository
    {
        private List<Gender> _genders;
        private Guid _guid;

        public RepositoryInMemory()
        {
            _genders = new List<Gender>()
            {
                new Gender(){Id = 1, Name = "Horror"},
                new Gender(){Id = 2, Name = "Romantic"},
            };

            _guid = Guid.NewGuid(); //23242-AFASFAFS-3424DSFFADS-AFASFDAF
        }

        public List<Gender> getAllGenders()
        {
            return _genders;
        }


        public async Task<Gender> getById(int Id)
        {
            await Task.Delay(1);
            return _genders.FirstOrDefault(x => x.Id == Id);
        }

        public Guid getGuid()
        {
            return _guid;
        }

        public void createGender(Gender gender)
        {
            gender.Id = _genders.Count() + 1;
            _genders.Add(gender);
        }
    }
}
