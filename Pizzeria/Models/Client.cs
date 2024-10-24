using System;
using System.ComponentModel;
using Pizzeria.ViewModels;

namespace Pizzeria.Models
{
	public class Client : BaseViewModel
    {
        public Client(string nome, string fedelityId, bool disable, int anno)
        {
            this.Nome = nome;
            this.FedelityId = fedelityId;
            this.Disable = disable;
            this.AnnoNascita = anno;
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

        private string _fedelityId;
        public string FedelityId
        {
            get
            {
                return (_fedelityId);
            }
            set
            {
                _fedelityId = value;
                OnPropertyChanged(nameof(FedelityId));
            }
        }

        private bool _disable;
        public bool Disable
        {
            get
            {
                return (_disable);
            }
            set
            {
                _disable = value;
                OnPropertyChanged(nameof(Disable));
            }
        }

        private int _annoNascita;
        public int AnnoNascita
        {
            get
            {
                return (_annoNascita);
            }
            set
            {
                _annoNascita = value;
                OnPropertyChanged(nameof(AnnoNascita));
            }
        }
    }
}

