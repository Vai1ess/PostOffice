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
    /// Логика взаимодействия для Branches.xaml
    /// </summary>
    public partial class Branches : Window
    {
        public ObservableCollection<Branch> BranchesList { get; set; } = new ObservableCollection<Branch>();
        public Branches()
        {
            InitializeComponent();
            LoadBranches();
            BranchesDataGrid.ItemsSource = BranchesList;
        }
        private async void LoadBranches()
        {
            try
            {
                using (var context = new DataContext())
                {
                    var branches = await context.Branches.ToListAsync();
                    BranchesList = new ObservableCollection<Branch>(branches);
                    BranchesDataGrid.ItemsSource = BranchesList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
