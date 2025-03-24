using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using PostOffice.Data;
using PostOffice.Models;
using System.Windows.Input;

namespace PostOffice
{

    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand CreateParcelCommand { get; private set; }
        public MainViewModel()
        {
            LoadSenderCustomerAsync();
            LoadRecipientCustomersAsync();
            LoadDepartureBranchAsync();
            LoadDestinationBranchAsync();
            LoadParcelTypeAsync();
            CreateParcelCommand = new RelayCommand(CreateParcel);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<Client> _senderCustomer = new ObservableCollection<Client>();
        public ObservableCollection<Client> SenderCustomer
        {
            get { return _senderCustomer; }
            set
            {
                _senderCustomer = value;
                OnPropertyChanged(nameof(SenderCustomer));
            }
        }

        private Client _selectedSender;
        public Client SelectedSender
        {
            get { return _selectedSender; }
            set
            {
                _selectedSender = value;
                OnPropertyChanged(nameof(SelectedSender));
            }
        }

        private async Task LoadSenderCustomerAsync()
        {
            try
            {
                using (var dbContext = new DataContext())
                {
                    var clients = await dbContext.Clients.ToListAsync();
                    SenderCustomer.Clear();
                    foreach (var client in clients)
                    {
                        SenderCustomer.Add(client);
                    }
                    OnPropertyChanged(nameof(SenderCustomer));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке отправителей: {ex.Message}", "Ошибка", MessageBoxButton.OK);
                Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
            }
        }



        private ObservableCollection<Client> _recipientCustomers = new ObservableCollection<Client>();
        public ObservableCollection<Client> RecipientCustomers
        {
            get { return _recipientCustomers; }
            set
            {
                _recipientCustomers = value;
                OnPropertyChanged(nameof(RecipientCustomers));
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

        public async Task LoadRecipientCustomersAsync()
        {
            try
            {
                using (var dbContext = new DataContext()) 
                {
                    var clients = await dbContext.Clients.ToListAsync();
                    RecipientCustomers.Clear(); 
                    foreach (var client in clients)
                    {
                        RecipientCustomers.Add(client);
                    }
                    OnPropertyChanged(nameof(RecipientCustomers)); 
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
                OnPropertyChanged(nameof(RecipientCustomers));
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
                OnPropertyChanged(nameof(RecipientCustomers));
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
                OnPropertyChanged(nameof(IsActive)); 
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
                OnPropertyChanged(nameof(IsActive)); 
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
                OnPropertyChanged(nameof(IsActive));
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
                OnPropertyChanged(nameof(IsActive)); 
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
                OnPropertyChanged(nameof(IsActive)); 
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
                OnPropertyChanged(nameof(IsActive)); 
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
                OnPropertyChanged(nameof(IsActive)); 
            }
        }

        public bool IsActive 
        {
            get
            {
                return !string.IsNullOrEmpty(NewClientName) &&
                       !string.IsNullOrEmpty(NewClientSurname) &&
                       !string.IsNullOrEmpty(NewClientAddress) &&
                       !string.IsNullOrEmpty(NewClientCity) &&
                       !string.IsNullOrEmpty(NewClientZipCode) &&
                       !string.IsNullOrEmpty(NewClientPhoneNumber) &&
                       !string.IsNullOrEmpty(NewClientEmail);
            }
        }


        // код для кнопки "создать заказ"

        private decimal? _weight;
        public decimal? Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        private string _dimensions;
        public string Dimensions
        {
            get { return _dimensions; }
            set
            {
                _dimensions = value;
                OnPropertyChanged(nameof(Dimensions));
            }
        }

        private decimal? _insuranceAmount;
        public decimal? InsuranceAmount
        {
            get { return _insuranceAmount; }
            set
            {
                _insuranceAmount = value;
                OnPropertyChanged(nameof(InsuranceAmount));
            }
        }

        private async void CreateParcel(object parameter)
        {
            if (SelectedSender == null || SelectedRecipient == null || SelectedDepartureBranch == null || SelectedDestinationBranch == null || SelectedParcelType == null)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля (отправитель, получатель, отделения, тип посылки).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }

            if (Weight == null || Weight <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректный вес посылки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }

            if (string.IsNullOrEmpty(Dimensions))
            {
                MessageBox.Show("Пожалуйста, введите размеры посылки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }

            if (InsuranceAmount == null || InsuranceAmount <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректную страховую сумму.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }

            if (SelectedSender.IdClient == SelectedRecipient.IdClient)
            {
                MessageBox.Show("Отправитель и получатель не могут быть одним и тем же лицом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }

            if (SelectedDepartureBranch.IdBranch == SelectedDestinationBranch.IdBranch)
            {
                MessageBox.Show("Отделения отправления и назначения не могут совпадать.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }

            try
            {
                using (var dbContext = new DataContext())
                {
                    var newParcel = new Parcel
                    {
                        IdSender = SelectedSender?.IdClient, 
                        IdRecipient = SelectedRecipient?.IdClient,
                        IdDepartureBranch = SelectedDepartureBranch?.IdBranch,
                        IdDestinationBranch = SelectedDestinationBranch?.IdBranch,
                        IdParcelType = SelectedParcelType?.IdParcelType,
                        Weight = Weight,
                        Dimensions = Dimensions,
                        ShippingDate = DateOnly.FromDateTime(DateTime.Now),
                        ShippingCost = InsuranceAmount, 

                    };

                    dbContext.Parcels.Add(newParcel);
                    await dbContext.SaveChangesAsync();

                    MessageBox.Show("Посылка успешно создана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании посылки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}