using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using PostOffice.Data;
using PostOffice.Models;

namespace PostOffice
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            LoadCustomersAsync();
            LoadDepartureBranchAsync();
            LoadDestinationBranchAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<Client> _customers = new ObservableCollection<Client>();
        public ObservableCollection<Client> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        private Client _selectedRecipient;
        public Client SelectedRecipient
        {
            get { return _selectedRecipient; }
            set
            {
                _selectedRecipient = value;
                OnPropertyChanged(nameof(SelectedRecipient));
            }
        }

        private async Task LoadCustomersAsync()
        {
            try
            {
                using (var dbContext = new DataContext()) 
                {
                    var clients = await dbContext.Clients.ToListAsync();
                    Customers.Clear(); 
                    foreach (var client in clients)
                    {
                        Customers.Add(client);
                    }
                    OnPropertyChanged(nameof(Customers)); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке клиентов: {ex.Message}", "Ошибка", MessageBoxButton.OK);
                Console.WriteLine($"Ошибка при загрузке: {ex.Message}"); 
            }
        }



        private ObservableCollection<Branch> _departureBranch = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> DepartureBranch
        {
            get { return _departureBranch; }
            set
            {
                _departureBranch = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        private Branch _selectedDepartureBranch;
        public Branch SelectedDepartureBranch
        {
            get { return _selectedDepartureBranch; }
            set
            {
                _selectedDepartureBranch = value;
                OnPropertyChanged(nameof(SelectedDepartureBranch));
                CheckIfSameBranchSelected();
            }
        }

        private async Task LoadDepartureBranchAsync() {
            try
            {
                using (var dbContext = new DataContext())
                { 
                    var departureBranch = await dbContext.Branches.ToListAsync();
                    DepartureBranch.Clear();
                    foreach (var branch in departureBranch) {
                        DepartureBranch.Add(branch);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке отделений отправлений: {ex.Message}", "Ошибка", MessageBoxButton.OK);
                Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
                throw;
            }
        }




        private ObservableCollection<Branch> _destinationBranch = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> DestinationBranch
        {
            get { return _destinationBranch; }
            set
            {
                _destinationBranch = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        private Branch _selectedDestinationBranch;
        public Branch SelectedDestinationBranch
        {
            get { return _selectedDestinationBranch; }
            set
            {
                _selectedDestinationBranch = value;
                OnPropertyChanged(nameof(SelectedDestinationBranch));
                CheckIfSameBranchSelected();
            }
        }



        private async Task LoadDestinationBranchAsync()
        {
            try
            {
                using (var dbContext = new DataContext())
                {
                    var destinationBranch = await dbContext.Branches.ToListAsync();
                    DestinationBranch.Clear();
                    foreach (var branch in destinationBranch)
                    {
                        DestinationBranch.Add(branch);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке отделений отправлений: {ex.Message}", "Ошибка", MessageBoxButton.OK);
                Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
                throw;
            }
        }

        private void CheckIfSameBranchSelected()
        {
            if (SelectedDepartureBranch != null && SelectedDestinationBranch != null && SelectedDepartureBranch.IdBranch == SelectedDestinationBranch.IdBranch)
            {
                MessageBox.Show("Внимание! Выбраны одинаковые отделения для отправления между почтовыми отделениями");
            }
        }

        private ObservableCollection<ParcelType> _parcelType= new ObservableCollection<ParcelType>();
        public ObservableCollection<ParcelType> ParcelType
        {
            get { return _parcelType; }
            set
            {
                _parcelType = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        private ParcelType _selectedParcelType;
        public ParcelType SelectedParcelType
        {
            get { return _selectedParcelType; }
            set
            {
                _selectedParcelType = value;
                OnPropertyChanged(nameof(SelectedDestinationBranch));
                CheckIfSameBranchSelected();
            }
        }



        //private async Task LoadDestinationBranchAsync()
        //{
        //    try
        //    {
        //        using (var dbContext = new DataContext())
        //        {
        //            var destinationBranch = await dbContext.Branches.ToListAsync();
        //            DepartureBranch.Clear();
        //            foreach (var branch in destinationBranch)
        //            {
        //                DestinationBranch.Add(branch);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка при загрузке отделений отправлений: {ex.Message}", "Ошибка", MessageBoxButton.OK);
        //        Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
        //        throw;
        //    }
        //}


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}