using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Web.Models.PassViewModels
{
    public class ListViewModel
    {
        public int SkiCardId { get; set; }
        public IEnumerable<ListViewModelItem> Passes { get; set; }
    }

    public class ListViewModelItem
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
