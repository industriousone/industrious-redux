using System;
using Xunit;

using Industrious.Redux;

namespace Examples.SyncViews.ViewModels.Tests
{
	public class MainPageModelTests
	{
		readonly MockStore<AppState> _store = new MockStore<AppState>(AppState.SampleState);


		[Fact]
		public void AddItemCommand_SendsAddItemAction()
		{
			var sut = new MainPageModel(_store);
			sut.AddItemCommand.Execute(null);
			Assert.IsType<Actions.AddItem>(_store.ReceivedActions[0]);
		}


		[Fact]
		public void ResetItemsCommand_SendsResetItemsAction()
		{
			var sut = new MainPageModel(_store);
			sut.ResetItemsCommand.Execute(null);
			Assert.IsType<Actions.ResetItems>(_store.ReceivedActions[0]);
		}
	}
}
