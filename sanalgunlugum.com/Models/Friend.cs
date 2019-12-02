using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sanalgunlugum.com.Models
{
    [Table("Friends")]
    public class Friend
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string FriendText { get; set; }
        public string PhotoWay { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}