using System;
using System.Collections.Immutable;

namespace Examples.SyncViews
{
	public static class Projections
	{
		public static IImmutableList<Guid> Index(AppState state)
		{
			return (state.Index ?? ImmutableList<Guid>.Empty);
		}


		public static Item Item(AppState state, Guid itemId)
		{
			if (state.Items != null)
			{
				return (state.Items.ContainsKey(itemId))
					? state.Items[itemId]
					: null;
			}

			return (null);
		}
	}
}