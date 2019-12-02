using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sanalgunlugum.com.Models
{
    [Table("Daily")]
    public class Daily
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DailyTitle { get; set; }
        public string DailyText { get; set; }
        public string Mood { get; set; }
        public string FriendTag { get; set; }
        public string BookColor { get; set; }
        public bool DailyDeleteStatus { get; set; }
        public DateTime DailyDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}