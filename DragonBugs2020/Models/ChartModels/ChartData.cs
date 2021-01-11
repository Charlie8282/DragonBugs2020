using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBugs2020.Models.ChartModels
{
    public class ChartData
    {
        

        public string Priority { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
        public int ProjectId { get; set; }
    }
}
