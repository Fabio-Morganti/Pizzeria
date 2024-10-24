using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Pizzeria.Models;
using Pizzeria.Services;
using static Pizzeria.Constants.Constants;

namespace Pizzeria.ViewModels
{
    [QueryProperty(nameof(group), nameof(group))]

    public class DetailsViewModel : BaseViewModel
    {
        public ICommand CalculateCommand { get; private set; }
        public ICommand VerifyCommand { get; private set; }
        public ICommand ForwardClientCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        public DetailsViewModel()
		{
            CalculateCommand = new Command(() =>
            {
                CalculatePrice();
            });

            VerifyCommand = new Command(() =>
            {
                VerifyClient();
            });

            ForwardClientCommand = new Command(() =>
            {
                ForwardClient();
            });

            ExitCommand = new Command(() =>
            {
                Exit();
            });
        }

        public ObservableCollection<Pizza> Pizze { get; set; } = new ObservableCollection<Pizza>();

        private int _group;
        public int group
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    _group = value;
                    OnPropertyChanged(nameof(group));
                }
            }
        }

        private Pizza _selectedPizza;
        public Pizza SelectedPizza
        {
            get { return _selectedPizza; }
            set
            {
                if (_selectedPizza != value)
                {
                    _selectedPizza = value;
                    OnPropertyChanged(nameof(SelectedPizza));
                }
            }
        }

        private string _fedelityId = string.Empty;
        public string FedelityId
        {
            get { return _fedelityId; }
            set
            {
                if (_fedelityId != value)
                {
                    _fedelityId = value;
                    OnPropertyChanged(nameof(FedelityId));
                }
            }
        }

        private bool _visibilityError = false;
        public bool VisibilityError
        {
            get { return _visibilityError; }
            set
            {
                if (_visibilityError != value)
                {
                    _visibilityError = value;
                    OnPropertyChanged(nameof(VisibilityError));
                }
            }
        }

        private bool _disable = false;
        public bool Disable
        {
            get { return _disable; }
            set
            {
                if (_disable != value)
                {
                    _disable = value;
                    OnPropertyChanged(nameof(Disable));
                }
            }
        }

        private int _eta = 0;
        public int Eta
        {
            get { return _eta; }
            set
            {
                if (_eta != value)
                {
                    _eta = value;
                    OnPropertyChanged(nameof(Eta));
                }
            }
        }

        private double _unitPrice = 0;
        public double UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (_unitPrice != value)
                {
                    _unitPrice = value;
                    OnPropertyChanged(nameof(UnitPrice));
                }
            }
        }

        private double _totalPrice = 0;
        public double TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (_totalPrice != value)
                {
                    _totalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }
        
        private double _selectedIndexPicker = -1;
        public double SelectedIndexPicker
        {
            get { return _selectedIndexPicker; }
            set
            {
                if (_selectedIndexPicker != value)
                {
                    _selectedIndexPicker = value;
                    OnPropertyChanged(nameof(SelectedIndexPicker));
                }
            }
        }

        private int _nclient = 1;
        public int Nclient
        {
            get { return _nclient; }
            set
            {
                if (_nclient != value)
                {
                    _nclient = value;
                    OnPropertyChanged(nameof(Nclient));
                }
            }
        }

        public void LoadData()
        {
            Pizze.Clear();
            VisibilityError = false;

            foreach (var pizza in DatabaseServices.LoadPizzeFromDB())
            {
                Pizze.Add(pizza);
            }
        }

        private void VerifyClient()
        {
            Client client = ClientServices.GetClient(FedelityId);

            if(client != null)
            {
                VisibilityError = false;
                Disable = client.Disable;
                Eta = DateTime.Now.Year - client.AnnoNascita;
            }
            else
            {
                VisibilityError = true;
            }
        }

        //Calcola il prezzo di una determinata pizza scelta applicando il corrispettivo sconto
        private void CalculatePrice()
        {
            if(SelectedPizza != null)
            {
                UnitPrice = ClientServices.GetPrice(FedelityId, Disable, Eta, group, SelectedPizza.Prezzo).Item2;

                Shell.Current.DisplayAlert("ATTENZIONE", MessageDiscount(ClientServices.GetPrice(FedelityId, Disable, Eta, group, SelectedPizza.Prezzo).Item1), "OK");
            }
            else
            {
                Shell.Current.DisplayAlert("ATTENZIONE", "Selezionare il tipo di pizza", "OK");
            }
        }

        //Resetta tutte le informazioni compilate per il precedente cliente
        private async void ForwardClient()
        {
            if(UnitPrice == 0)
            {
                await Shell.Current.DisplayAlert("ATTENZIONE", "Non hai processato il cliente corrente.", "OK");
            }
            else
            {
                var resp = await Shell.Current.DisplayAlert("ATTENZIONE", "Sei sicuro di voler procedere? Proseguendo passerai al cliente successivo.", "OK", "CANCEL");

                if (resp)
                {
                    TotalPrice += UnitPrice;

                    Nclient++;
                    SelectedIndexPicker = -1;
                    FedelityId = string.Empty;
                    Disable = false;
                    Eta = 0;
                    UnitPrice = 0;
                    VisibilityError = false;
                }
            }
        }

        //Torna alla pagina principale
        private async void Exit()
        {
            var resp = await Shell.Current.DisplayAlert("ATTENZIONE", "Sei sicuro di voler tornare alla schermata iniziale?", "OK", "CANCEL");

            if (resp)
            {
                await Shell.Current.GoToAsync("//MainPage");
            }
        }


        //Metodo che restituisce un messaggio che indica lo sconto effettuato
        private string MessageDiscount(TypeDiscount discount)
        {
            switch (discount)
            {
                case TypeDiscount.NoDiscount:
                    return "Non è stato applicato nessuno sconto";
                case TypeDiscount.Fedelity:
                    return "È stato applicato lo sconto Fedelity del 15%";
                case TypeDiscount.Group1:
                    return "È stato applicato lo sconto gruppo 15-21 del 20%";
                case TypeDiscount.Group2:
                    return "È stato applicato lo sconto gruppo 21-25 del 30%";
                case TypeDiscount.Group3:
                    return "È stato applicato lo sconto gruppo 25+ del 50%";
                case TypeDiscount.Age60:
                    return "È stato applicato lo sconto età 60+ del 70%";
                case TypeDiscount.Age12:
                    return "È stato applicato lo sconto età 12- del 20%";
                case TypeDiscount.Age4:
                    return "È stato applicato lo sconto età 4- del 50%";
                case TypeDiscount.Disability:
                    return "È stato applicato lo sconto per disabile del 90%";
                case TypeDiscount.WeekEnd:
                    return "È stato applicato lo sconto Weekend del 10%";
            }

            return string.Empty;
        }
    }

    
}

