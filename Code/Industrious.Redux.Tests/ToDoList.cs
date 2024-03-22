namespace Industrious.Redux.Tests;

public record ToDoItem (Int32 Id, String Text, Boolean IsComplete);


public record ToDoList (String ListName, ImmutableList<ToDoItem> Items)
{
	public static readonly ToDoList Empty = new ToDoList ("Untitled", ImmutableList<ToDoItem>.Empty);


	public static ToDoList Reduce (ToDoList current, Object action)
	{
		return action switch {
			AddItemAction addItemAction =>
				new ToDoList (current.ListName, current.Items.Add (new ToDoItem (current.Items.Count, addItemAction.Text, addItemAction.IsComplete))),

			SetNameAction setNameAction =>
				new ToDoList (setNameAction.NewName, current.Items),

			_ => current
		};
	}
}


public readonly record struct AddItemAction(String Text, Boolean IsComplete = false);

public readonly record struct SetNameAction (String NewName);
