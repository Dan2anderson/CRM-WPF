using System.Runtime.ExceptionServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CustomerRelationshipManagment.db;
using CustomerRelationshipManagment.Interfaces;
using CustomerRelationshipManagment.Models;
using CustomerRelationshipManagment.ViewModels;

namespace CustomerRelationshipManagment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly IMainViewModel _mainViewModel;


        public MainWindow(IMainViewModel mainViewModel)
        {
            InitializeComponent();

            _mainViewModel = mainViewModel;
            DataContext = mainViewModel;

            // Example data

            //using (var context = new AppDbContext())
            //{
            //    context.Database.EnsureCreated();
            //    //var leads = context.Leads.ToList();
            //    //LeadsDataGrid.ItemsSource = leads;

            //    var clients = context.Clients.ToList();
            //    ClientDataGrid.ItemsSource = clients;

                
            //}           
        }

        private void NewLead_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the new view
            NewLead newLead = new NewLead();

            // Show the new view
            newLead.ShowDialog();
            _mainViewModel.LoadLeads();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var client = button.DataContext as Client;

            if (client != null)
            {
                MessageBox.Show($"Details for {client.Name}");
            }
        }
    }
}