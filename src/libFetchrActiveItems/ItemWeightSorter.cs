using libFetchrActiveItems.DataStructures;
using System.Collections.Generic;
using System.Linq;

namespace libFetchrActiveItems
{
	internal class ItemWeightSorter(string cat) : IComparer<ItemData>
	{
		public int Compare(ItemData x, ItemData y)
		{
			int xWeight = x.Categories.Single(c => c.Id == cat).Weight;
			int yWeight = y.Categories.Single(c => c.Id == cat).Weight;

			if (xWeight == yWeight) return Translate.ItemNameUsingKey(x).CompareTo(Translate.ItemNameUsingKey(y));
			return yWeight.CompareTo(xWeight);
		}
	}
}
