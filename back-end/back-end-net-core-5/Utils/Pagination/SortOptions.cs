using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end_net_core_5.Utils.Pagination
{
    public class SortOptions
    {
        public bool Reverse { get; set; }
        public string Sort { get; set; }
    }
}
