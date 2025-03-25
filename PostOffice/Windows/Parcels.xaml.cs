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
using Microsoft.EntityFrameworkCore;
using PostOffice.Models;
using System.Collections.ObjectModel;

namespace PostOffice.Windows
{
    /// <summary>
    /// Логика взаимодействия для Parcels.xaml
    /// </summary>
    public partial class Parcels : Window
    {

        public ObservableCollection<Parcel> ParcelsList { get; set; } = new ObservableCollection<Parcel>();
        public Parcels()
        {
            InitializeComponent();
            LoadParcels();
            ParcelsDataGrid.ItemsSource = ParcelsList;
        }


        private void LoadParcels()
        {
            try
            {
                using (var context = new DataContext())
                {
                    // Загрузка данных о посылках и связанных данных (отправитель, получатель, тип, статус)
                    var parcels = context.Parcels
                        .Include(p => p.IdSenderNavigation)
                        .Include(p => p.IdRecipientNavigation)
                        .Include(p => p.IdParcelTypeNavigation)
                        .Include(p => p.IdParcelStatusNavigation)
                        .ToList();

                    foreach (var parcel in parcels)
                    {
                        ParcelsList.Add(parcel);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }
    }
}
