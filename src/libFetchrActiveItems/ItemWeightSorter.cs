using libFetchrActiveItems.DataStructures;
using System.Collections.Generic;

namespace libFetchrActiveItems
{
	internal class ItemWeightSorter : IComparer<ItemData>
	{
		public int Compare(ItemData x, ItemData y)
		{
			if (x.Weight == y.Weight) return Translate.ItemName(x).CompareTo(Translate.ItemName(y));
			return y.Weight.CompareTo(x.Weight);
		}
	}
}
