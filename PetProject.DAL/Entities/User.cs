using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PetProject.DAL.Entities
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int UserId { get; set; }

        [MaxLength(128)]
        public string UserName { get; set; }

        [MaxLength(64)]
        public int MaxScore { get; set; }
    }
}
