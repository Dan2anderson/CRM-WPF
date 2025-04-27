using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CustomerRelationshipManagment.Commands;
using CustomerRelationshipManagment.db;
using CustomerRelationshipManagment.Interfaces;
using CustomerRelationshipManagment.Models;
using CustomerRelationshipManagment.Repositories;

namespace CustomerRelationshipManagment.ViewModels
{
    internal class MainWindowViewModel : IMainViewModel
    {
        public ObservableCollection<Lead> Leads { get; set; }
        public ObservableCollection<Client> Clients { get; set; }

        private readonly RepositoryInterface _repository;

        public ICommand LeadToClientCommand { get; }
        public ICommand RecordMowingCommand { get; }

        public ICommand SaveCommand => throw new NotImplementedException();

        public MainWindowViewModel(RepositoryInterface repository)
        {
            _repository = repository;

            Leads = new ObservableCollection<Lead>();
            Clients = new ObservableCollection<Client>();
            LoadLeads();
            LoadClients();

            LeadToClientCommand = new RelayCommand(lead => LeadToClient((Lead)lead));
            RecordMowingCommand = new RelayCommand(client => RecordMowing((Client)client));
        }

        public void LoadLeads()
        {
            //using (var context = _repository.getAppDBContext())
            //{
            var context = _repository.getAppDBContext();
            context.Database.EnsureCreated();
                var leads = context.Leads.ToList();
                Leads.Clear();
                foreach (var lead in leads)
                {
                    Leads.Add(lead);
                }           
            //}
        }
        public void LoadClients()
        {
            //using (var context = _repository.getAppDBContext())
            //{
            var context = _repository.getAppDBContext();
                context.Database.EnsureCreated();

            var clients = _repository.getAppDBContext().Clients
                    .OrderBy(c => c.DateScheduled)
                    .ToList();
                Clients.Clear();
                foreach (var client in clients)
                {
                    Clients.Add(client);
                }
            //}
        }

        public void RecordMowing(Client currentClient)
        {
            DateOnly? lastMowed = DateOnly.FromDateTime(DateTime.Now);
            if (currentClient.DateScheduled != null)
            {
                lastMowed = currentClient.DateScheduled;
            }

            int daysToAdd = 0;
            switch (currentClient.Frequency)
            {
                case "Weekly":
                    daysToAdd = 7;
                    break;
                case "Bi-Weekly":
                    daysToAdd = 14;
                    break;
                case "Monthly":
                    daysToAdd = 30;
                    break;
                default:
                    daysToAdd = 0;
                    break;
            }


            //using (var context = _repository.getAppDBContext())
            //{
            var context = _repository.getAppDBContext();
           
            var clientInDb = context.Clients.FirstOrDefault(c => c.Id == currentClient.Id);
                if (clientInDb != null)
                {
                    clientInDb.DateLastMowed = lastMowed;
                    clientInDb.DateScheduled = lastMowed?.AddDays(daysToAdd);
                    context.SaveChanges();
                   LoadClients();
                }
            //}

        }

        public void LeadToClient(Lead currentLead)
        {
            //parse lead to client
            //save client to DB
            //delete Lead from DB
            //refresh UI. 
            //using (var context = _repository.getAppDBContext())
            //{
            var context = _repository.getAppDBContext();
          
            context.Database.EnsureCreated();            
                if (currentLead != null)
                {
                    var newClient = new Client
                    {
                        Name = currentLead.Name,
                        Phone = currentLead.Phone,
                        Address = currentLead.Address,
                        Frequency = currentLead.Frequency,
                        Price = currentLead.QuotePrice,
                        Acres = currentLead.Acres,
                        DateScheduled = currentLead.DateScheduled,
                        Notes = currentLead.Notes
                    };
                    context.Clients.Add(newClient);
                    context.Leads.Remove(currentLead);
                    context.SaveChanges();
                    LoadLeads();
                    LoadClients();
                }
            //}
        }
    }
}
