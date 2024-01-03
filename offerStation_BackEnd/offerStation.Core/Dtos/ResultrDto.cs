using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class ResultrDto<T>
    {
       public List<T> List { get; set; }
       public long itemsCount { get; set; }
    }
}
