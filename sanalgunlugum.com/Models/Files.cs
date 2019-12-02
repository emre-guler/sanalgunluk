using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sanalgunlugum.com.Models
{
    [Table("Files")]
    public class Files
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DailyId { get; set; }
        public int UserId { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FileWay { get; set; }
        public string FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}