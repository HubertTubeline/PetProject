using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PetProject.DAL.Entities;

namespace PetProject.DAL.Interfaces
{
    public interface IUserRepository
    {
        User Get(string userName);
        List<User> Get();
        bool Create(User item);
        bool Update(User item);
        bool Delete(string userName);
    }
}
