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
    /// Логика взаимодействия для Employees.xaml
    /// </summary>
    public partial class Employees : Window
    {
        public ObservableCollection<Employee> EmployeesList { get; set; } = new ObservableCollection<Employee>();
        public Employees()
        {
            InitializeComponent();
            LoadEmployees();
            EmployeesDataGrid.ItemsSource = EmployeesList;
        }
        private async void LoadEmployees()
        {
            try
            {
                using (var context = new DataContext())
                {
                    var employees = await context.Employees.ToListAsync(); // Замените Employees на имя вашей таблицы
                    EmployeesList = new ObservableCollection<Employee>(employees);
                    EmployeesDataGrid.ItemsSource = EmployeesList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
