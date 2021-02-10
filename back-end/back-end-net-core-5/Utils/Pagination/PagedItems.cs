using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end_net_core_5.Utils.Pagination
{
    public class PagedItems<T>
    {
        public IList<T> Items { get; set; }
        public long? Total { get; set; }
    }
}
