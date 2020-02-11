using System;

namespace Examples.SyncViews
{
	public class Actions
	{
		public struct AddItem
		{ }


		public struct DeleteItem
		{
			public DeleteItem(Guid itemId)
			{
				ItemId = itemId;
			}

			public Guid ItemId { get; }
		}


		public struct ResetItems
		{ }


		public struct ToggleItem
		{
			public ToggleItem(Guid itemId, Boolean toggleValue)
			{
				ItemId = itemId;
				ToggleValue = toggleValue;
			}

			public Guid ItemId { get; }

			public Boolean ToggleValue { get; }
		}
	}
}
