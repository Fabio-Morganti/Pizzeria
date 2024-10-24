
using Pizzeria.ViewModels;

namespace Pizzeria.Views;

public partial class DetailsPage : ContentPage
{
	DetailsViewModel ViewModel;

	public DetailsPage()
	{
		InitializeComponent();

		ViewModel = new DetailsViewModel();
		BindingContext = ViewModel;

		ViewModel.LoadData();
	}
}
