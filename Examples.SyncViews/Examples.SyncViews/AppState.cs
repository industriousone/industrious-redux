using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Examples.SyncViews
{
	public struct AppState
	{
		public static readonly AppState Empty = AppState.FromItems(new Item[] { });

		public static readonly AppState SampleState = AppState.FromItems(new Item[]
			{
				new Item(),
				new Item(),
				new Item()
			});


		public static AppState FromItems(IEnumerable<Item> items)
		{
			var dictionaryBuilder = ImmutableDictionary.CreateBuilder<Guid, Item>();
			dictionaryBuilder.AddRange(items.Select(i => new KeyValuePair<Guid, Item>(i.ID, i)));

			return new AppState()
			{
				Items = dictionaryBuilder.ToImmutable(),
				Index = ImmutableList<Guid>.Empty.AddRange(items.Select(item => item.ID))
			};
		}


		/// <summary>
		///  The current ordering of items, as a list of item IDs. Keeping a list of IDs,
		///  rather than a list of Items, means that the list doesn't need to change
		///  (and trigger downstream updates) every time the contents of an Item changes.
		///  Instead the list only updates when a item is added or removed.
		/// </summary>
		public ImmutableList<Guid> Index { get; private set; }


		/// <summary>
		///  The current collection of Items, keyed by their ID. Keeping this as a
		///  separate dictionary allows the ItemViewCellModels to efficiently monitor
		///  their corresponding item via ID lookups.
		/// </summary>
		public ImmutableDictionary<Guid, Item> Items { get; private set; }


		public AppState AddItem(Item newItem)
		{
			return (new AppState
			{
				Items = Items.Add(newItem.ID, newItem),
				Index = Index.Add(newItem.ID)
			});
		}


		public AppState DeleteItem(Guid itemId)
		{
			return (new AppState
			{
				Items = Items.Remove(itemId),
				Index = Index.Remove(itemId)
			});
		}


		public AppState ToggleItem(Guid itemId, Boolean toggleValue)
		{
			if (Items.ContainsKey(itemId))
			{
				return (new AppState
				{
					Items = Items.SetItem(itemId, Items[itemId].WithToggleValue(toggleValue)),
					Index = Index
				});
			}
			else
			{
				return (this);
			}
		}
	}
}
