using PostOffice.Data;
using PostOffice.Models;
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

namespace PostOffice.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private async void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext; 


            var newClient = new Client
            {
                Name = viewModel.NewClientName,
                Surname = viewModel.NewClientSurname,
                Address = viewModel.NewClientAddress,
                City = viewModel.NewClientCity,
                ZipCode = viewModel.NewClientZipCode,
                PhoneNumber = viewModel.NewClientPhoneNumber,
                Email = viewModel.NewClientEmail
            };

            try
            {
                using (var dbContext = new DataContext()) 
                {
                    dbContext.Clients.Add(newClient);
                    await dbContext.SaveChangesAsync();

                    await viewModel.LoadRecipientCustomersAsync();

                    viewModel.NewClientName = "";
                    viewModel.NewClientSurname = "";
                    viewModel.NewClientAddress = "";
                    viewModel.NewClientCity = "";
                    viewModel.NewClientZipCode = "";
                    viewModel.NewClientPhoneNumber = "";
                    viewModel.NewClientEmail = "";

                    MessageBox.Show("Клиент успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void BranchesButton_Click(object sender, RoutedEventArgs e)
        {
            Branches branchesWindow = new Branches();
            branchesWindow.Show();
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            Clients clientsWindow = new Clients();
            clientsWindow.Show();
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            Employees employeesWindow = new Employees();
            employeesWindow.Show();
        }

        private void ParcelsButton_Click(object sender, RoutedEventArgs e)
        {
            Parcels parcelsWindow = new Parcels();
            parcelsWindow.Show();
        }


        private void ParcelsTrackingButton_Click(object sender, RoutedEventArgs e)
        {
            ParcelsTracking parcelTrackingWindow = new ParcelsTracking();
            parcelTrackingWindow.Show();
        }

        private void TypesStatusesParcelsButton_Click(object sender, RoutedEventArgs e)
        {
            TypesStatusesParcels parcelTypesStatusesWindow = new TypesStatusesParcels();
            parcelTypesStatusesWindow.Show();
        }
    }
}