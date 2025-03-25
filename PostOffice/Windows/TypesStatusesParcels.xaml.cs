using PostOffice.Data;
using PostOffice.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.EntityFrameworkCore;

namespace PostOffice.Windows
{
    /// <summary>
    /// Логика взаимодействия для TypesStatusesParcels.xaml
    /// </summary>
    public partial class TypesStatusesParcels : Window
    {
        public ObservableCollection<ParcelType> ParcelTypesList { get; set; } = new ObservableCollection<ParcelType>();
        public ObservableCollection<ParcelStatus> ParcelStatusesList { get; set; } = new ObservableCollection<ParcelStatus>();
        public TypesStatusesParcels()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                using (var context = new DataContext())
                {
                    var parcelTypes = await context.ParcelTypes.ToListAsync();
                    ParcelTypesList = new ObservableCollection<ParcelType>(parcelTypes);
                    ParcelTypesDataGrid.ItemsSource = ParcelTypesList;

                    var parcelStatuses = await context.ParcelStatuses.ToListAsync();
                    ParcelStatusesList = new ObservableCollection<ParcelStatus>(parcelStatuses);
                    ParcelStatusesDataGrid.ItemsSource = ParcelStatusesList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
