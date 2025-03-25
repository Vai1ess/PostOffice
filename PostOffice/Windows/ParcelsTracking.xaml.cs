using PostOffice.Data;
using PostOffice.Models;
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


namespace PostOffice.Windows
{
    /// <summary>
    /// Логика взаимодействия для ParcelsTracking.xaml
    /// </summary>
    public partial class ParcelsTracking : Window
    {
        private readonly int _idParcel;
        private int? _selectedBranchId;
        private int? _selectedStatusId;
        public ParcelsTracking()
        {
            InitializeComponent();
           // _idParcel = idParcel;
            ParcelIdTextBox.Text = _idParcel.ToString();
            LoadBranches();
            LoadStatuses();
        }
        private async Task LoadBranches()
        {
            try
            {
                using (var context = new DataContext())
                {
                    var branches = await context.Branches.ToListAsync();
                    BranchComboBox.ItemsSource = branches;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки отделений: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadStatuses()
        {
            try
            {
                using (var context = new DataContext())
                {
                    var parcelStatuses = await context.ParcelStatuses.ToListAsync();
                    StatusComboBox.ItemsSource = parcelStatuses;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки статусов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BranchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BranchComboBox.SelectedItem is Branch selectedBranch)
            {
                _selectedBranchId = selectedBranch.IdBranch;
            }
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusComboBox.SelectedItem is ParcelStatus selectedStatus)
            {
                _selectedStatusId = selectedStatus.IdParcelStatuses;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. Получаем данные из формы
            string description = DescriptionTextBox.Text?.Trim();

            // 2. Проверяем, что все необходимые данные выбраны / заполнены
            if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Пожалуйста, введите описание.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_selectedBranchId == null)
            {
                MessageBox.Show("Пожалуйста, выберите отделение.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_selectedStatusId == null)
            {
                MessageBox.Show("Пожалуйста, выберите статус.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Создаём новую запись
            var newTrackingRecord = new ParcelTracking
            {
                IdParcel = _idParcel,
                IdBranch = _selectedBranchId,
                IdPercelStatus = _selectedStatusId,
                TrackingDate = DateTime.Now,
                Description = description
            };

            try
            {
                using (var context = new DataContext())
                {
                    context.ParcelTrackings.Add(newTrackingRecord);
                    await context.SaveChangesAsync();
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения записи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}