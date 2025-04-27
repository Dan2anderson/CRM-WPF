using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CustomerRelationshipManagment.Models;

namespace CustomerRelationshipManagment.Interfaces
{
    public interface IMainViewModel
    {
        public void LoadLeads();
        public void LoadClients();

        public void RecordMowing(Client currentClient);

        public void LeadToClient(Lead currentLead);
    }
}
