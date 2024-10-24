using System;
using System.ComponentModel;

namespace Pizzeria.Models
{
	public class Pizza : INotifyPropertyChanged
    {
		public Pizza(string nome, double prezzo)
		{
            this.Nome = nome;
            this.Prezzo = prezzo;
		}

        private string _nome;
        public string Nome
        {
            get
            {
                return (_nome);
            }
            set
            {
                _nome = value;
                OnPropertyChanged(nameof(Nome));
            }
        }

        private double _prezzo;
        public double Prezzo
        {
            get
            {
                return (_prezzo);
            }
            set
            {
                _prezzo = value;
                OnPropertyChanged(nameof(Prezzo));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}

