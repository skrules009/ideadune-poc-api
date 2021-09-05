using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ideadune_pos.Model
{
    public partial class Login
    {
        public int id { get; set; }
        public string userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime? createDt { get; set; }
        public string role { get; set; }
        public string password { get; set; }
        public string city { get; set; }
        public string address { get; set; }
    }
}
