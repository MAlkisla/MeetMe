using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetMe.Models
{
    public class HomeViewModel
    {
        public List<MeetingViewModel> Meetings { get; set; }
        public int TotalItemCount { get; set; }
        public int ItemCount { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int MyProperty { get; set; }
        public bool IsPrevious { get; set; }
        public bool IsNext { get; set; }

    }
}
