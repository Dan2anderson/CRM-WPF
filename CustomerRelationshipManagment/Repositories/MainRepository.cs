using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CustomerRelationshipManagment.db;
using CustomerRelationshipManagment.Interfaces;
using CustomerRelationshipManagment.Models;

namespace CustomerRelationshipManagment.Repositories
{
    public class MainRepository : RepositoryInterface
    {
        private AppDbContext db = new AppDbContext();

        public MainRepository()
        {
            db.Database.EnsureCreated();
        }

        public List<Lead> getLeads()
        {
            return db.Leads.ToList();
        }
        public List<Client> getClients()
        {
            return db.Clients
                .OrderBy(c => c.DateScheduled)
                .ToList();
        }
        public Client getClientById(int id)
        {
            return db.Clients.FirstOrDefault(c => c.Id == id);
            
        }

        public void saveChanges()
        {
            db.SaveChanges();
        }

        public void addClient(Client newClient)
        {
            db.Clients.Add(newClient);
            db.SaveChanges();
        }

        public void deleteLead(Lead lead)
        {           
            db.Leads.Remove(lead);
            db.SaveChanges();
        }
    }
}
