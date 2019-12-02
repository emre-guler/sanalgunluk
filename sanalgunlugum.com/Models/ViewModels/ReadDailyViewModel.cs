using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sanalgunlugum.com.Models.ViewModels
{
    public class ReadDailyViewModel
    {
        public int Id { get; set; }
        public string DailyText { get; set; }
        public string DailyTitle { get; set; }
        public DateTime DailyDate { get; set; }
    }
}