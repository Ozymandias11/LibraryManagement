using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Helpers
{
    public class MonthlyRegistrationReport
    {
        public required string MonthName { get; set; }
        public int Registrations { get; set; }
    }
}
