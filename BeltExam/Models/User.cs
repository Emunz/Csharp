using System;
using System.Collections.Generic;

namespace BeltExam.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Connection> Connections {get;set;}
        public User()
        {
            Connections = new List<Connection>();
        }
    }
}