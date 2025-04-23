using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupNineMobileProject
{
    [Table("logged_games")]
    public class LoggedGames
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public double? Rating { get; set; } = 0.0;
        public string Publisher { get; set; }
        public DateTime DateLogged { get; set; } = DateTime.Now;
    }
}
