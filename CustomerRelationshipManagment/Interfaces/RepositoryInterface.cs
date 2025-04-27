using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerRelationshipManagment.db;
using CustomerRelationshipManagment.Models;

namespace CustomerRelationshipManagment.Interfaces
{
    public interface RepositoryInterface
    {
        public List<Lead> getLeads();
        public List<Client> getClients();
        public Client getClientById(int id);
        public void saveChanges();
        public void addClient(Client newClient);
        public void deleteLead(Lead lead);
    }
}
