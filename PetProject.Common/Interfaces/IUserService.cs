using System;
using System.Collections.Generic;
using System.Text;
using PetProject.Common.Models;

namespace PetProject.Common.Interfaces
{
    public interface IUserService
    {
        UserModel Get(string userName);
        bool Create(UserModel model);
        bool Update(UserModel model);
        bool Delete(string userName);
    }
}
