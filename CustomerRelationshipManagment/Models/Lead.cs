using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRelationshipManagment.Models
{
    public class Lead
    {
        public int Id { get; set; } // Primary Key
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public required string Frequency { get; set; }
        public required decimal QuotePrice { get; set; }
        public required double Acres { get; set; }
        public DateOnly? DateScheduled { get; set; }
        public string? Notes { get; set; }
    }
}
