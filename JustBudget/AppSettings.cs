using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustBudget
{
    public class AppSettings
    {
        public double FontSize { get; set; } = 14;
        public int FilterMonth { get; set; } = 0; // 0 = all
        public int FilterYear { get; set; } = 0;  // 0 = all
    }

}
