using System.Diagnostics;
using System.Windows.Input;
using Pizzeria.Views;

namespace Pizzeria;

public partial class MainPage : ContentPage
{
    public ICommand GoCommand { get; }
    int group = -1;

    public MainPage()
	{
        GoCommand = new Command(() =>
        {
            //Apre la pagina di DetailsPage e porta con se l'informazione del gruppo di persone scelto
            if(RangePicker.SelectedIndex != -1)
            {
                Shell.Current.GoToAsync($"{nameof(DetailsPage)}?group={group}");
            }
            else
            {
                DisplayAlert("ATTENZIONE", "Selezionare un gruppo", "OK");
            }
            
        });

        InitializeComponent();
	}

    void RangePicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
    {
        group = RangePicker.SelectedIndex;
    }
}


