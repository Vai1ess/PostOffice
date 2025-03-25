using PostOffice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
using PostOffice.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace PostOffice.Windows
{
    /// <summary>
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        public ObservableCollection<Client> ClientsList { get; set; } = new ObservableCollection<Client>();
        public Clients()
        {
            InitializeComponent();
            LoadClients();
            ClientsDataGrid.ItemsSource = ClientsList;
        }

        private async void LoadClients()
        {
            try
            {
                using (var context = new DataContext())
                {
                    var clients = await context.Clients.ToListAsync();

                    ClientsList.Clear();

                    foreach (var client in clients)
                    {
                        ClientsList.Add(client);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
