using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class OwnerAnalysis
    {
    }
    public class AnalysisResult
    {
        public long Count { get; set; }
        public string Name { get; set; }
    }

    public class customerInfoAnalysis
    {
        public long ordersCount { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
