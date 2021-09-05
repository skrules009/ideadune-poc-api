using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ideadune_pos.Model
{
    public partial class BOAccounts
    {
        public int accountId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string address { get; set; }
        public string doorCode { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public string facebook { get; set; }
        public int size { get; set; }
        public string additionalInfo { get; set; }
        public string office { get; set; }
        public string crr { get; set; }
        public string zone { get; set; }
        public string visitFrequency { get; set; }
        public string contactStatus { get; set; }
        public string infoForClinicalTeam { get; set; }
    }
}
