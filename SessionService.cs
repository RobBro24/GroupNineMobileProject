using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupNineMobileProject
{
    public static class SessionService
    {
        public static Profile? CurrentProfile { get; set; }
        public static LocalDbService DbService { get; set; } = new LocalDbService();
    }
}
