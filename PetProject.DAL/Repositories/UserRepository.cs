using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PetProject.DAL.Entities;
using PetProject.DAL.Interfaces;
using SQLite;

namespace PetProject.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SQLiteConnection _db;

        public UserRepository(string databaseName)
        {
            var dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                databaseName + ".db3");

            _db = new SQLiteConnection(dbPath);
            if (databaseName == "test") TruncateTable();
            InitTables();
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
                item.Id = user.Id;

                _db.Update(item);
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

        private void InitTables()
        {
            _db.CreateTable<User>();
        }

        public bool TruncateTable()
        {
            try
            {
                _db.DeleteAll<User>();
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