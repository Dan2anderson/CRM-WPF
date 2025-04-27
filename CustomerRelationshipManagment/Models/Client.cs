using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRelationshipManagment.Models
{
    public class Client
    {
        public int Id { get; set; } // Primary Key
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public required string Frequency { get; set; }
        public required decimal Price { get; set; }
        public DateOnly? DateLastMowed { get; set; }
        public DateOnly? DateScheduled { get; set; }
        public required double Acres { get; set; }
        public string? Notes { get; set; }

        public int? RecentHoursMowing { get; set; }

        public List<int> HistoricHoursMowing { get; set; } = new List<int>();

    }
}
