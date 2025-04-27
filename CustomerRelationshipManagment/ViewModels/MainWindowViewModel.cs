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
    public class MainWindowViewModel : IMainViewModel
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
            var leads = _repository.getLeads();
            Leads.Clear();
            foreach (var lead in leads)
            {
                Leads.Add(lead);
            }
        }
        public void LoadClients()
        {
            var clients = _repository.getClients();
            Clients.Clear();
            foreach (var client in clients)
            {
                Clients.Add(client);
            }
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




            var clientInDb = _repository.getClientById(currentClient.Id);
            if (clientInDb != null)
            {
                clientInDb.DateLastMowed = lastMowed;
                clientInDb.DateScheduled = lastMowed?.AddDays(daysToAdd);
                _repository.saveChanges();
                LoadClients();
            }

        }

        public void LeadToClient(Lead currentLead)
        {


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
                _repository.addClient(newClient);
                _repository.deleteLead(currentLead);
                LoadLeads();
                LoadClients();
            }
        }
    }
}
