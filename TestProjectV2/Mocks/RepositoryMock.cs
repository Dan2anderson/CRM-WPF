using Moq;
using CustomerRelationshipManagment.Interfaces;
using CustomerRelationshipManagment.db;
using System;
using CustomerRelationshipManagment.Models;

namespace TestProjectV2.Mocks
{
    public static class RepositoryMock
    {
        public static Mock<RepositoryInterface> CreateMockRepository()
        {
           
            var mockLeads = new List<Lead>
            {
                new Lead { Id = 1, Name = "Lead 1", Frequency = "Weekly", Phone = "1234567890", Address = "123 Main St", QuotePrice = 100, Acres = 1.5, DateScheduled = DateOnly.FromDateTime(DateTime.Now.AddDays(7)), Notes = "Test Lead 1" },
                new Lead { Id = 2, Name = "Lead 2", Frequency = "Monthly", Phone = "9876543210", Address = "456 Center St", QuotePrice = 200, Acres = 2.0, DateScheduled = DateOnly.FromDateTime(DateTime.Now.AddDays(30)), Notes = "Test Lead 2" }
            };

     
            var mockClients = new List<Client>
            {
                new Client { Id = 1, Name = "Client 1", Frequency = "Weekly", Phone = "1234567890", Address = "789 Elm St", Price = 150, Acres = 1.0, DateScheduled = DateOnly.FromDateTime(DateTime.Now), Notes = "Test Client 1" },
                new Client { Id = 2, Name = "Client 2", Frequency = "Bi-Weekly", Phone = "9876543210", Address = "321 Oak St", Price = 250, Acres = 2.5, DateScheduled = DateOnly.FromDateTime(DateTime.Now), Notes = "Test Client 2" }
            };

      
            var mockRepository = new Mock<RepositoryInterface>();

         
            mockRepository.Setup(repo => repo.getLeads()).Returns(mockLeads);

            mockRepository.Setup(repo => repo.getClients()).Returns(mockClients);

            mockRepository.Setup(repo => repo.getClientById(It.IsAny<int>()))
                .Returns((int id) => mockClients.FirstOrDefault(client => client.Id == id));

            mockRepository.Setup(repo => repo.saveChanges())
                .Callback(() => { /* Simulate saving changes */ });

            mockRepository.Setup(repo => repo.addClient(It.IsAny<Client>()))
                .Callback((Client newClient) => mockClients.Add(newClient));

            mockRepository.Setup(repo => repo.deleteLead(It.IsAny<Lead>()))
                .Callback((Lead lead) => mockLeads.Remove(lead));

            return mockRepository;
        }
    }
}
