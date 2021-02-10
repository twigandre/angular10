using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end_net_core_5.Utils.Pagination
{
    public class PagedOptions
    {
        public int? Page { get; set; } = 1;
        public bool Reverse { get; set; }
        public int? Size { get; set; } = 10;
        public string Sort { get; set; }
        /// <summary>
        /// Limite de registros na busca
        /// </summary>
        public int? RegLimited { get; set; }
        public IEnumerable<SortOptions> SortManny { get; set; }
    }
}
