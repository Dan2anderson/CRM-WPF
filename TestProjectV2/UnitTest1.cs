using CustomerRelationshipManagment.db;
using CustomerRelationshipManagment.Interfaces;
using CustomerRelationshipManagment.Models;
using CustomerRelationshipManagment.ViewModels;
//using Microsoft.EntityFrameworkCore;
using Moq;
using TestProjectV2.Mocks;

namespace TestProjectV2
{
    [TestFixture]
    public class Tests
    {


        private MainWindowViewModel _viewModel;
        private Mock<RepositoryInterface> _mockRepository;

        [SetUp]
        public void SetUp()
        {
            // Create the mocked repository
            _mockRepository = RepositoryMock.CreateMockRepository();

            // Initialize the ViewModel with the mocked repository
            _viewModel = new MainWindowViewModel(_mockRepository.Object);
        }

        [Test]
        public void LoadLeads_ShouldPopulateLeadsCollection()
        {
           
            _viewModel.LoadLeads();

            
            Assert.That(2, Is.EqualTo(_viewModel.Leads.Count));
            Assert.That("Lead 1", Is.EqualTo(_viewModel.Leads[0].Name));
            Assert.That("Lead 2", Is.EqualTo(_viewModel.Leads[1].Name));
            //Assert.Equals("Lead 1", _viewModel.Leads[0].Name);
            //Assert.Equals("Lead 2", _viewModel.Leads[1].Name);
        }

        [Test]
        public void LoadClients_ShouldAddClientToRepository()
        {

            var newClient = new Client { Id = 3, Name = "New Client", Frequency = "Monthly", Phone = "5555555555", Address = "123 New St", Price = 300, Acres = 3.0 };

            _viewModel.LoadClients();

            Assert.That(2, Is.EqualTo(_mockRepository.Object.getClients().Count));
            Assert.That("Client 1", Is.EqualTo(_viewModel.Clients[0].Name));
            Assert.That("Client 2", Is.EqualTo(_viewModel.Clients[1].Name));

        }

        [Test]
        public void LeadToClient_ShouldRemoveLeadFromRepositoryAndAddClient()
        {

            _viewModel.LeadToClient(currentLead: _mockRepository.Object.getLeads()[0]);

            Assert.That(1, Is.EqualTo(_viewModel.Leads.Count));
            Assert.That(3, Is.EqualTo(_viewModel.Clients.Count));
        }

        [Test]
        public void RecordMowing_ShouldUpdateClientDates()
        {
            var client1 = _viewModel.Clients[0];
            var client2 = _viewModel.Clients[1];
            _viewModel.RecordMowing(client1);
            _viewModel.RecordMowing(client2);
            Assert.That(client1.DateLastMowed, Is.Not.Null);
            Assert.That(client1.DateScheduled, Is.Not.Null);
            Assert.That(client2.DateLastMowed, Is.Not.Null);
            Assert.That(client2.DateScheduled, Is.Not.Null);
            Assert.That(client1.DateScheduled, Is.EqualTo(client1.DateLastMowed?.AddDays(7)));
            Assert.That(client2.DateScheduled, Is.EqualTo(client2.DateLastMowed?.AddDays(14)));
        }


    }
}