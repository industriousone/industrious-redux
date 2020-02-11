using System;
using System.Collections.Generic;

namespace Examples.SyncViews
{
	public class Item : IEquatable<Item>
	{
		public Item()
			: this(Guid.NewGuid(), false)
		{ }


		private Item(Guid id, Boolean toggleValue)
		{
			ID = id;
			Text = id.ToString().Substring(0, 8);
			ToggleValue = toggleValue;
		}


		public Guid ID { get; }


		public String Text { get; }


		public Boolean ToggleValue { get; }


		public static bool operator ==(Item item1, Item item2)
		{
			return EqualityComparer<Item>.Default.Equals(item1, item2);
		}


		public static bool operator !=(Item item1, Item item2)
		{
			return !(item1 == item2);
		}


		public bool Equals(Item other)
		{
			if (other is null)
				return (false);

			return (ID == other.ID && ToggleValue == other.ToggleValue);
		}


		public override bool Equals(Object obj)
		{
			return this.Equals(obj as Item);
		}


		public override int GetHashCode()
		{
			var hashCode = -612858477;
			hashCode = hashCode * -1521134295 + ID.GetHashCode();
			hashCode = hashCode * -1521134295 + ToggleValue.GetHashCode();
			return hashCode;
		}


		public Item WithToggleValue(Boolean newValue)
		{
			return (new Item(ID, newValue));
		}
	}
}
