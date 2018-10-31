using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PetProject.DAL.Entities;
using PetProject.DAL.Interfaces;
using SQLite;

namespace PetProject.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SQLiteConnection _db;

        public UserRepository()
        {
            string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "database.db3");
            _db = new SQLiteConnection(dbPath);
        }

        private void InitTable()
        {
            _db.CreateTable<User>();
        }

        public List<User> Get()
        {
            return _db.Table<User>().ToList();
        }

        public User Get(string userName)
        {
            return _db.Table<User>().FirstOrDefault(x => x.UserName == userName);
        }

        public bool Create(User item)
        {
            try
            {
                var user = Get(item.UserName);
                if (user == null)
                    return _db.Insert(item) == 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return false;
        }

        public bool Update(User item)
        {
            try
            {
                var user = Get(item.UserName);
                user.RaceMaxScore = item.RaceMaxScore;
                user.FlappyMaxScore = item.FlappyMaxScore;
                _db.Update(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        public bool Delete(string userName)
        {
            try
            {
                _db.Table<User>().Delete(x => x.UserName == userName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }
    }
}
