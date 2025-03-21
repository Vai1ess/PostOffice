﻿using System;
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
            LoadParcelTypeAsync();
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

        public async Task LoadCustomersAsync()
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
        public ObservableCollection<ParcelType> ParcelTypes
        {
            get { return _parcelType; }
            set
            {
                _parcelType = value;
                OnPropertyChanged(nameof(ParcelType));
            }
        }

        private ParcelType _selectedParcelType;
        public ParcelType SelectedParcelType
        {
            get { return _selectedParcelType; }
            set
            {
                _selectedParcelType = value;
                OnPropertyChanged(nameof(SelectedParcelType));
            }
        }



        private async Task LoadParcelTypeAsync()
        {
            try
            {
                using (var dbContext = new DataContext())
                {
                    var parcelType = await dbContext.ParcelTypes.ToListAsync();
                    ParcelTypes.Clear();
                    foreach (var typeParcel in parcelType)
                    {
                        ParcelTypes.Add(typeParcel);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке типов посылок: {ex.Message}", "Ошибка", MessageBoxButton.OK);
                Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
                throw;
            }
        }


        // код для задания функционала кнопке добавления пользователей
        private string _newClientName;
    public string NewClientName
    {
        get { return _newClientName; }
        set
        {
            _newClientName = value;
            OnPropertyChanged(nameof(NewClientName));
        }
    }

    private string _newClientSurname;
    public string NewClientSurname
    {
        get { return _newClientSurname; }
        set
        {
            _newClientSurname = value;
            OnPropertyChanged(nameof(NewClientSurname));
        }
    }

    private string _newClientAddress;
    public string NewClientAddress
    {
        get { return _newClientAddress; }
        set
        {
            _newClientAddress = value;
            OnPropertyChanged(nameof(NewClientAddress));
        }
    }

     private string _newClientCity;
    public string NewClientCity
    {
        get { return _newClientCity; }
        set
        {
            _newClientCity = value;
            OnPropertyChanged(nameof(NewClientCity));
        }
    }

     private string _newClientZipCode;
    public string NewClientZipCode
    {
        get { return _newClientZipCode; }
        set
        {
            _newClientZipCode = value;
            OnPropertyChanged(nameof(NewClientZipCode));
        }
    }

     private string _newClientPhoneNumber;
    public string NewClientPhoneNumber
    {
        get { return _newClientPhoneNumber; }
        set
        {
            _newClientPhoneNumber = value;
            OnPropertyChanged(nameof(NewClientPhoneNumber));
        }
    }

     private string _newClientEmail;
    public string NewClientEmail
    {
        get { return _newClientEmail; }
        set
        {
            _newClientEmail = value;
            OnPropertyChanged(nameof(NewClientEmail));
        }
    }




        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}