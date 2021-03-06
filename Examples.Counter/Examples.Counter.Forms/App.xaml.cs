﻿using System;
using Xamarin.Forms;

using Industrious.Redux;

using Examples.Counter.ViewModels;

namespace Examples.Counter.Forms
{
	public partial class App : Application
	{
		private readonly IStore<AppState> _store;


		public App()
		{
			InitializeComponent();

			_store = new Store<AppState>(Reducers.Root, new AppState());

			MainPage = new NavigationPage(new MainPage()
			{
				BindingContext = new MainPageModel(_store)
			});
		}
	}
}
