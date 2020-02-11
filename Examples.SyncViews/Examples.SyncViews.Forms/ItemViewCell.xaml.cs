using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Examples.SyncViews.ViewModels;

namespace Examples.SyncViews.Forms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemViewCell : ViewCell
	{
		public ItemViewCell()
		{
			InitializeComponent();
		}


		public void OnToggled(Object sender, ToggledEventArgs e)
		{
			var viewModel = BindingContext as ItemViewCellModel;
			viewModel?.ToggleItemCommand.Execute(e.Value);
		}
	}
}

