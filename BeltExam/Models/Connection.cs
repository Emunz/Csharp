using System;
using System.Collections.Generic;

namespace BeltExam.Models
{
    public class Connection : BaseEntity
    {
        public int ConnectionId { get; set; }
        public int ConnectionStatus { get; set; }
        public int SenderId { get; set; }
        public User Sender{ get; set; }
        public int ReceiverId { get; set; }
        
    }
}