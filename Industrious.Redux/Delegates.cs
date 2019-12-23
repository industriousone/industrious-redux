using System;

namespace Industrious.Redux
{
	/// <summary>
	///  Selects and returns a value from a collection contained by the application state.
	/// </summary>
	/// <typeparam name="TState">
	///  The application state type.
	/// </typeparam>
	/// <typeparam name="T">
	///  The type of the collection value to be returned.
	/// </typeparam>
	/// <typeparam name="KI">
	///  The type of the key or index that is used to select the value from the collection.
	/// </typeparam>
	/// <param name="state">
	///  The current application state.
	/// </param>
	/// <param name="keyOrIndex">
	///  The key or indexed used to look up the desired value in the collection.
	/// </param>
	/// <returns>
	///  The requested collection item or a meaningful alternative (i.e. <c>null</c>).
	/// </returns>
	public delegate T ElementProjector<TState, T, KI>(TState state, KI keyOrIndex);


	/// <summary>
	///  Selects and returns a subset of values from the full application state.
	/// </summary>
	/// <typeparam name="TState">
	///  The application state type.
	/// </typeparam>
	/// <typeparam name="T">
	///  The return type, representing the desired subset of the full state.
	/// </typeparam>
	/// <param name="state">
	///  The current application state.
	/// </param>
	/// <returns>
	///  The selected values from the application state.
	/// </returns>
	public delegate T ValueProjector<TState, T>(TState state);


	/// <summary>
	///  Produce a new state object in response to an action.
	/// </summary>
	public delegate TState Reducer<TState>(TState state, Object action);
}
