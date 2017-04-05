using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ybm.Framework.Interface
{
    public interface IFilterable
    {


         List<CustomFilterDescriptor> FilterData { get; set; }
        ActionResult FilterThePage(Dictionary<string, string> selectedFilters);


    }
}
