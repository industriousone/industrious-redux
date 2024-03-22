namespace Industrious.Redux.Tests;

public class StoreTests
{
	[Fact]
	public async void DispatchAsync_UpdatesStateViaReducer ()
	{
		var store = new Store<ToDoList> (ToDoList.Reduce, ToDoList.Empty);
		await store.DispatchAsync (new SetNameAction ("My List"));
		Assert.Equal ("My List", store.Current.ListName);
	}
}
