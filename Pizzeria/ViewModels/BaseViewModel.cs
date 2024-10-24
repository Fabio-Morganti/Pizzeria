using System;
using System.ComponentModel;

namespace Pizzeria.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
    {
		public BaseViewModel()
		{
		}

        //Metodo utilizzato per supportare il data binding in un contesto MVVM
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}

    