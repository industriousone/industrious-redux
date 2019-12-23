using System;

namespace Examples.Counter.Forms
{
	public class Store : Industrious.Redux.Store<AppState>
	{
		public Store()
			: this(new AppState())
		{ }


		public Store(AppState initialState)
			: base(Reducers.Root, initialState)
		{ }
	}
}
