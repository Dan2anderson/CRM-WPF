using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CustomerRelationshipManagment.db;

namespace CustomerRelationshipManagment
{
    /// <summary>
    /// Interaction logic for NewLead.xaml
    /// </summary>
    public partial class NewLead : Window
    {
        public NewLead()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //todo parse and save the data. 

            using (var context = new AppDbContext())
            {
                var newLead = new Models.Lead
                {
                    Name = NameTextBox.Text.Trim(),
                    Phone = PhoneTextBox.Text.Trim(),
                    Address = AddressTextBox.Text.Trim(),
                    Frequency = FrequencyBox.Text.Trim(),
                    QuotePrice = decimal.Parse(PriceBox.Text.Trim()),
                    Acres = double.Parse(AcresTextBox.Text.Trim()),
                    DateScheduled = DateOnly.FromDateTime(DateScheduledDateBox.SelectedDate.Value),
                    Notes = NotesTextBox.Text.Trim()
                };
                // Create a new lead object
                context.Database.EnsureCreated();             
                var clients = context.Clients.ToList();
                context.Leads.Add(newLead);
                context.SaveChanges();


            }
            //todo close this window
            this.Close();
            //todo return to main window
        }
    }
}
